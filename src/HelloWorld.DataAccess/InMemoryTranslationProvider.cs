using HelloWorld.Models.Translations;
using HelloWorld.Models.ValueObjects;

namespace HelloWorld.DataAccess;

public sealed class InMemoryTranslationProvider : ITranslationProvider
{
    private static readonly Dictionary<(string From, string To), Dictionary<string, string>> Translations = new()
    {
        [("en", "nl")] = new() { ["Hello"] = "Hallo", ["Goodbye"] = "Tot ziens", ["Welcome"] = "Welkom", ["Good morning"] = "Goedemorgen" },
        [("en", "fr")] = new() { ["Hello"] = "Bonjour", ["Goodbye"] = "Au revoir", ["Welcome"] = "Bienvenue", ["Good morning"] = "Bonjour" },
        [("en", "es")] = new() { ["Hello"] = "Hola", ["Goodbye"] = "Adiós", ["Welcome"] = "Bienvenido", ["Good morning"] = "Buenos días" },
        [("en", "de")] = new() { ["Hello"] = "Hallo", ["Goodbye"] = "Auf Wiedersehen", ["Welcome"] = "Willkommen", ["Good morning"] = "Guten Morgen" },
        [("nl", "en")] = new() { ["Hallo"] = "Hello", ["Tot ziens"] = "Goodbye", ["Welkom"] = "Welcome", ["Goedemorgen"] = "Good morning" },
        [("fr", "en")] = new() { ["Bonjour"] = "Hello", ["Au revoir"] = "Goodbye", ["Bienvenue"] = "Welcome" },
        [("es", "en")] = new() { ["Hola"] = "Hello", ["Adiós"] = "Goodbye", ["Bienvenido"] = "Welcome", ["Buenos días"] = "Good morning" },
        [("de", "en")] = new() { ["Hallo"] = "Hello", ["Auf Wiedersehen"] = "Goodbye", ["Willkommen"] = "Welcome", ["Guten Morgen"] = "Good morning" },
    };

    public Task<GreetingMessage> TranslateAsync(
        GreetingMessage message, Language from, Language to, CancellationToken ct = default)
    {
        if (from == to)
            return Task.FromResult(message);

        var key = (from.Code, to.Code);

        if (!Translations.TryGetValue(key, out var dictionary))
            throw new NotSupportedException(
                $"Translation from '{from.Code}' to '{to.Code}' is not supported.");

        if (!dictionary.TryGetValue(message.Value, out var translated))
            throw new KeyNotFoundException(
                $"No translation found for '{message.Value}' from '{from.Code}' to '{to.Code}'.");

        return Task.FromResult(GreetingMessage.Create(translated));
    }
}
