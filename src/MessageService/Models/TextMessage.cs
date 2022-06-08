namespace MessageService.Models;

internal sealed class TextMessage : IMessage
{
    public string Content { get; }

    public TextMessage(string content)
    {
        Content = content;
    }
}