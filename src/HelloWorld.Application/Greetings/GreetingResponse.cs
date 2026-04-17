namespace HelloWorld.Application.Greetings;

public sealed record GreetingResponse
{
    public required Guid Id { get; init; }
    public required string Target { get; init; }
    public required string Message { get; init; }
    public required string Language { get; init; }
    public required bool IsFormal { get; init; }
    public string FormattedGreeting { get; init; } = string.Empty;
    public required DateTimeOffset CreatedAt { get; init; }
}
