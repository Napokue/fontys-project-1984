using MessageService.Models;

namespace MessageService.Factories;

public class TextMessageFactory : IMessageFactory
{
    public IMessage Create(string content)
    {
        return new TextMessage(content);
    }
}