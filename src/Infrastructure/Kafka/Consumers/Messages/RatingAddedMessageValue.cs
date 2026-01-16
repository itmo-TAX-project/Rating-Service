using Application.DTO.Enums;

namespace Infrastructure.Kafka.Consumers.Messages;

public class RatingAddedMessageValue
{
    public SubjectType SubjectType { get; set; }

    public long SubjectId { get; set; }

    public long RaterId { get; set; }

    public int Stars { get; set; }

    public string? Comment { get; set; }
}