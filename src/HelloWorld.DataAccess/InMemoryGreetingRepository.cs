using System.Collections.Concurrent;
using HelloWorld.Application.Greetings;
using HelloWorld.Models.Greetings;
using HelloWorld.Models.ValueObjects;

namespace HelloWorld.DataAccess;

public sealed class InMemoryGreetingRepository : IGreetingRepository
{
    private readonly ConcurrentDictionary<GreetingId, Greeting> _greetings = new();

    public InMemoryGreetingRepository()
    {
        SeedData();
    }

    public Task<Greeting?> GetByIdAsync(GreetingId id, CancellationToken ct = default)
    {
        _greetings.TryGetValue(id, out var greeting);
        return Task.FromResult(greeting);
    }

    public Task<IReadOnlyList<Greeting>> GetAllAsync(CancellationToken ct = default)
    {
        IReadOnlyList<Greeting> result = _greetings.Values.OrderBy(g => g.CreatedAt).ToList();
        return Task.FromResult(result);
    }

    public Task AddAsync(Greeting greeting, CancellationToken ct = default)
    {
        _greetings[greeting.Id] = greeting;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Greeting greeting, CancellationToken ct = default)
    {
        _greetings[greeting.Id] = greeting;
        return Task.CompletedTask;
    }

    private void SeedData()
    {
        var greetings = new[]
        {
            Greeting.Create(PersonName.Create("World"), GreetingMessage.Create("Hello"), Language.English),
            Greeting.Create(PersonName.Create("Wereld"), GreetingMessage.Create("Hallo"), Language.Dutch),
            Greeting.Create(PersonName.Create("le Monde"), GreetingMessage.Create("Bonjour"), Language.French, isFormal: true),
            Greeting.Create(PersonName.Create("Mundo"), GreetingMessage.Create("Hola"), Language.Spanish),
        };

        foreach (var greeting in greetings)
            _greetings[greeting.Id] = greeting;
    }
}
