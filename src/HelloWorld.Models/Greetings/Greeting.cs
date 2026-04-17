using HelloWorld.Models.ValueObjects;

namespace HelloWorld.Models.Greetings;

public sealed class Greeting
{
    public GreetingId Id { get; }
    public PersonName Target { get; private set; }
    public GreetingMessage Message { get; private set; }
    public Language Language { get; private set; }
    public bool IsFormal { get; private set; }
    public DateTimeOffset CreatedAt { get; }

    public Greeting(
        GreetingId id,
        PersonName target,
        GreetingMessage message,
        Language language,
        bool isFormal,
        DateTimeOffset createdAt)
    {
        Id = id;
        Target = target;
        Message = message;
        Language = language;
        IsFormal = isFormal;
        CreatedAt = createdAt;
    }

    public static Greeting Create(
        PersonName target,
        GreetingMessage message,
        Language language,
        bool isFormal = false)
    {
        return new Greeting(GreetingId.New(), target, message, language, isFormal, DateTimeOffset.UtcNow);
    }

    // Domain logic WITHOUT data: formats the complete greeting string.
    // Formal: "Dear World, Hello." / Casual: "Hey World! Hello!"
    public string FormatGreeting()
    {
        return IsFormal
            ? $"Dear {Target.Value}, {Message.Value}."
            : $"Hey {Target.Value}! {Message.Value}!";
    }

    // Domain logic WITHOUT data: makes the greeting formal
    public void MakeFormal() => IsFormal = true;

    // Domain logic WITHOUT data: makes the greeting casual
    public void MakeCasual() => IsFormal = false;

    // Domain logic WITHOUT data: retargets the greeting to a different person
    public void RetargetTo(PersonName newTarget) => Target = newTarget;
}
