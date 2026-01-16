using Infrastructure.Kafka.Consumers.Handlers;
using Infrastructure.Kafka.Consumers.Messages;
using Itmo.Dev.Platform.Common.Extensions;
using Itmo.Dev.Platform.Kafka.Configuration;
using Itmo.Dev.Platform.Kafka.Extensions;
using Itmo.Dev.Platform.MessagePersistence;
using Itmo.Dev.Platform.MessagePersistence.Postgres.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class KafkaExtensions
{
    public static IServiceCollection AddKafka(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // https://github.com/itmo-is-dev/platform/wiki/Kafka:-Configuration
        services.AddPlatformKafka(builder => builder
            .ConfigureOptions(configuration.GetSection("Kafka"))
            .AddRatingAddedConsumer(configuration));

        return services;
    }

    public static IServiceCollection AddMessagePersistence(this IServiceCollection services)
    {
        services.AddUtcDateTimeProvider();
        services.AddSingleton(new Newtonsoft.Json.JsonSerializerSettings());

        services.AddPlatformMessagePersistence(builder => builder
            .WithDefaultPublisherOptions("MessagePersistence:Publisher:Default")
            .UsePostgresPersistence(configurator => configurator.ConfigureOptions("MessagePersistence")));

        return services;
    }

    private static IKafkaConfigurationBuilder AddRatingAddedConsumer(
        this IKafkaConfigurationBuilder builder,
        IConfiguration configuration)
    {
        return builder.AddConsumer(c => c
            .WithKey<RatingAddedMessageKey>()
            .WithValue<RatingAddedMessageValue>()
            .WithConfiguration(configuration.GetSection("Kafka:Consumers:RatingAddedMessage"))
            .DeserializeKeyWithNewtonsoft()
            .DeserializeValueWithNewtonsoft()
            .HandleInboxWith<RatingAddedHandler>());
    }
}