namespace TwistedFizzBuzz.Tests;
using TokenDictionary = Dictionary<int, string>;

public class FizzBuzzGeneratorTests
{
    private static readonly TokenDictionary _customTokens = new TokenDictionary()
    {
        { 2, "Two" },
        { 4, "Four" }
    };

    [Theory]
    [InlineData(1, 15, new[] { "1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz" })]
    [InlineData(15, 1, new[] { "FizzBuzz", "14", "13", "Fizz", "11", "Buzz", "Fizz", "8", "7", "Fizz", "Buzz", "4", "Fizz", "2", "1" })]
    public void ItShouldGenerateValuesWithIntegerInputs(int start, int end, string[] expected, TokenDictionary? tokens = null)
    {
        // Arrange & Act
        string[] result = FizzBuzzGenerator.Generate(start, end, tokens).ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ItShouldGenerateValuesWithIntegerInputsAndCustomTokens()
    {
        // Arrange
        int start = 1;
        int end = 8;
        string[] expected = ["1", "Two", "3", "TwoFour", "5", "Two", "7", "TwoFour"];

        // Act
        string[] result = FizzBuzzGenerator.Generate(start, end, _customTokens).ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1-15", new[] { "1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz" })]
    [InlineData("15-1", new[] { "FizzBuzz", "14", "13", "Fizz", "11", "Buzz", "Fizz", "8", "7", "Fizz", "Buzz", "4", "Fizz", "2", "1" })]
    [InlineData("(-5)-5", new[] { "Buzz", "-4", "Fizz", "-2", "-1", "FizzBuzz", "1", "2", "Fizz", "4", "Buzz" })]
    [InlineData("(-10)-(-5)", new[] { "Buzz", "Fizz", "-8", "-7", "Fizz", "Buzz" })]
    [InlineData("0-(-5)", new[] { "FizzBuzz", "-1", "-2", "Fizz", "-4", "Buzz" })]
    public void ItShouldGenerateValuesWithStringRangeInputs(string range, string[] expected, TokenDictionary? tokens = null)
    {
        // Arrange & Act
        string[] result = FizzBuzzGenerator.Generate(range, tokens).ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ItShouldGenerateValuesWithStringRangeInputsAndCustomTokens()
    {
        // Arrange
        string range = "1-8";
        string[] expected = ["1", "Two", "3", "TwoFour", "5", "Two", "7", "TwoFour"];

        // Act
        string[] result = FizzBuzzGenerator.Generate(range, _customTokens).ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ItShouldGenerateValuesNonSequentialInputs()
    {
        // Arrange
        int[] numbers = [2, 4, 5, 8, 10, 13, 15];
        string[] expected = ["2", "4", "Buzz", "8", "Buzz", "13", "FizzBuzz"];

        // Act
        string[] result = FizzBuzzGenerator.Generate(numbers).ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ItShouldGenerateValuesNonSequentialInputsAndCustomTokens()
    {
        // Arrange
        int[] numbers = [2, 4, 5, 8, 10, 13];
        string[] expected = ["Two", "TwoFour", "5", "TwoFour", "Two", "13"];

        // Act
        string[] result = FizzBuzzGenerator.Generate(numbers, _customTokens).ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ItShouldThrowInvalidRangeInputs()
    {
        // Arrange
        string range = "-15";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => FizzBuzzGenerator.Generate(range));
    }
}
