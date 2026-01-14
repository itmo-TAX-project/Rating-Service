using RatingService.Api.Grpc;
using SubjectType = Application.DTO.Enums.SubjectType;

namespace Presentation.Grpc.Mapper;

public static class GrpcMapper
{
    public static AddRatingResponse ToGrpcResponse(
        long id)
    {
        return new AddRatingResponse
        {
            Id = id,
        };
    }

    public static SubjectType MapSubjectType(RatingService.Api.Grpc.SubjectType type)
    {
        return type switch
        {
            RatingService.Api.Grpc.SubjectType.Driver => SubjectType.Driver,
            RatingService.Api.Grpc.SubjectType.Passenger => SubjectType.Passenger,
            RatingService.Api.Grpc.SubjectType.Unknown => SubjectType.Unknown,
            _ => SubjectType.Unknown,
        };
    }
}