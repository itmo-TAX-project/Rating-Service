namespace Application.DTO;

public class GetRatingDto
{
    public long SubjectId { get; init; }

    public double Average { get; init; }

    public long Count { get; init; }
}