using Application.DTO;

namespace Application.Services.Interfaces;

public interface IRatingService
{
    Task<long> AddRatingAsync(RatingDto rating, CancellationToken token);

    Task<RatingDto> GetRatingAsync(long id, CancellationToken token);
}