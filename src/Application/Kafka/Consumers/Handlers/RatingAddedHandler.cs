using Application.DTO;
using Application.Kafka.Consumers.Messages;
using Application.Services.Interfaces;
using Itmo.Dev.Platform.Kafka.Consumer;

namespace Application.Kafka.Consumers.Handlers;

public class RatingAddedHandler : IKafkaInboxHandler<long, RatingAddedMessage>
{
    private readonly IRatingService _service;

    public RatingAddedHandler(IRatingService service)
    {
        _service = service;
    }

    public async ValueTask HandleAsync(
        IEnumerable<IKafkaInboxMessage<long, RatingAddedMessage>> messages,
        CancellationToken cancellationToken)
    {
        foreach (IKafkaInboxMessage<long, RatingAddedMessage> message in messages)
        {
            RatingAddedMessage msg = message.Value;

            await _service.AddRatingAsync(
                new RatingDto(msg.SubjectType, msg.SubjectId, msg.RaterId, msg.Stars, msg.Comment),
                cancellationToken);
        }
    }
}