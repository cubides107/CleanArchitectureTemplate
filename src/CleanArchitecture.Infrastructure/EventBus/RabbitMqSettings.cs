namespace CleanArchitecture.Infrastructure.EventBus;

internal sealed record RabbitMqSettings(string Host, string Username = "guest", string Password = "guest");
