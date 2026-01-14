using Application.DTO;

namespace Application.Repositories;

public interface IRatingRepository
{
    Task<long> AddAsync(RatingDto rating, CancellationToken token);

    Task<IEnumerable<RatingDto>> GetRatingsByIdAsync(long id, CancellationToken token);
}