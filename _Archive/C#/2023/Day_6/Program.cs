using System.Text.RegularExpressions;

namespace Day_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            MatchCollection times = Regex.Matches(input[0], @"\d+");
            MatchCollection records = Regex.Matches(input[1], @"\d+");

            if (times.Count != records.Count)
            {
                throw new Exception("Invalid input. The number of times and records do not match.");
            }

            int marginOfError = 1;
            for (int i = 0; i < times.Count; i++)
            {
                marginOfError *= CalculateNumberOfWaysToWin(int.Parse(times[i].Value), int.Parse(records[i].Value));
            }

            Console.WriteLine($"Part One: {marginOfError}");


            long partTwoRaceTime = long.Parse(input[0].Replace(" ", "").Split(":")[1]);
            long partTwoRaceRecord = long.Parse(input[1].Replace(" ", "").Split(":")[1]);

            Console.WriteLine($"Part Two: {CalculateNumberOfWaysToWin(partTwoRaceTime, partTwoRaceRecord)}");
        }

        public static long CalculateNumberOfWaysToWin(long time, long record)
        {
            long numberOfWaysToWin = 0;
            for (long i = 0; i <= time; i++)
            {
                if (i * (time - i) > record)
                {
                    numberOfWaysToWin++;
                }
            }
            return numberOfWaysToWin;
        }

        public static int CalculateNumberOfWaysToWin(int time, int record)
        {
            int numberOfWaysToWin = 0;
            for (int i = 0; i <= time; i++)
            {
                if (i * (time - i) > record)
                {
                    numberOfWaysToWin++;
                }
            }
            return numberOfWaysToWin;
        }
    }
}
