using WebApp.Models;

namespace WebApp.Clients;

public class ReplacementWordsServiceClient
{
    private readonly HttpClient _client;

    public ReplacementWordsServiceClient(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("http://replacement-words-service:80/");
    }
    
    public async Task<IEnumerable<ReplacementWordModel>?> GetReplacementWordsAsync(int skip = 0, int take = 10) =>
        await _client.GetFromJsonAsync<IEnumerable<ReplacementWordModel>>($"/all?skip={skip}&take={take}");

    public async Task CreateReplacementWordAsync(ReplacementWordModel replacementWordModel) =>
        await _client.PostAsJsonAsync("/", replacementWordModel);

    public async Task UpdateReplacementWordAsync(ReplacementWordModel replacementWordModel) =>
        await _client.PutAsJsonAsync("/", replacementWordModel);

    public async Task DeleteReplacementWordAsync(Guid id) =>
        await _client.DeleteAsync($"/{id}");
}