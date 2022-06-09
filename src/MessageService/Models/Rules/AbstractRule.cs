using MessageService.Factories;

namespace MessageService.Models.Rules;

public abstract class AbstractRule
{
    protected readonly IMessageFactory MessageFactory;
    
    protected AbstractRule(IMessageFactory messageFactory)
    {
        MessageFactory = messageFactory;
    }

    public abstract IMessage ValidateMessage(IMessage message);
}