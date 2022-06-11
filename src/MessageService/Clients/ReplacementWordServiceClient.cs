using MessageService.Models;

namespace MessageService.Clients;

public class ReplacementWordServiceClient
{
    private readonly HttpClient _client;

    public ReplacementWordServiceClient(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("http://replacement-words-service:80/");
    }
    
    public async Task<List<ReplacementWord>?> GetAllAsync()
    {
        var response = await _client.GetAsync($"/all");
        if (!response.IsSuccessStatusCode)
        {
            // TODO Error handling can be better
            throw new Exception();
        }
        return await response.Content.ReadFromJsonAsync<List<ReplacementWord>>();
    }
}