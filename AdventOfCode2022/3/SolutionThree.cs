using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022._3
{
    public static class SolutionThree
    {
        private const int CapitalAAsciiIdx = 65;
        private const int CapitalZAsciiIdx = 90;
        private const int CapitalAPriority = 27;
        private const int CapitalOffset = CapitalAAsciiIdx - CapitalAPriority;

        private const int LowerAAsciiIdx = 97;
        private const int LowerZAsciiIdx = 122;
        private const int LowerAPriority = 1;
        private const int LowerOffset = LowerAAsciiIdx - LowerAPriority;

        public static async Task<int> AnswerPartOne()
        {
            var rucksacks = await BuildRucksacks();

            int totalMatchPriority = 0;

            foreach (var rucksack in rucksacks)
            {
                var matches = rucksack.FindMatchesInCompartments();
                
                foreach (var match in matches)
                {
                    totalMatchPriority += GetPriorityFromChar(match);
                }
            }

            return totalMatchPriority;
        }

        public static async Task<int> AnswerPartTwo()
        {
            var rucksacks = await BuildRucksacks();

            int totalMatchPriority = 0;

            // Iterate per 3 rucksacks (1 group)
            for (int i = 0; i < rucksacks.Count; i += 3)
            {
                // group
                var sack1 = rucksacks[i];
                var sack2 = rucksacks[i+1];
                var sack3 = rucksacks[i+2];

                // Find the item that all sacks have
                var matchesOneVTwo = sack1.FindMatches(sack2);
                var matchesTwoVThree = sack2.FindMatches(sack3);
                var matchesThreeVOne = sack3.FindMatches(sack1);

                var completeGroupMatch = matchesOneVTwo
                    .Where(c => matchesTwoVThree.Contains(c) && matchesThreeVOne.Contains(c))
                    // There can and shall ever only be one match. Single call will crash if this is not the case so works fine as a validation
                    .Single();

                totalMatchPriority += GetPriorityFromChar(completeGroupMatch);
            }

            return totalMatchPriority;
        }

        private static int GetPriorityFromChar(char match)
        {
            var asciiIdx = Convert.ToInt32(match);
            int priority;

            if (asciiIdx >= CapitalAAsciiIdx && asciiIdx <= CapitalZAsciiIdx)
            {
                priority = asciiIdx - CapitalOffset;
            }
            else if (asciiIdx >= LowerAAsciiIdx && asciiIdx <= LowerZAsciiIdx)
            {
                priority = asciiIdx - LowerOffset;
            }
            else
            {
                throw new ArgumentException("shenanigans");
            }

            return priority;
        }

        private static async Task<List<Rucksack>> BuildRucksacks()
        {
            var input = await ReadInput();

            List<Rucksack> result = new List<Rucksack>();

            foreach (var line in input)
            {
                // Ensure no spaces
                var trimmed = line.Trim();
                result.Add(Rucksack.Create(trimmed));
            }

            return result;
        }

        private static async Task<List<string>> ReadInput()
        {
            return (await File.ReadAllLinesAsync("./3/input.txt")).ToList();
        }
    }
}
