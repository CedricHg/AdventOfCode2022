using System.Diagnostics;

namespace AdventOfCode2022._2
{
    /// <summary>
    /// Overengineered this cause I felt like it
    /// </summary>
    public static class SolutionTwo
    {
        public static async Task<int> AnswerPartOne()
        {
            var array = await BuildArray();

            int totalScore = 0;
            int length = array.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                try {
                    var theirPick = new RpsPick(array[i, 0]);
                    var myPick = new RpsPick(array[i, 1]);

                    int beatScore = myPick.BeatScore(theirPick);

                    if (myPick.IsMyMove)
                    {
                        totalScore += myPick.PickScore();
                    }

                    totalScore += beatScore;
                }
                catch (Exception ex)
                {
                    // Debug here
                    throw ex;
                }
                
            }

            return totalScore;
        }

        public static async Task<int> AnswerPartTwo()
        {
            var array = await BuildArray();

            int totalScore = 0;
            int length = array.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                try
                {
                    var theirPick = new RpsPick(array[i, 0]);
                    var requestedOutcome = array[i, 1];

                    var myPick = RpsPick.CreateForOutcome(theirPick, requestedOutcome);
                    int beatScore = myPick.BeatScore(theirPick);

                    if (myPick.IsMyMove)
                    {
                        totalScore += myPick.PickScore();
                    }

                    totalScore += beatScore;
                }
                catch (Exception ex)
                {
                    // Debug here
                    throw ex;
                }

            }

            return totalScore;
        }

        /// <summary>
        /// Read the 2 columns a multi-dimensional array
        /// </summary>
        private static async Task<string[,]> BuildArray()
        {
            var input = await ReadInput();

            string[,] result = new string[input.Count, 2];

            for (int i = 0; i < input.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(input[i]))
                {
                    throw new ArgumentException("Input shenanigans");
                }

                var chars = input[i].Split(" ");

                if (chars.Count() != 2)
                {
                    throw new ArgumentException("More shenanigans");
                }

                result[i, 0] = chars[0];
                result[i, 1] = chars[1];
            }

            return result;
        }

        private static async Task<List<string>> ReadInput()
        {
            return (await File.ReadAllLinesAsync("./2/input.txt")).ToList();
        }

        public enum RpsPickEnum
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }

        private class RpsPick
        {
            public bool IsMyMove { get; }
            public RpsPickEnum EnumValue { get; }

            public RpsPick(string value)
            {
                switch (value)
                {
                    case "A":
                    case "X":
                        EnumValue = RpsPickEnum.Rock;
                        break;
                    case "B":
                    case "Y":
                        EnumValue = RpsPickEnum.Paper;
                        break;
                    case "C":
                    case "Z":
                        EnumValue = RpsPickEnum.Scissors;
                        break;
                    default:
                        throw new ArgumentException("Nope");
                }

                IsMyMove = value == "X" || value == "Y" || value == "Z";
            }

            public RpsPick(RpsPickEnum enumValue, bool myPick) 
            {
                EnumValue = enumValue;
                IsMyMove = myPick;
            }

            public int PickScore()
            {
                switch (EnumValue)
                {
                    case RpsPickEnum.Rock:
                        return 1;
                    case RpsPickEnum.Paper:
                        return 2;
                    case RpsPickEnum.Scissors:
                        return 3;
                    default:
                        return 0;
                }
            }

            /// <summary>
            /// True if beats, false if not, null if draw
            /// </summary>
            public int BeatScore(RpsPick other)
            {
                if (EnumValue == RpsRuleBook.WinsAgainst(other)) 
                {
                    return 0;
                }
                else if (EnumValue == RpsRuleBook.LosesAgainst(other))
                {
                    return 6;
                }

                return 3;
            }

            /// <summary>
            /// Create an RPS pick that would result in a requested outcome against the other pick
            /// X = loss, Y = draw, Z = win
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public static RpsPick CreateForOutcome(RpsPick other, string outcomeRequest)
            {
                RpsPickEnum pickMustBe;

                if (outcomeRequest == "X")
                {
                    pickMustBe = RpsRuleBook.WinsAgainst(other);
                }
                else if (outcomeRequest == "Y")
                {
                    pickMustBe = RpsRuleBook.DrawAgainst(other);
                }
                else if (outcomeRequest == "Z")
                {
                    pickMustBe = RpsRuleBook.LosesAgainst(other);
                }
                else
                {
                    throw new ArgumentException("invalid requested outcome");
                }

                return new RpsPick(pickMustBe, true);
            }

            public override string ToString()
            {
                return EnumValue.ToString();
            }
        }

        private static class RpsRuleBook
        {
            public static RpsPickEnum WinsAgainst(RpsPick pick)
            {
                if (pick.EnumValue == RpsPickEnum.Rock)
                {
                    return RpsPickEnum.Scissors;
                }

                if (pick.EnumValue == RpsPickEnum.Paper)
                {
                    return RpsPickEnum.Rock;
                }

                if (pick.EnumValue == RpsPickEnum.Scissors)
                {
                    return RpsPickEnum.Paper;
                }

                throw new ArgumentException("no");
            }

            public static RpsPickEnum LosesAgainst(RpsPick pick)
            {
                if (pick.EnumValue == RpsPickEnum.Rock)
                {
                    return RpsPickEnum.Paper;
                }

                if (pick.EnumValue == RpsPickEnum.Paper)
                {
                    return RpsPickEnum.Scissors;
                }

                if (pick.EnumValue == RpsPickEnum.Scissors)
                {
                    return RpsPickEnum.Rock;
                }

                throw new ArgumentException("no");
            }

            public static RpsPickEnum DrawAgainst(RpsPick pick)
            {
                if (pick.EnumValue == RpsPickEnum.Rock)
                {
                    return RpsPickEnum.Rock;
                }

                if (pick.EnumValue == RpsPickEnum.Paper)
                {
                    return RpsPickEnum.Paper;
                }

                if (pick.EnumValue == RpsPickEnum.Scissors)
                {
                    return RpsPickEnum.Scissors;
                }

                throw new ArgumentException("no");
            }
        }
    }
}
