// See https://aka.ms/new-console-template for more information
using AdventOfCode2022._1;
using AdventOfCode2022._2;
using AdventOfCode2022._3;
Console.WriteLine("Hello, World!");

/*
int answerPOne = await SolutionOne.AnswerPartOne();
Console.WriteLine($"Answer to assignment 1 part 1 is {answerPOne}");

int answerPTwo = await SolutionOne.AnswerPartTwo();
Console.WriteLine($"Answer to assignment 1 part 2 is {answerPTwo}");
*/

/*
int answerPOne = await SolutionTwo.AnswerPartOne();
Console.WriteLine($"Answer to assignment 2 part 1 is {answerPOne}");

int answerPTwo = await SolutionTwo.AnswerPartTwo();
Console.WriteLine($"Answer to assignment 2 part 2 is {answerPTwo}");

*/
int answerPOne = await SolutionThree.AnswerPartOne();
Console.WriteLine($"Answer to assignment 3 part 1 is {answerPOne}");

int answerPTwo = await SolutionThree.AnswerPartTwo();
Console.WriteLine($"Answer to assignment 3 part 2 is {answerPTwo}");

var _ = Console.ReadKey();