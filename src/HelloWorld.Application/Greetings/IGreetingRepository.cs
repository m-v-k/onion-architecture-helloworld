using HelloWorld.Models.Greetings;

namespace HelloWorld.Application.Greetings;

public interface IGreetingRepository
{
    Task<Greeting?> GetByIdAsync(GreetingId id, CancellationToken ct = default);
    Task<IReadOnlyList<Greeting>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Greeting greeting, CancellationToken ct = default);
    Task UpdateAsync(Greeting greeting, CancellationToken ct = default);
}
