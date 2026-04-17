using HelloWorld.Application.Abstractions;
using HelloWorld.Models.Greetings;

namespace HelloWorld.Application.Greetings.Commands;

public sealed record MakeGreetingCasualCommand(Guid GreetingId);

public sealed class MakeGreetingCasualCommandHandler
    : ICommandHandler<MakeGreetingCasualCommand, GreetingResponse>
{
    private readonly IGreetingRepository _repository;

    public MakeGreetingCasualCommandHandler(IGreetingRepository repository)
    {
        _repository = repository;
    }

    public async Task<GreetingResponse> HandleAsync(
        MakeGreetingCasualCommand command, CancellationToken ct = default)
    {
        var greeting = await _repository.GetByIdAsync(GreetingId.From(command.GreetingId), ct)
            ?? throw new KeyNotFoundException($"Greeting with ID '{command.GreetingId}' not found.");

        greeting.MakeCasual();

        await _repository.UpdateAsync(greeting, ct);

        return GreetingMapper.ToResponse(greeting);
    }
}
