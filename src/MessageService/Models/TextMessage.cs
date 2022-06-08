namespace MessageService.Models;

internal sealed class TextMessage : IMessage
{
    public Guid Id { get; set; }
    public string Content { get; set; }

    public TextMessage(string content)
    {
        Content = content;
    }
}