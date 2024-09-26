
namespace Day_1
{
    internal class Program
    {
        static int FindCalibrationValue(string s)
        {
            char first = s.First(x => x >= 48 && x <= 57);  // 48 is decimal for char '0'
            char last = s.Last(x => x >= 48 && x <= 57);    // 57 is decimal for char '9'
            return Int32.Parse(string.Concat(first, last));  
        }

        static void Main(string[] args)
        {
            /* Part One */
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            int sum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                sum += FindCalibrationValue(input[i]);
            }

            Console.WriteLine($"Part One: {sum}");


            /* Part Two */
            Dictionary<string, string> keyValuePairs = new()
            {
                { "zero", "z0o" },
                { "one", "o1e" },
                { "two", "t2o" },
                { "three", "t3e" },
                { "four", "f4r" },
                { "five", "f5e" },
                { "six", "s6x" },
                { "seven", "s7n" },
                { "eight", "e8t" },
                { "nine", "n9e" }
            };

            sum = 0;

            foreach (string s in input)
            {
                string temp = s;
                foreach (KeyValuePair<string, string> keyValuePair in keyValuePairs)
                {
                    temp = temp.Replace(keyValuePair.Key, keyValuePair.Value);
                }
                sum += FindCalibrationValue(temp);
            }

            Console.WriteLine($"Part Two: {sum}");
        }
    }
}
