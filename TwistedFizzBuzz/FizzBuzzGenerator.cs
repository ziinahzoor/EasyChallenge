using System.Text.RegularExpressions;

namespace TwistedFizzBuzz;
using TokenDictionary = Dictionary<int, string>;

public class FizzBuzzGenerator
{
    private static readonly TokenDictionary defaultTokens = new()
    {
        { 3, "Fizz" },
        { 5, "Buzz" }
    };

    public static IEnumerable<string> Generate(string range, TokenDictionary? tokens = null)
    {
        var match = Regex.Match(range, @"^(\(-?\d+\)|\d+)-(\(-?\d+\)|\d+)$");

        if (!match.Success)
        {
            throw new ArgumentException("Invalid range format. Expected format: start-end, with optional negative numbers inside parenthesis.");
        }

        string startText = match.Groups[1].Value.Trim('(', ')');
        string endText = match.Groups[2].Value.Trim('(', ')');

        int start = int.Parse(startText);
        int end = int.Parse(endText);

        return Generate(start, end, tokens);
    }

    public static IEnumerable<string> Generate(int start, int end, TokenDictionary? tokens = null)
    {
        int numbersFromStart;
        IEnumerable<int> range;

        if (start > end)
        {
            numbersFromStart = start - end + 1;
            range = Enumerable.Range(end, numbersFromStart).Reverse();
        }
        else
        {
            numbersFromStart = end - start + 1;
            range = Enumerable.Range(start, numbersFromStart);
        }

        return Generate(range, tokens);
    }

    public static IEnumerable<string> Generate(IEnumerable<int> numbers, TokenDictionary? tokens = null)
    {
        tokens ??= defaultTokens;
        return numbers.Select(number => GetTokenValue(number, tokens));
    }

    private static string GetTokenValue(int number, TokenDictionary tokens)
    {
        var valueTokens = tokens.Where(t => number % t.Key == 0).Select(t => t.Value);

        return valueTokens.Any()
            ? string.Join(string.Empty, valueTokens)
            : number.ToString();
    }
}
