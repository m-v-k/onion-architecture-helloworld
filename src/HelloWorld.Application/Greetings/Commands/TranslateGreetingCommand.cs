using HelloWorld.Application.Abstractions;
using HelloWorld.Models.Greetings;
using HelloWorld.Models.Translations;
using HelloWorld.Models.ValueObjects;

namespace HelloWorld.Application.Greetings.Commands;

// Demonstrates domain logic that REQUIRES DATA (translation lookup).
// The application layer orchestrates: loads the entity, calls the domain
// service (ITranslationProvider), and creates a new translated greeting.
public sealed record TranslateGreetingCommand(Guid GreetingId, string TargetLanguage);

public sealed class TranslateGreetingCommandHandler
    : ICommandHandler<TranslateGreetingCommand, GreetingResponse>
{
    private readonly IGreetingRepository _repository;
    private readonly ITranslationProvider _translationProvider;

    public TranslateGreetingCommandHandler(
        IGreetingRepository repository,
        ITranslationProvider translationProvider)
    {
        _repository = repository;
        _translationProvider = translationProvider;
    }

    public async Task<GreetingResponse> HandleAsync(
        TranslateGreetingCommand command, CancellationToken ct = default)
    {
        var greeting = await _repository.GetByIdAsync(GreetingId.From(command.GreetingId), ct)
            ?? throw new KeyNotFoundException($"Greeting with ID '{command.GreetingId}' not found.");

        var targetLanguage = Language.FromCode(command.TargetLanguage);

        // Domain logic WITH data: translation requires external data (the translation dictionary).
        // The ITranslationProvider interface is defined in Models, implemented in DataAccess.
        var translatedMessage = await _translationProvider.TranslateAsync(
            greeting.Message, greeting.Language, targetLanguage, ct);

        var translatedGreeting = Greeting.Create(
            greeting.Target, translatedMessage, targetLanguage, greeting.IsFormal);

        await _repository.AddAsync(translatedGreeting, ct);

        return GreetingMapper.ToResponse(translatedGreeting);
    }
}
