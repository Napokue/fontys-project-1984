using MessageService.Models;
using MessageService.Models.Rules;

namespace MessageService.Services;

public interface IMessageValidator
{
    public void ValidateMessage(IMessage message, IRule[] rules);
}