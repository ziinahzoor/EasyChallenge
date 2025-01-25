using System.Text.RegularExpressions;

namespace TwistedFizzBuzz;
using TokenDictionary = Dictionary<int, string>;

public class DefaultFizzBuzzGenerator : FizzBuzzGenerator
{
    private DefaultFizzBuzzGenerator(TokenDictionary tokens) : base(tokens) { }

    public static DefaultFizzBuzzGenerator Create(TokenDictionary? tokens = null)
    {
        TokenDictionary constructorTokens = tokens ?? new()
        {
            { 3, "Fizz" },
            { 5, "Buzz" }
        };

        return new DefaultFizzBuzzGenerator(constructorTokens);
    }
}
