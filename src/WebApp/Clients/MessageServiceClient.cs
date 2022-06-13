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
    
    public async Task<IEnumerable<MessageModel>?> GetMessagesAsync(int skip = 0, int take = 10) =>
        await _client.GetFromJsonAsync<IEnumerable<MessageModel>>($"/all?skip={skip}&take={take}");
}