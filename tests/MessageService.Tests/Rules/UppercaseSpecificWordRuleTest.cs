using MessageService.Models.Rules;
using NUnit.Framework;

namespace MessageService.Tests.Rules;

[TestFixture]
public class UppercaseSpecificWordRuleTest
{
    private const string SpecificWord = "TestWord";

    [Test]
    public void TestWordAsSpecificWordLowercase()
    {
        var uppercaseSpecificWordRule = new UppercaseSpecificWordRule(SpecificWord);

        var word = SpecificWord;
        var validatedWord = uppercaseSpecificWordRule.ValidateWord(word);
        Assert.IsTrue(word.ToUpper() == validatedWord);
    }

    [Test]
    public void TestWordAsSpecificWordUppercase()
    {
        var uppercaseSpecificWordRule = new UppercaseSpecificWordRule(SpecificWord);

        var word = SpecificWord.ToUpper();
        var validatedWord = uppercaseSpecificWordRule.ValidateWord(word);
        Assert.IsTrue(word == validatedWord);
    }

    [Test]
    public void TestWordNotAsSpecificWord()
    {
        var uppercaseSpecificWordRule = new UppercaseSpecificWordRule(SpecificWord);

        var word = new string('a', 5);
        var validatedWord = uppercaseSpecificWordRule.ValidateWord(word);
        Assert.IsTrue(word == validatedWord);
    }
}