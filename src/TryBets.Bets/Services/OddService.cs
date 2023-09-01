using System.Net.Http;
namespace TryBets.Bets.Services;

public class OddService : IOddService
{
    private readonly HttpClient _client;
    public OddService(HttpClient client)
    {
        _client = client;
    }

    public async Task<object> UpdateOdd(int MatchId, int TeamId, decimal BetValue)
    {
        var response = await _client.GetAsync($"http://localhost:5504/{MatchId}/{TeamId}/{BetValue}");
        var updatedMatch = await response.Content.ReadAsStringAsync();
        return updatedMatch;
    }
}