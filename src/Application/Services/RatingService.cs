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

    public Task<RatingDto> GetRatingAsync(long id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    private static TransactionScope CreateTransactionScope()
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);
    }
}