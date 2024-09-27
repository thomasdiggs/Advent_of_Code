using System.Text.RegularExpressions;

namespace Day_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /**** Part One ****/
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            int sumPossibles = 0;
            int sumPowers = 0;

            for (int i = 0; i < input.Length; i++)
            {
                int minRedNeeded = 0;
                int minGreenNeeded = 0;
                int minBlueNeeded = 0;

                // \d is a digit (i.e., [0-9]).
                // + means one or more times.
                // Thus, \d+ means match one or more digits.
                int gameNumber = int.Parse(Regex.Match(input[i].Split(":")[0], @"\d+").Value);
                bool isGamePossible = true;

                string[] rounds = input[i].Split(":")[1].Split(";");
                for (int j = 0; j < rounds.Length; j++)
                {
                    string[] colorsAndNumber = rounds[j].Split(",");
                    for (int k = 0; k < colorsAndNumber.Length; k++)
                    {
                        // [] is a character class, which means match any character inside the square brackets.
                        // [^] is a negated character class, which means match any character not inside the square brackets.
                        // [0-9] is a character range, which means match any character in the range 0 to 9.
                        // \s is a whitespace character (i.e., space, tab, newline, etc.).
                        // + means one or more times.
                        // Thus, [^0-9\s]+ means match one or more characters that are not digits or whitespace characters.
                        string color = Regex.Match(colorsAndNumber[k], @"[^0-9\s]+").Value;
                        int number = int.Parse(Regex.Match(colorsAndNumber[k], @"\d+").Value);
                        // Part One logic
                        if (color.Equals("red") && number > 12)
                        {
                            isGamePossible = false;
                        }
                        if (color.Equals("green") && number > 13)
                        {
                            isGamePossible = false;
                        }
                        if (color.Equals("blue") && number > 14)
                        {
                            isGamePossible = false;
                        }
                        // Part Two logic
                        if (color.Equals("red") && number > minRedNeeded)
                        {
                            minRedNeeded = number;
                        }
                        if (color.Equals("green") && number > minGreenNeeded)
                        {
                            minGreenNeeded = number;
                        }
                        if (color.Equals("blue") && number > minBlueNeeded)
                        {
                            minBlueNeeded = number;
                        }
                    }
                }
                if (isGamePossible)
                {
                    sumPossibles += gameNumber;
                }
                sumPowers += minRedNeeded * minGreenNeeded * minBlueNeeded;
            }

            Console.WriteLine($"Part One: {sumPossibles}");
            Console.WriteLine($"Part Two: {sumPowers}");
        }
    }
}
