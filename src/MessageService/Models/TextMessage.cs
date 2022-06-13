namespace MessageService.Models;

public sealed class TextMessage : IMessage
{
    public Guid Id { get; set; }
    public string Content { get; set; }

    public TextMessage()
    {
    }

    public TextMessage(string content)
    {
        Content = content;
    }
}