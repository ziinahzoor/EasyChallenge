using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace TwistedFizzBuzz.Tests;

public class ApiFizzBuzzGeneratorTests
{
    private readonly Mock<HttpMessageHandler> _httpHandlerMock;

    public ApiFizzBuzzGeneratorTests()
    {
        _httpHandlerMock = new Mock<HttpMessageHandler>();
        _httpHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"7\":\"Seven\", \"3\":\"Three\"}", System.Text.Encoding.UTF8, "application/json")
            });
    }

    [Fact]
    public async Task ItShouldGenerateValuesWithIntegerInputs()
    {
        // Arrange
        int start = 1;
        int end = 21;
        string[] expected = ["1", "2", "Three", "4", "5", "Three", "Seven", "8", "Three", "10", "11", "Three", "13", "Seven", "Three", "16", "17", "Three", "19", "20", "SevenThree"];
        HttpClient httpClient = new(_httpHandlerMock.Object);
        ApiFizzBuzzGenerator fizzBuzzGenerator = new(httpClient);

        // Act
        string[] result = (await fizzBuzzGenerator.Generate(start, end)).ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task ItShouldGenerateValuesWithStringRangeInputs()
    {
        // Arrange
        string range = "1-21";
        string[] expected = ["1", "2", "Three", "4", "5", "Three", "Seven", "8", "Three", "10", "11" ,"Three", "13", "Seven", "Three", "16", "17", "Three", "19", "20", "SevenThree"];
        HttpClient httpClient = new(_httpHandlerMock.Object);
        ApiFizzBuzzGenerator fizzBuzzGenerator = new(httpClient);

        // Act
        string[] result = (await fizzBuzzGenerator.Generate(range)).ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task ItShouldGenerateValuesNonSequentialInputs()
    {
        // Arrange
        int[] numbers = [1, 3, 7, 14, 15, 19, 21];
        string[] expected = ["1", "Three", "Seven", "Seven", "Three", "19", "SevenThree"];
        HttpClient httpClient = new(_httpHandlerMock.Object);
        ApiFizzBuzzGenerator fizzBuzzGenerator = new(httpClient);

        // Act
        string[] result = (await fizzBuzzGenerator.Generate(numbers)).ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ItShouldThrowApiInvalidResponse()
    {
        // Arrange
        Mock<HttpMessageHandler> httpHandlerMock = new();
        httpHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = null,
            });

        HttpClient httpClient = new(httpHandlerMock.Object);
        ApiFizzBuzzGenerator fizzBuzzGenerator = new(httpClient);

        // Act & Assert
        Assert.ThrowsAsync<Exception>(() => fizzBuzzGenerator.Generate("1-10"));
    }
}
