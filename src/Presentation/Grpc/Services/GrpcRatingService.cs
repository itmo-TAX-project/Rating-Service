using Application.DTO;
using Application.Services.Interfaces;
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

    public override async Task<AddRatingResponse> AddRating(AddRatingRequest request, ServerCallContext context)
    {
        long ratingId = await _service.AddRatingAsync(
            new RatingDto(GrpcMapper.MapSubjectType(request.SubjectType), request.SubjectId, request.RaterId, request.Stars, request.Comment),
            context.CancellationToken);

        return GrpcMapper.ToGrpcResponse(ratingId);
    }
}