using TwistedFizzBuzz;

var values = "1-100";

IEnumerable<string> fizzBuzzOutputs = FizzBuzzGenerator.Generate(values);

string resultOutput = string.Join(' ', fizzBuzzOutputs);

Console.WriteLine(resultOutput);