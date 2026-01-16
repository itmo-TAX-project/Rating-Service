using Application.DTO;

namespace Application.Contracts;

public interface IRatingService
{
    Task<long> AddRatingAsync(RatingDto rating, CancellationToken token);

    Task<GetRatingDto> GetRatingsByUserIdAsync(long id, CancellationToken token);
}