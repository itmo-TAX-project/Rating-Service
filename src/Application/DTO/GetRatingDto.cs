namespace Application.DTO;

public class GetRatingDto
{
    public long SubjectId { get; init; }

    public decimal Average { get; init; }

    public long Count { get; init; }
}