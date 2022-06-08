namespace MessageService.Models.Rules;

public interface IRule
{
    public string ValidateWord(string word);
}