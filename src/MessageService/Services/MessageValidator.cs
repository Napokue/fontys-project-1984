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

    public async Task ValidateMessage(IMessage message, IRule[] rules)
    {
        var words = message.Content.Split(' ').ToList();
        var newWords = new List<string>(words.Count);
        
        foreach (var word in words)
        {
            var newWord = word;
            foreach (var rule in rules)
            {
                newWord = rule.ValidateWord(newWord);
            }
            newWords.Add(newWord);
        }

        await _textMessageRepository.AddAsync(new TextMessage(string.Join(' ', newWords)));
    }
}