using DatabaseLib;
using MessageService.Models;
using MessageService.Models.Rules;

namespace MessageService.Services;

public class MessageValidator : IMessageValidator
{
    private readonly IRepository<IMessage> _messageRepository;

    public MessageValidator(IRepository<IMessage> messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task ValidateMessage(IMessage message, AbstractRule[] rules)
    {
        IMessage messageNew; 
        foreach (var rule in rules)
        {
            messageNew = rule.ValidateMessage(message);
        }

        await _messageRepository.AddAsync(message);
    }
}