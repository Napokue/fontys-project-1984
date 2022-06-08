using MessageService.Models;

namespace MessageService.Factories;

public interface IMessageFactory
{
    public IMessage Create(string content);
}