using System.Text.Json.Serialization;

namespace ReplacementWordsService.Models;

public class ReplacementWord
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("oldspeak")]
    public string Oldspeak { get; set; }
    
    [JsonPropertyName("newspeak")]
    public string Newspeak { get; set; }
}