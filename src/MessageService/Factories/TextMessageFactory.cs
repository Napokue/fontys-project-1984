using DatabaseLib;
using MessageService.Models;

namespace MessageService.Factories;

public class TextMessageFactory : IMessageFactory
{
    private readonly IRepository<TextMessage> _textMessageRepository;

    public TextMessageFactory(IRepository<TextMessage> textMessageRepository)
    {
        _textMessageRepository = textMessageRepository;
    }

    public async Task<IMessage> Create(string content)
    {
        var textMessage = new TextMessage(content);
        await _textMessageRepository.AddAsync(textMessage);
        return textMessage;
    }

}