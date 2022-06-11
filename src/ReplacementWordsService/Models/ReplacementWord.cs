namespace ReplacementWordsService.Models;

public class ReplacementWord
{
    public Guid Id { get; set; }
    public string Oldspeak { get; set; }
    public string Newspeak { get; set; }
}