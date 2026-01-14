using Application.DTO;
using Application.Repositories;
using Application.Services.Interfaces;
using System.Transactions;

namespace Application.Services;

public class RatingService(
    IRatingRepository repository) : IRatingService
{
    public async Task<long> AddRatingAsync(RatingDto rating, CancellationToken token)
    {
        using TransactionScope transaction = CreateTransactionScope();

        long ratingId = await repository.AddAsync(rating, token);

        transaction.Complete();

        return ratingId;
    }

    public async Task<GetRatingDto> GetRatingsByUserIdAsync(long id, CancellationToken token)
    {
        using TransactionScope transaction = CreateTransactionScope();

        IEnumerable<RatingDto> ratings = await repository.GetRatingsByIdAsync(id, token);

        long count = 0;
        decimal sum = 0;

        foreach (RatingDto rating in ratings)
        {
            count++;
            sum += rating.Stars;
        }

        transaction.Complete();

        return new GetRatingDto
        {
            SubjectId = id,
            Average = count == 0 ? 0 : sum / count,
            Count = count,
        };
    }

    private static TransactionScope CreateTransactionScope()
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);
    }
}