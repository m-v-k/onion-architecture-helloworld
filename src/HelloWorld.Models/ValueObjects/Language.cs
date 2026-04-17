namespace HelloWorld.Models.ValueObjects;

public sealed record Language
{
    private static readonly Dictionary<string, string> SupportedLanguages = new()
    {
        ["en"] = "English",
        ["nl"] = "Dutch",
        ["fr"] = "French",
        ["es"] = "Spanish",
        ["de"] = "German",
    };

    public static readonly Language English = new("en");
    public static readonly Language Dutch = new("nl");
    public static readonly Language French = new("fr");
    public static readonly Language Spanish = new("es");
    public static readonly Language German = new("de");

    public string Code { get; }
    public string DisplayName => SupportedLanguages[Code];

    private Language(string code) => Code = code;

    public static Language FromCode(string code)
    {
        var normalized = code.ToLowerInvariant();

        if (!SupportedLanguages.ContainsKey(normalized))
            throw new ArgumentException(
                $"Unsupported language: '{code}'. Supported: {string.Join(", ", SupportedLanguages.Keys)}");

        return new Language(normalized);
    }

    public override string ToString() => $"{DisplayName} ({Code})";
}
