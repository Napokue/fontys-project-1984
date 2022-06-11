using System.Text.Json.Serialization;

namespace MessageService.Models;

public class ReplacementWord
{
    [JsonPropertyName("oldspeak")]
    public string Oldspeak { get; set; }
    
    [JsonPropertyName("newspeak")]
    public string Newspeak { get; set; }
}