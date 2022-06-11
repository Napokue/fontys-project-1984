using MessageService.Clients;
using MessageService.Factories;

namespace MessageService.Models.Rules;

public class ReplacementWordRule : AbstractRule
{
    private readonly ReplacementWordsServiceClient _replacementWordsServiceClient;

    public ReplacementWordRule(
        IMessageFactory messageFactory,
        ReplacementWordsServiceClient replacementWordsServiceClient) 
        : base(messageFactory)
    {
        _replacementWordsServiceClient = replacementWordsServiceClient;
    }
    
    public override async Task<IMessage> ValidateMessage(IMessage message)
    {
        var replacementWords = await _replacementWordsServiceClient.GetAllAsync();

        var content = replacementWords!.Aggregate(message.Content,
            (current, replacementWord) => current.Replace(
                replacementWord.Oldspeak, 
                replacementWord.Newspeak));
        return MessageFactory.Create(content);
    }
}