using MessageService.Factories;
using MessageService.Models.Rules;
using NUnit.Framework;

namespace MessageService.Tests.Rules;

[TestFixture]
public class LowercaseRuleTest
{
    [Test]
    public void TestUppercaseWord()
    {
        var lowercaseRule = new LowercaseRule(new TextMessageFactory());

        var word = new string('A', 5);
        var validateWord = lowercaseRule.ValidateMessage(word);
        Assert.IsTrue(word.ToLower() == validateWord);
    }

    [Test]
    public void TestLowercaseWord()
    {
        var lowercaseRule = new LowercaseRule(new TextMessageFactory());

        var word = new string('a', 5);
        var validateWord = lowercaseRule.ValidateMessage(word);
        Assert.IsTrue(word == validateWord);
    }
}