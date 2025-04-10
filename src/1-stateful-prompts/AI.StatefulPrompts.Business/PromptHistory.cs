using System.Collections.Immutable;
using Akka.Actor;
using Microsoft.Extensions.AI;

namespace AI.StatefulPrompts.Business;

/// <summary>
/// An immutable record that represents the history of a prompt.
/// </summary>
/// <param name="PromptTitle">The title, to be assigned later.</param>
/// <param name="CreatedAt">Start of the prompt history.</param>
/// <param name="Messages"></param>
public sealed record PromptHistory(string PromptTitle, DateTimeOffset CreatedAt, ImmutableStack<ChatMessage> Messages)
{
    public DateTimeOffset LastUpdatedAt { get; } = DateTimeOffset.UtcNow;
}

public sealed class PromptHistoryActor : UntypedActor
{
    private IActorRef _signalRMessagingActor;
    
    protected override void OnReceive(object message)
    {
        throw new NotImplementedException();
    }
}