namespace HelloWorld.Models.ValueObjects;

public sealed record GreetingMessage
{
    public string Value { get; }

    private GreetingMessage(string value) => Value = value;

    public static GreetingMessage Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Greeting message cannot be empty.", nameof(value));

        var trimmed = value.Trim();

        if (trimmed.Length > 250)
            throw new ArgumentException("Greeting message cannot exceed 250 characters.", nameof(value));

        return new GreetingMessage(trimmed);
    }

    public override string ToString() => Value;
}
