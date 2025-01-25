﻿using TwistedFizzBuzz;
using TokenDictionary = System.Collections.Generic.Dictionary<int, string>;

TokenDictionary tokens = new()
{
    { 5, "Fizz" },
    { 9, "Buzz" },
    { 27, "Bar" }
};

var values = "(-20)-127";

IEnumerable<string> fizzBuzzOutputs = FizzBuzzGenerator.Generate(values, tokens);

string resultOutput = string.Join(' ', fizzBuzzOutputs);

Console.WriteLine(resultOutput);