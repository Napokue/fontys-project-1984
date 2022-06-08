namespace MessageService.Models.Rules;

public class UppercaseSpecificWordRule : IRule
{
    private readonly string _specificWord;
    
    public UppercaseSpecificWordRule(string specificWord)
    {
        _specificWord = specificWord.ToUpper();
    }

    public string ValidateWord(string word) =>
        string.Equals(word, _specificWord, StringComparison.OrdinalIgnoreCase)
            ? _specificWord
            : word;
}