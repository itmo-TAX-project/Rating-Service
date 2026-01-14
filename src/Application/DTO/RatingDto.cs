using Application.DTO.Enums;

namespace Application.DTO;

public class RatingDto
{
    public SubjectType SubjectType { get; private set; }

    public long SubjectId { get; private set; }

    public long RaterId { get; private set; }

    public int Stars { get; private set; }

    public string? Comment { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public RatingDto(
        SubjectType subjectType,
        long subjectId,
        long raterId,
        int stars,
        string? comment = null,
        DateTime? createdAt = null)
    {
        if (stars is < 1 or > 5)
            throw new Exception("fixme"); // todo

        SubjectType = subjectType;
        SubjectId = subjectId;
        RaterId = raterId;
        Stars = stars;
        Comment = comment;
        CreatedAt = createdAt ?? DateTime.Now;
    }
}