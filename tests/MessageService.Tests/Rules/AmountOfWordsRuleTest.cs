using System.Text;
using MessageService.Factories;
using MessageService.Models.Rules;
using NUnit.Framework;

namespace MessageService.Tests.Rules;

[TestFixture]
public class AmountOfWordsRuleTest
{
    private const int MaximumAllowedWords = 5;
    private TextMessageFactory _textMessageFactory;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _textMessageFactory = new TextMessageFactory();
    }

    [Test]
    public async Task TestNoSentence()
    {
        var rule = new AmountOfWordsRule(_textMessageFactory, MaximumAllowedWords);
        
        var validatedMessage = await rule.ValidateMessage(_textMessageFactory.Create(string.Empty));
        Assert.IsTrue(validatedMessage.Content == string.Empty);
    }
    
    [Test]
    public async Task TestSingleTooLongSentence()
    {
        var rule = new AmountOfWordsRule(_textMessageFactory, MaximumAllowedWords);

        var sentence = GenerateSentence(1, MaximumAllowedWords + 1);

        var validatedMessage = await rule.ValidateMessage(_textMessageFactory.Create(sentence));
        Assert.IsTrue(validatedMessage.Content == string.Empty);
    }

    [Test]
    public async Task TestSingleSentence()
    {
        var rule = new AmountOfWordsRule(_textMessageFactory, MaximumAllowedWords);

        var sentence = GenerateSentence(1, MaximumAllowedWords);

        var validatedMessage = await rule.ValidateMessage(_textMessageFactory.Create(sentence));
        Assert.IsTrue(sentence == validatedMessage.Content);
    }

    [Test]
    public async Task TestMultipleSentencesTooLong()
    {
        var rule = new AmountOfWordsRule(_textMessageFactory, MaximumAllowedWords);
        var sentence = GenerateSentence(MaximumAllowedWords + 1, 1);

        var validatedMessage = await rule.ValidateMessage(_textMessageFactory.Create(sentence));

        var sentenceWordCount = sentence.Split(".").Length - 1;
        var validatedMessageWordCount = validatedMessage.Content.Split(".").Length;
        Assert.IsTrue(sentenceWordCount == validatedMessageWordCount);
    }

    [Test]
    public async Task TestMultipleSentences()
    {
        var rule = new AmountOfWordsRule(_textMessageFactory, MaximumAllowedWords);
        var sentence = GenerateSentence(MaximumAllowedWords, 1);

        var validatedMessage = await rule.ValidateMessage(_textMessageFactory.Create(sentence));

        var sentenceWordCount = sentence.Split(".").Length;
        var validatedMessageWordCount = validatedMessage.Content.Split(".").Length;
        Assert.IsTrue(sentenceWordCount == validatedMessageWordCount);
    }

    /// <summary>
    /// Generate specified number of sentences with a specified amount of words
    /// </summary>
    /// <param name="sentences">Amount of sentences that need to be generated.</param>
    /// <param name="words">Amount of words that need to be generated.</param>
    /// <returns></returns>
    private string GenerateSentence(int sentences, int words)
    {
        var output = new StringBuilder();
        var sentenceBuilder = new StringBuilder();
        
        for (var i = 0; i < sentences; i++)
        {
            sentenceBuilder.Clear();
            
            for (var j = 0; j < words; j++)
            {
                sentenceBuilder.Append($"{RandomWord()}");
                
                if (j < words - 1)
                {
                    sentenceBuilder.Append(' ');
                }
            }

            sentenceBuilder.Append('.');

            if (i < sentences - 1)
            {
                sentenceBuilder.Append(' ');
            }
            
            output.Append(sentenceBuilder.ToString());
        }

        return output.ToString();
    }

    public static string RandomWord(int min = 3, int max = 5)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var length = GenerateRandomInt(min, max);
        var chars = new char[length];
            
        for (var i = 0; i < length; i++)
        {
            chars[i] = characters[GenerateRandomInt(0, characters.Length)];
        }

        return new string(chars);
    }
    
    private static int GenerateRandomInt(int min = 3, int max = 20)
    {
        return Random.Shared.Next(min, max);
    }
}