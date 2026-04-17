using HelloWorld.Application.Abstractions;
using HelloWorld.Models.Greetings;

namespace HelloWorld.Application.Greetings.Queries;

public sealed record GetGreetingByIdQuery(Guid Id);

public sealed class GetGreetingByIdQueryHandler
    : IQueryHandler<GetGreetingByIdQuery, GreetingResponse>
{
    private readonly IGreetingRepository _repository;

    public GetGreetingByIdQueryHandler(IGreetingRepository repository)
    {
        _repository = repository;
    }

    public async Task<GreetingResponse> HandleAsync(
        GetGreetingByIdQuery query, CancellationToken ct = default)
    {
        var greeting = await _repository.GetByIdAsync(GreetingId.From(query.Id), ct)
            ?? throw new KeyNotFoundException($"Greeting with ID '{query.Id}' not found.");

        return GreetingMapper.ToResponse(greeting);
    }
}
