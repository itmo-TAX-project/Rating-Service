using Application.DTO;
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

    public Task<RatingDto> GetByIdAsync(long id, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}