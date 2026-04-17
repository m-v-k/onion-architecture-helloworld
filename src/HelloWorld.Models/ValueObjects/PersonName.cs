namespace HelloWorld.Models.ValueObjects;

public sealed record PersonName
{
    public string Value { get; }

    private PersonName(string value) => Value = value;

    public static PersonName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Person name cannot be empty.", nameof(value));

        var trimmed = value.Trim();

        if (trimmed.Length > 100)
            throw new ArgumentException("Person name cannot exceed 100 characters.", nameof(value));

        return new PersonName(trimmed);
    }

    public override string ToString() => Value;
}
