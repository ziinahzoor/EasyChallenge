using TwistedFizzBuzz;

var values = "1-100";

IFizzBuzzGenerator generator = DefaultFizzBuzzGenerator.Create();
IEnumerable<string> fizzBuzzOutputs = generator.Generate(values);

string resultOutput = string.Join(' ', fizzBuzzOutputs);

Console.WriteLine(resultOutput);