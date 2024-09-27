
using System.Text.RegularExpressions;

namespace Day_3
{
    internal class PartNumber(string value, int row, int index, int length)
    {
        public string Value { get; } = value;
        public int Row { get; } = row;
        public int Index { get; } = index;
        public int Length { get; } = length;
        public bool HasSymbol { get; set; } = false;
    }

    internal class Symbol(string value, int row, int index)
    {
        public string Value { get; } = value;
        public int Row { get; } = row;
        public int Index { get; } = index;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            /**** Part One ****/
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            //string[] input =
            //[
            //    "467..114..",
            //    "...*......",
            //    "..35..633.",
            //    "......#...",
            //    "617*......",
            //    ".....+.58.",
            //    "..592.....",
            //    "......755.",
            //    "...$.*....",
            //    ".664.598.."
            //];

            List<PartNumber> partNumbers = [];
            List<Symbol> symbols = [];

            string patternPartNumber = @"[0-9]+";
            string patternSymbol = @"[^0-9.]";

            for (int i = 0; i < input.Length; i++)
            {
                MatchCollection matchesPartNumbers = Regex.Matches(input[i], patternPartNumber);
                foreach (Match match in matchesPartNumbers)
                {
                    partNumbers.Add(new PartNumber(match.Value, i, match.Index, match.Length));
                }
                MatchCollection matchesSymbols = Regex.Matches(input[i], patternSymbol);
                foreach (Match match in matchesSymbols)
                {
                    symbols.Add(new Symbol(match.Value, i, match.Index));
                }
            }

            int sum = 0;

            foreach (PartNumber partNumber in partNumbers)
            {
                foreach (Symbol symbol in symbols)
                {
                    // Check left
                    if (partNumber.Index > 0)
                    {
                        int positionLeft = partNumber.Index - 1;
                        if (symbol.Index == positionLeft && symbol.Row == partNumber.Row)
                        {
                            partNumber.HasSymbol = true;
                        }
                    }
                    // Check right
                    if (partNumber.Index + partNumber.Length < input[partNumber.Row].Length)
                    {
                        int positionRight = partNumber.Index + partNumber.Length;
                        if (symbol.Index == positionRight && symbol.Row == partNumber.Row)
                        {
                            partNumber.HasSymbol = true;
                        }
                    }
                    // Check above
                    if (partNumber.Row > 0)
                    {
                        int startingPosition = partNumber.Index > 0 ? (partNumber.Index - 1) : (partNumber.Index);
                        int endingPosition = partNumber.Index + partNumber.Length < input[partNumber.Row].Length ? (partNumber.Index + partNumber.Length) : (partNumber.Index + partNumber.Length - 1);
                        if (symbol.Index >= startingPosition && symbol.Index <= endingPosition && symbol.Row == partNumber.Row - 1)
                        {
                            partNumber.HasSymbol = true;
                        }
                    }
                    // Check below
                    if (partNumber.Row < input.Length - 1)
                    {
                        int startingPosition = partNumber.Index > 0 ? (partNumber.Index - 1) : (partNumber.Index);
                        int endingPosition = partNumber.Index + partNumber.Length < input[partNumber.Row].Length ? (partNumber.Index + partNumber.Length) : (partNumber.Index + partNumber.Length - 1);
                        if (symbol.Index >= startingPosition && symbol.Index <= endingPosition && symbol.Row == partNumber.Row + 1)
                        {
                            partNumber.HasSymbol = true;
                        }
                    }
                }
                sum += partNumber.HasSymbol ? int.Parse(partNumber.Value) : 0;
            }

            Console.WriteLine("Part Numbers:");
            foreach (PartNumber partNumber in partNumbers)
            {
                Console.WriteLine($"Value: {partNumber.Value}, Row: {partNumber.Row}, Index: {partNumber.Index}, Length: {partNumber.Length}, Has Symbol: {partNumber.HasSymbol}");
            }
            Console.WriteLine();
            Console.WriteLine($"Symbols:");
            foreach (Symbol symbol in symbols)
            {
                Console.WriteLine($"Value: {symbol.Value}, Row: {symbol.Row}, Index: {symbol.Index}");
            }

            Console.WriteLine();
            Console.WriteLine($"Part One: {sum}");
        }
    }
}
