
namespace TwistedFizzBuzz;

using System.Text.RegularExpressions;
using TokenDictionary = Dictionary<int, string>;

public abstract class FizzBuzzGenerator(TokenDictionary tokens) : IFizzBuzzGenerator
{
    private readonly TokenDictionary _tokens = tokens;

    public IEnumerable<string> Generate(string range)
    {
        Match match = Regex.Match(range, @"^(\(-?\d+\)|\d+)-(\(-?\d+\)|\d+)$");

        if (!match.Success)
        {
            throw new ArgumentException("Invalid range format. Expected format: start-end, with optional negative numbers inside parenthesis.");
        }

        string startText = match.Groups[1].Value.Trim('(', ')');
        string endText = match.Groups[2].Value.Trim('(', ')');

        int start = int.Parse(startText);
        int end = int.Parse(endText);

        return Generate(start, end);
    }

    public IEnumerable<string> Generate(int start, int end)
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

        return Generate(range);
    }

    public IEnumerable<string> Generate(IEnumerable<int> numbers)
    {
        return numbers.Select(number => GetTokenValue(number));
    }

    private string GetTokenValue(int number)
    {
        IEnumerable<string> valueTokens = _tokens
            .Where(t => number % t.Key == 0)
            .Select(t => t.Value);

        return valueTokens.Any()
            ? string.Join(string.Empty, valueTokens)
            : number.ToString();
    }
}
