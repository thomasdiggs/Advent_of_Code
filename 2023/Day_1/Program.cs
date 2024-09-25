using System.Globalization;

namespace Day_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            int sum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char first = input[i].First(x => x >= 48 && x <= 57);   // 48 is decimal for char '0'
                char last = input[i].Last(x =>  x >= 48 && x <= 57);    // 57 is decimal for char '9'
                sum += Int32.Parse(string.Concat(first, last));
            }

            Console.WriteLine(sum);
        }
    }
}
