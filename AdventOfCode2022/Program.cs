// See https://aka.ms/new-console-template for more information
using AdventOfCode2022._1;

Console.WriteLine("Hello, World!");

int answerPOne = await SolutionOne.AnswerPartOne();
Console.WriteLine($"Answer to assignment 1 part 1 is {answerPOne}");

int answerPTwo = await SolutionOne.AnswerPartTwo();
Console.WriteLine($"Answer to assignment 1 part 2 is {answerPTwo}");
var _ = Console.ReadKey();