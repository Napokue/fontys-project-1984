using MessageService.Clients;
using MessageService.Factories;

namespace MessageService.Models.Rules;

public class ReplacementWordRule : AbstractRule
{
    private readonly ReplacementWordServiceClient _replacementWordServiceClient;

    public ReplacementWordRule(
        IMessageFactory messageFactory,
        ReplacementWordServiceClient replacementWordServiceClient) 
        : base(messageFactory)
    {
        _replacementWordServiceClient = replacementWordServiceClient;
    }
    
    public override async Task<IMessage> ValidateMessage(IMessage message)
    {
        var replacementWords = await _replacementWordServiceClient.GetAllAsync();

        var content = replacementWords!.Aggregate(message.Content,
            (current, replacementWord) => current.Replace(
                replacementWord.Oldspeak, 
                replacementWord.Newspeak));
        return MessageFactory.Create(content);
    }
}