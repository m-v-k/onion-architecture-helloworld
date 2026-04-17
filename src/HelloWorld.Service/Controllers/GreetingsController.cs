using HelloWorld.Application.Abstractions;
using HelloWorld.Application.Greetings;
using HelloWorld.Application.Greetings.Commands;
using HelloWorld.Application.Greetings.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GreetingsController : ControllerBase
{
    private readonly IQueryHandler<GetAllGreetingsQuery, IReadOnlyList<GreetingResponse>> _getAllHandler;
    private readonly IQueryHandler<GetGreetingByIdQuery, GreetingResponse> _getByIdHandler;
    private readonly ICommandHandler<CreateGreetingCommand, GreetingResponse> _createHandler;
    private readonly ICommandHandler<MakeGreetingFormalCommand, GreetingResponse> _makeFormalHandler;
    private readonly ICommandHandler<MakeGreetingCasualCommand, GreetingResponse> _makeCasualHandler;
    private readonly ICommandHandler<TranslateGreetingCommand, GreetingResponse> _translateHandler;

    public GreetingsController(
        IQueryHandler<GetAllGreetingsQuery, IReadOnlyList<GreetingResponse>> getAllHandler,
        IQueryHandler<GetGreetingByIdQuery, GreetingResponse> getByIdHandler,
        ICommandHandler<CreateGreetingCommand, GreetingResponse> createHandler,
        ICommandHandler<MakeGreetingFormalCommand, GreetingResponse> makeFormalHandler,
        ICommandHandler<MakeGreetingCasualCommand, GreetingResponse> makeCasualHandler,
        ICommandHandler<TranslateGreetingCommand, GreetingResponse> translateHandler)
    {
        _getAllHandler = getAllHandler;
        _getByIdHandler = getByIdHandler;
        _createHandler = createHandler;
        _makeFormalHandler = makeFormalHandler;
        _makeCasualHandler = makeCasualHandler;
        _translateHandler = translateHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GreetingResponse>>> GetAll(CancellationToken ct)
    {
        var result = await _getAllHandler.HandleAsync(new GetAllGreetingsQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GreetingResponse>> GetById(Guid id, CancellationToken ct)
    {
        var result = await _getByIdHandler.HandleAsync(new GetGreetingByIdQuery(id), ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GreetingResponse>> Create(
        [FromBody] CreateGreetingCommand command, CancellationToken ct)
    {
        var result = await _createHandler.HandleAsync(command, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}/formalize")]
    public async Task<ActionResult<GreetingResponse>> MakeFormal(Guid id, CancellationToken ct)
    {
        var result = await _makeFormalHandler.HandleAsync(new MakeGreetingFormalCommand(id), ct);
        return Ok(result);
    }

    [HttpPut("{id:guid}/casualize")]
    public async Task<ActionResult<GreetingResponse>> MakeCasual(Guid id, CancellationToken ct)
    {
        var result = await _makeCasualHandler.HandleAsync(new MakeGreetingCasualCommand(id), ct);
        return Ok(result);
    }

    [HttpPost("{id:guid}/translate")]
    public async Task<ActionResult<GreetingResponse>> Translate(
        Guid id, [FromBody] TranslateRequest request, CancellationToken ct)
    {
        var command = new TranslateGreetingCommand(id, request.TargetLanguage);
        var result = await _translateHandler.HandleAsync(command, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
}

public sealed record TranslateRequest(string TargetLanguage);
