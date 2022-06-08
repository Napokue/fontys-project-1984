using WebApp.Models;

namespace WebApp.Clients;

public class MessageServiceClient
{
    private readonly HttpClient _client;

    public MessageServiceClient(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("http://message-service:80/");
    }

    public async Task SendMessage(SendMessageModel sendMessageModel) =>
        await _client.PostAsJsonAsync("/", sendMessageModel.Content);
}