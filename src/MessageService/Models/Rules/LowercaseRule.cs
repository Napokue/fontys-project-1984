using MessageService.Factories;

namespace MessageService.Models.Rules;

public class LowercaseRule : AbstractRule
{
    public LowercaseRule(IMessageFactory messageFactory) : base(messageFactory)
    {
    }
    
    public string ValidateMessage(string word) =>
        string.Equals(word, word.ToUpper(), StringComparison.Ordinal) 
            ? word.ToLower() 
            : word;

    public override Task<IMessage> ValidateMessage(IMessage message)
    {
        return Task.FromResult(MessageFactory.Create(message.Content.ToLower()));
    }
}