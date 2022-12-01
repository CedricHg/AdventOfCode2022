using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022._1
{
    public static class SolutionOne
    {
        /// <summary>
        /// Get the highest number
        /// </summary>
        public static async Task<int> AnswerPartOne()
        {
            var answer = (await BuildList()).OrderByDescending(x => x).First();
            return answer;
        }

        /// <summary>
        /// Get the sum of top 3
        /// </summary>
        public static async Task<int> AnswerPartTwo()
        {
            var answer = (await BuildList()).OrderByDescending(x => x)
                .Select((x, i) => new { Index = i, Number = x })
                .Where(x => x.Index < 3)
                .Select(x => x.Number)
                .Sum();

            return answer;
        }

        /// <summary>
        /// Read the groups of data, calculate the number per group
        /// </summary>
        private static async Task<List<int>> BuildList()
        {
            var input = await ReadInput();

            List<int> counts = new List<int>();
            int currentCount = 0;
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    counts.Add(currentCount);
                    currentCount = 0;
                    continue;
                }
                else
                {
                    currentCount += int.Parse(line);
                }
            }

            return counts;
        }

        private static async Task<List<string>> ReadInput()
        {
            return (await File.ReadAllLinesAsync("./1/input.txt")).ToList();
        }
    }
}
