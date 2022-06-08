using MessageService.Models;
using MessageService.Models.Rules;

namespace MessageService.Services;

public class MessageValidator : IMessageValidator
{
    public void ValidateMessage(IMessage message, IRule[] rules)
    {
        throw new NotImplementedException();
    }
}