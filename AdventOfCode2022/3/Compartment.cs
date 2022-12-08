namespace AdventOfCode2022._3
{
    public class Compartment
    {
        public string Letters { get; }

        public Compartment(string letters)
        {
            Letters = letters;
        }

        public List<char> FindMatches(Compartment other)
        {
            return Letters.Where(l => other.Letters.Contains(l)).Distinct().ToList();
        }
    }
}
