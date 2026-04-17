namespace HelloWorld.Models.Greetings;

public readonly record struct GreetingId(Guid Value)
{
    public static GreetingId New() => new(Guid.NewGuid());

    public static GreetingId From(Guid value) => value == Guid.Empty
        ? throw new ArgumentException("Greeting ID cannot be empty.", nameof(value))
        : new(value);
}
