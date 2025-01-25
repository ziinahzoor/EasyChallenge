using System.Text.Json;

namespace TwistedFizzBuzz;

using TokenDictionary = Dictionary<int, string>;

public class ApiFizzBuzzGenerator(HttpClient httpClient)
{
    private static readonly string _apiUrl = "https://rich-red-cocoon-veil.cyclic.app/";
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IEnumerable<string>> Generate(string range)
    {
        TokenDictionary? tokens = await GetTokens();
        return FizzBuzzGenerator.Generate(range, tokens);
    }

    public async Task<IEnumerable<string>> Generate(int start, int end)
    {
        TokenDictionary? tokens = await GetTokens();
        return FizzBuzzGenerator.Generate(start, end, tokens);
    }

    public async Task<IEnumerable<string>> Generate(IEnumerable<int> numbers)
    {
        TokenDictionary? tokens = await GetTokens();
        return FizzBuzzGenerator.Generate(numbers, tokens);
    }

    private async Task<TokenDictionary?> GetTokens()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to retrieve tokens from API. Status code: {response.StatusCode}");
        }

        var jsonString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TokenDictionary>(jsonString);
    }
}
