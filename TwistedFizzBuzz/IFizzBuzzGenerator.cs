namespace TwistedFizzBuzz;

public interface IFizzBuzzGenerator
{
    public IEnumerable<string> Generate(string range);

    public IEnumerable<string> Generate(int start, int end);

    public IEnumerable<string> Generate(IEnumerable<int> numbers);
}
