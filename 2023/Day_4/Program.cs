
using System.Text.RegularExpressions;

namespace Day_4
{
    internal class Card(string name, List<int> winningNumbers, List<int> yourNumbers, int matchingNumbersCount)
    {
        public string Name { get; set; } = name;
        public List<int> WinningNumbers { get; set; } = winningNumbers;
        public List<int> YourNumbers { get; set; } = yourNumbers;
        public int Matches { get; set; } = matchingNumbersCount;
        public int Copies { get; set; } = 1;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            int sum = 0;

            List<Card> cards = [];

            for (int i = 0; i < input.Length; i++)
            {
                string cardName = input[i].Split(':')[0];

                MatchCollection winningNumbers = Regex.Matches(input[i].Split(':')[1].Split('|')[0], @"\d+");
                List<int> winningNumbersInt = [];
                foreach (Match winningNumber in winningNumbers)
                {
                    winningNumbersInt.Add(int.Parse(winningNumber.Value));
                }

                MatchCollection yourNumbers = Regex.Matches(input[i].Split(':')[1].Split('|')[1], @"\d+");
                List<int> yourNumbersInt = [];
                foreach (Match yourNumber in yourNumbers)
                {
                    yourNumbersInt.Add(int.Parse(yourNumber.Value));
                }

                int score = 0;
                int matchingNumbersCount = 0;

                foreach (int number in yourNumbersInt)
                {
                    if (winningNumbersInt.Contains(number))
                    {
                        matchingNumbersCount++;
                        if (score == 0)
                        {
                            score = 1;
                        }
                        else
                        {
                            score *= 2;
                        }
                    }
                }

                sum += score;

                cards.Add(new Card(cardName, winningNumbersInt, yourNumbersInt, matchingNumbersCount));
            }

            Console.WriteLine($"Part One: {sum}");
            
            sum = 0;

            for (int i = 0; i < cards.Count; i++)
            {                
                int matches = cards[i].Matches;
                int copies = cards[i].Copies;
                int counter = 1;
                for (int j = 0; j < matches; j++)
                {
                    cards[i + counter].Copies += copies;
                    counter++;
                }
                sum += copies;
            }

            Console.WriteLine($"Part Two: {sum}");
        }
    }
}
