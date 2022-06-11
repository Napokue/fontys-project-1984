using MessageService.Factories;
using MessageService.Models.Rules;
using NUnit.Framework;

namespace MessageService.Tests.Rules;

[TestFixture]
public class UppercaseSpecificWordRuleTest
{
    private const string SpecificWord = "This is a test";

    [Test]
    public async Task TestWordAsSpecificWordLowercase()
    {
        var textMessageFactory = new TextMessageFactory();
        var uppercaseSpecificWordRule = new UppercaseSpecificWordBaseRule(textMessageFactory, SpecificWord);

        var originalMessage = textMessageFactory.Create(SpecificWord);
        var validateMessage = await uppercaseSpecificWordRule.ValidateMessage(originalMessage);
        Assert.IsTrue(originalMessage.Content.ToUpper() == validateMessage.Content);
    }

    [Test]
    public async Task TestWordAsSpecificWordUppercase()
    {
        var textMessageFactory = new TextMessageFactory();
        var uppercaseSpecificWordRule = new UppercaseSpecificWordBaseRule(textMessageFactory, SpecificWord);

        var originalMessage = textMessageFactory.Create(SpecificWord.ToUpper());
        var validatedMessage = await uppercaseSpecificWordRule.ValidateMessage(originalMessage);
        Assert.IsTrue(originalMessage.Content == validatedMessage.Content);
    }

    [Test]
    public async Task TestWordNotAsSpecificWord()
    {
        var textMessageFactory = new TextMessageFactory();
        var uppercaseSpecificWordRule = new UppercaseSpecificWordBaseRule(textMessageFactory, SpecificWord);

        var testWord = new string('a', 5);
        var originalMessage = textMessageFactory.Create($"{testWord} {SpecificWord}");
        var validatedMessage = await uppercaseSpecificWordRule.ValidateMessage(originalMessage);
        Assert.IsTrue($"{testWord} {SpecificWord.ToUpper()}" == validatedMessage.Content);
    }
}