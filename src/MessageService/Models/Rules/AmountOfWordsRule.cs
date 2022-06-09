using System.Text;
using System.Text.RegularExpressions;
using MessageService.Factories;

namespace MessageService.Models.Rules;

public class AmountOfWordsRule : AbstractRule
{
    private const string SentenceDelimiter = ".";
    private const string WordDelimiter = " ";
    
    private readonly int _maximumAllowedWords;
    
    public AmountOfWordsRule(IMessageFactory messageFactory, int maximumAllowedWords) : base(messageFactory)
    {
        _maximumAllowedWords = maximumAllowedWords;
    }

    public override IMessage ValidateMessage(IMessage message)
    {
        var sentences = SplitInto(message.Content, SentenceDelimiter).ToList();

        if (sentences.Count == 0)
        {
            return message;
        }

        if (sentences.Count == 1)
        {
            var words = SplitInto(sentences[0], WordDelimiter).ToList();
            return words.Count > _maximumAllowedWords 
                ? MessageFactory.Create(string.Empty) 
                : message;
        }

        var currentCount = 0;
        var contentBuilder = new StringBuilder();
        foreach (var sentence in sentences)
        {
            var words = SplitInto(sentence, WordDelimiter).ToList();

            if (words.Count + currentCount > _maximumAllowedWords)
            {
                break;
            }

            contentBuilder.Append(sentence);
            currentCount += words.Count;
        }

        return new TextMessage(contentBuilder.ToString());
    }

    private IEnumerable<string> SplitInto(string input, string delimiter) =>
        Regex.Split(input, @$"(?<=[{delimiter}])", RegexOptions.IgnorePatternWhitespace)
            .Where(str => !string.IsNullOrWhiteSpace(str));
}