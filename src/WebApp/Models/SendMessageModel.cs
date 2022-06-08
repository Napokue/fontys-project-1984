using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class SendMessageModel
{
    [Required]
    public string? Content { get; set; }
}