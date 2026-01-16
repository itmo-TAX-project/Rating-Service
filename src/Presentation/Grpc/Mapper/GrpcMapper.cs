using Application.DTO;
using RatingService.Api.Grpc;

namespace Presentation.Grpc.Mapper;

public static class GrpcMapper
{
    public static GetRatingResponse ToGetRatingGrpcResponse(GetRatingDto rating)
    {
        return new GetRatingResponse
        {
            Aggregate = new RatingAggregate
            {
                SubjectId = rating.SubjectId,
                Avg = rating.Average,
                Count = rating.Count,
            },
        };
    }
}