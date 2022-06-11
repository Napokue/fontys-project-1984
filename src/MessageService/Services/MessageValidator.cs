using MessageService.Models;
using MessageService.Models.Rules;
using MessageService.Repository;

namespace MessageService.Services;

public class MessageValidator : IMessageValidator
{
    private readonly TextMessageRepository _textMessageRepository;

    public MessageValidator(TextMessageRepository textMessageRepository)
    {
        _textMessageRepository = textMessageRepository;
    }

    public async Task ValidateMessage(IMessage message, AbstractRule[] rules)
    {
        message = rules.Aggregate(message, (current, rule) => 
            rule.ValidateMessage(current).GetAwaiter().GetResult());
        await _textMessageRepository.AddAsync((TextMessage) message);
    }
}