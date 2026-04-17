using HelloWorld.Application.Abstractions;

namespace HelloWorld.Application.Greetings.Queries;

public sealed record GetAllGreetingsQuery;

public sealed class GetAllGreetingsQueryHandler
    : IQueryHandler<GetAllGreetingsQuery, IReadOnlyList<GreetingResponse>>
{
    private readonly IGreetingRepository _repository;

    public GetAllGreetingsQueryHandler(IGreetingRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<GreetingResponse>> HandleAsync(
        GetAllGreetingsQuery query, CancellationToken ct = default)
    {
        var greetings = await _repository.GetAllAsync(ct);
        return GreetingMapper.ToResponseList(greetings);
    }
}
