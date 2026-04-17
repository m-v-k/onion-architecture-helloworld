using HelloWorld.Application.Abstractions;
using HelloWorld.Models.Greetings;
using HelloWorld.Models.ValueObjects;

namespace HelloWorld.Application.Greetings.Commands;

public sealed record CreateGreetingCommand(
    string Target,
    string Message,
    string Language,
    bool IsFormal = false);

public sealed class CreateGreetingCommandHandler
    : ICommandHandler<CreateGreetingCommand, GreetingResponse>
{
    private readonly IGreetingRepository _repository;

    public CreateGreetingCommandHandler(IGreetingRepository repository)
    {
        _repository = repository;
    }

    public async Task<GreetingResponse> HandleAsync(
        CreateGreetingCommand command, CancellationToken ct = default)
    {
        var target = PersonName.Create(command.Target);
        var message = GreetingMessage.Create(command.Message);
        var language = Language.FromCode(command.Language);

        var greeting = Greeting.Create(target, message, language, command.IsFormal);

        await _repository.AddAsync(greeting, ct);

        return GreetingMapper.ToResponse(greeting);
    }
}
