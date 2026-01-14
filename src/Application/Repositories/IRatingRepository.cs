using Application.DTO;

namespace Application.Repositories;

public interface IRatingRepository
{
    Task<long> AddAsync(RatingDto rating, CancellationToken token);

    Task<RatingDto> GetByIdAsync(long id, CancellationToken token);
}