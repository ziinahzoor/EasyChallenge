using System.Text.Json;

namespace TwistedFizzBuzz;

using TokenDictionary = Dictionary<int, string>;

public class ApiFizzBuzzGenerator : FizzBuzzGenerator
{
    private static readonly string _apiUrl = "https://rich-red-cocoon-veil.cyclic.app/";

    private ApiFizzBuzzGenerator(TokenDictionary tokens) : base(tokens) { }

    public static async Task<ApiFizzBuzzGenerator> CreateAsync(HttpClient httpClient)
    {
        HttpResponseMessage response = await httpClient.GetAsync(_apiUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Can't create generator: Failed to retrieve tokens from API. Status code: {response.StatusCode}");
        }

        string jsonString = await response.Content.ReadAsStringAsync();

        TokenDictionary tokens = JsonSerializer.Deserialize<TokenDictionary>(jsonString)!;

        return new(tokens);
    }
}
