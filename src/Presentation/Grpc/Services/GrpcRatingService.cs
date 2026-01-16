using Application.Contracts;
using Application.DTO;
using Grpc.Core;
using Presentation.Grpc.Mapper;
using RatingService.Api.Grpc;

namespace Presentation.Grpc.Services;

public class GrpcRatingService : RatingService.Api.Grpc.RatingService.RatingServiceBase
{
    private readonly IRatingService _service;

    public GrpcRatingService(IRatingService service)
    {
        _service = service;
    }

    public override async Task<GetRatingResponse> GetRating(GetRatingRequest request, ServerCallContext context)
    {
        GetRatingDto rating = await _service.GetRatingsByUserIdAsync(request.SubjectId, context.CancellationToken);

        return GrpcMapper.ToGetRatingGrpcResponse(rating);
    }
}