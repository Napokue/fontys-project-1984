namespace MessageService.Models.Rules;

public class LowercaseRule : IRule
{
    public string ValidateWord(string word) =>
        string.Equals(word, word.ToUpper(), StringComparison.Ordinal) 
            ? word.ToLower() 
            : word;
}