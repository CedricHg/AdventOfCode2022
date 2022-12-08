using System.Diagnostics;

namespace AdventOfCode2022._3
{
    public class Rucksack
    {
        public Compartment CompartmentOne { get; }
        public Compartment CompartmentTwo { get; }

        public Rucksack(Compartment first, Compartment second)
        {
            CompartmentOne = first;
            CompartmentTwo = second;
        }

        public static Rucksack Create(string line)
        {
            var length = line.Length;
            var compartment1 = line.Substring(0, length / 2);
            var compartment2 = line.Substring(compartment1.Length);

            Debug.Assert(compartment1.Length == compartment2.Length);
            Debug.Assert(compartment1.Length + compartment2.Length == line.Length);

            return new Rucksack(new Compartment(compartment1), new Compartment(compartment2));
        }

        public List<char> FindMatchesInCompartments()
        {
            var matches = CompartmentOne.FindMatches(CompartmentTwo);
            return matches;
        }

        public List<char> FindMatches(Rucksack other)
        {
            // Create two virtual compartments that contain the entire rucksack
            var myCompartment = ToVirtualCompartment();
            var otherCompartment = other.ToVirtualCompartment();

            var matches = myCompartment.FindMatches(otherCompartment);
            return matches;
        }

        private Compartment ToVirtualCompartment()
        {
            return new Compartment(CompartmentOne.Letters + CompartmentTwo.Letters);
        }
    }
}
