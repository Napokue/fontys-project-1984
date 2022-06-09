using MessageService.Factories;

namespace MessageService.Models.Rules;

public class AmountOfWordsAbstractRule : AbstractRule
{
    public AmountOfWordsAbstractRule(IMessageFactory messageFactory) : base(messageFactory)
    {
    }

    public override IMessage ValidateMessage(IMessage message)
    {
        throw new NotImplementedException();
    }
}