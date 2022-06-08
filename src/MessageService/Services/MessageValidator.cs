using DatabaseLib;
using MessageService.Models;
using MessageService.Models.Rules;

namespace MessageService.Services;

public class MessageValidator : IMessageValidator
{
    private readonly IRepository<TextMessage> _textMessageRepository;

    public MessageValidator(IRepository<TextMessage> textMessageRepository)
    {
        _textMessageRepository = textMessageRepository;
    }

    public void ValidateMessage(IMessage message, IRule[] rules)
    {
        var words = message.Content.Split(' ').ToList();

        foreach (var word in words)
        {
            foreach (var rule in rules)
            {
                rule.ValidateWord(word);
            }
        }

        _textMessageRepository.UpdateAsync(new TextMessage(message.Content));
    }
}