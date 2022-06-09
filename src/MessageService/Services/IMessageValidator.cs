using MessageService.Models;
using MessageService.Models.Rules;

namespace MessageService.Services;

public interface IMessageValidator
{
    public Task ValidateMessage(IMessage message, AbstractRule[] rules);
}