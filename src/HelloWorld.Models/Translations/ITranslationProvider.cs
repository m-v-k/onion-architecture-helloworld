using HelloWorld.Models.ValueObjects;

namespace HelloWorld.Models.Translations;

// This interface lives in the Models layer because translation
// is domain logic that requires external data.
// The implementation lives in the DataAccess layer.
public interface ITranslationProvider
{
    Task<GreetingMessage> TranslateAsync(
        GreetingMessage message,
        Language from,
        Language to,
        CancellationToken ct = default);
}
