namespace CleanArchitecture.Worker.EventBus;

internal sealed record RabbitMqSettings(string Host, string Username = "guest", string Password = "guest");
