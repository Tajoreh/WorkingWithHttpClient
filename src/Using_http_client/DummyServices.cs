namespace Using_http_client;

public sealed class DummyServices
{
    private readonly HttpClient _httpClient;

    public DummyServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<object> GetByIdAsync(string userId)
    {
        var content = await _httpClient.GetAsync($"/api/v1/employee/{userId}");

        return content;
    }
}