using Application.DTO;
using Application.DTO.Enums;
using Application.Repositories;
using Npgsql;

namespace Infrastructure.Repositories;

public class RatingRepository(NpgsqlDataSource dataSource) : IRatingRepository
{
    public async Task<long> AddAsync(RatingDto rating, CancellationToken token)
    {
        const string sql = """
                           insert into ratings (subject_id, subject_type, rater_id, stars, comment)
                           values (@subject_id, @subject_type, @rater_id, @stars, @comment)
                           returning rating_id;
                           """;

        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync(token);

        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("subject_id", rating.SubjectId);
        command.Parameters.AddWithValue("subject_type", rating.SubjectType);
        command.Parameters.AddWithValue("rater_id", rating.RaterId);
        command.Parameters.AddWithValue("stars", rating.Stars);
        if (rating.Comment != null)
        {
            command.Parameters.AddWithValue("comment", rating.Comment);
        }

        return (long)(await command.ExecuteScalarAsync(token) ??
                      throw new NullReferenceException("could not add rating"));
    }

    public async Task<IEnumerable<RatingDto>> GetRatingsByIdAsync(long id, CancellationToken token)
    {
        const string sql = """
                           select 
                               subject_type,
                               subject_id,
                               rater_id,
                               stars,
                               comment,
                               created_at
                           from ratings
                           where subject_id = @subject_id;
                           """;

        await using NpgsqlConnection connection = await dataSource.OpenConnectionAsync(token);

        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("subject_id", id);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync(token);

        var ratings = new List<RatingDto>();

        while (await reader.ReadAsync(token))
        {
            ratings.Add(new RatingDto(
                reader.GetFieldValue<SubjectType>(0),
                reader.GetInt64(1),
                reader.GetInt64(2),
                reader.GetInt32(3),
                reader.GetString(4),
                reader.GetDateTime(5)));
        }

        return ratings;
    }
}