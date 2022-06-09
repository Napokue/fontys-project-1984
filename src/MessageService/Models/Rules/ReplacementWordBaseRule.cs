using MessageService.Factories;

namespace MessageService.Models.Rules;

public class ReplacementWordBaseRule : AbstractRule
{
    public ReplacementWordBaseRule(IMessageFactory messageFactory) : base(messageFactory)
    {
    }
    
    public override IMessage ValidateMessage(IMessage message)
    {
        throw new NotImplementedException();
    }
}