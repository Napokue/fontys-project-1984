using MessageService.Models;

namespace MessageService.Factories;

public interface IMessageFactory
{
    public Task<IMessage> Create(string content);
}