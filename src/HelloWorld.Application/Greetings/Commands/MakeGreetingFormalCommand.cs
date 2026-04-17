using HelloWorld.Application.Abstractions;
using HelloWorld.Models.Greetings;

namespace HelloWorld.Application.Greetings.Commands;

// Demonstrates domain logic WITHOUT data:
// The handler loads the entity, calls entity.MakeFormal() (pure domain logic),
// and persists the change. No external data is needed.
public sealed record MakeGreetingFormalCommand(Guid GreetingId);

public sealed class MakeGreetingFormalCommandHandler
    : ICommandHandler<MakeGreetingFormalCommand, GreetingResponse>
{
    private readonly IGreetingRepository _repository;

    public MakeGreetingFormalCommandHandler(IGreetingRepository repository)
    {
        _repository = repository;
    }

    public async Task<GreetingResponse> HandleAsync(
        MakeGreetingFormalCommand command, CancellationToken ct = default)
    {
        var greeting = await _repository.GetByIdAsync(GreetingId.From(command.GreetingId), ct)
            ?? throw new KeyNotFoundException($"Greeting with ID '{command.GreetingId}' not found.");

        greeting.MakeFormal();

        await _repository.UpdateAsync(greeting, ct);

        return GreetingMapper.ToResponse(greeting);
    }
}
