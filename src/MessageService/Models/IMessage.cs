namespace MessageService.Models;

public interface IMessage
{
    public Guid Id { get; }
    public string Content { get; }
}