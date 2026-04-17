using HelloWorld.Models.Greetings;
using HelloWorld.Models.ValueObjects;
using Riok.Mapperly.Abstractions;

namespace HelloWorld.Application.Greetings;

[Mapper]
public static partial class GreetingMapper
{
    public static GreetingResponse ToResponse(Greeting greeting)
    {
        var response = MapToResponse(greeting);
        return response with { FormattedGreeting = greeting.FormatGreeting() };
    }

    public static IReadOnlyList<GreetingResponse> ToResponseList(IEnumerable<Greeting> greetings)
        => greetings.Select(ToResponse).ToList();

    [MapperIgnoreTarget(nameof(GreetingResponse.FormattedGreeting))]
    private static partial GreetingResponse MapToResponse(Greeting greeting);

    private static string PersonNameToString(PersonName name) => name.Value;
    private static string GreetingMessageToString(GreetingMessage message) => message.Value;
    private static string LanguageToString(Language language) => language.Code;
    private static Guid GreetingIdToGuid(GreetingId id) => id.Value;
}
