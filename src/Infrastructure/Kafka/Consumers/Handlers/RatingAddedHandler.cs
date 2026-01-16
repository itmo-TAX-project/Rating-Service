using Application.Contracts;
using Application.DTO;
using Infrastructure.Kafka.Consumers.Messages;
using Itmo.Dev.Platform.Kafka.Consumer;

namespace Infrastructure.Kafka.Consumers.Handlers;

public class RatingAddedHandler : IKafkaInboxHandler<RatingAddedMessageKey, RatingAddedMessageValue>
{
    private readonly IRatingService _service;

    public RatingAddedHandler(IRatingService service)
    {
        _service = service;
    }

    public async ValueTask HandleAsync(
        IEnumerable<IKafkaInboxMessage<RatingAddedMessageKey, RatingAddedMessageValue>> messages,
        CancellationToken cancellationToken)
    {
        foreach (IKafkaInboxMessage<RatingAddedMessageKey, RatingAddedMessageValue> message in messages)
        {
            RatingAddedMessageValue msg = message.Value;

            await _service.AddRatingAsync(
                new RatingDto(msg.SubjectType, msg.SubjectId, msg.RaterId, msg.Stars, msg.Comment),
                cancellationToken);
        }
    }
}