using MessageService.Factories;

namespace MessageService.Models.Rules;

public class UppercaseSpecificWordBaseRule : AbstractRule
{
    private readonly string _specificWord;
    
    public UppercaseSpecificWordBaseRule(IMessageFactory messageFactory, string specificWord) 
        : base(messageFactory)
    {
        _specificWord = specificWord.ToUpper();
    }
    
    public override Task<IMessage> ValidateMessage(IMessage message)
    {
        var newMessage = MessageFactory.Create(message.Content.Replace(_specificWord, _specificWord,
            StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(newMessage);
    }
}