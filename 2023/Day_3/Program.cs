
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
            string patternGear = @"[*]";

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
                        int endingPosition = partNumber.Index + partNumber.Length < input[partNumber.Row - 1].Length ? (partNumber.Index + partNumber.Length) : (partNumber.Index + partNumber.Length - 1);
                        if (symbol.Index >= startingPosition && symbol.Index <= endingPosition && symbol.Row == partNumber.Row - 1)
                        {
                            partNumber.HasSymbol = true;
                        }
                    }
                    // Check below
                    if (partNumber.Row < input.Length - 1)
                    {
                        int startingPosition = partNumber.Index > 0 ? (partNumber.Index - 1) : (partNumber.Index);
                        int endingPosition = partNumber.Index + partNumber.Length < input[partNumber.Row + 1].Length ? (partNumber.Index + partNumber.Length) : (partNumber.Index + partNumber.Length - 1);
                        if (symbol.Index >= startingPosition && symbol.Index <= endingPosition && symbol.Row == partNumber.Row + 1)
                        {
                            partNumber.HasSymbol = true;
                        }
                    }
                }
                sum += partNumber.HasSymbol ? int.Parse(partNumber.Value) : 0;
            }

            Console.WriteLine($"Part One: {sum}");

            /**** Part Two ****/
            sum = 0;
            List<PartNumber> partNumbers2 = [];
            List<Symbol> symbols2 = [];

            for (int i = 0; i < input.Length; i++)
            {
                MatchCollection matchesPartNumbers = Regex.Matches(input[i], patternPartNumber);
                foreach (Match match in matchesPartNumbers)
                {
                    partNumbers2.Add(new PartNumber(match.Value, i, match.Index, match.Length));
                }
                MatchCollection matchesGears = Regex.Matches(input[i], patternGear);
                foreach (Match match in matchesGears)
                {
                    symbols2.Add(new Symbol(match.Value, i, match.Index));
                }
            }

            foreach (Symbol symbol in symbols2)
            {
                List<PartNumber> adjacentPartNumbers = [];
                // Check left
                if (symbol.Index > 0)
                {
                    foreach (PartNumber partNumber in partNumbers2)
                    {
                        if (partNumber.Index + partNumber.Length == symbol.Index && partNumber.Row == symbol.Row)
                        {
                            adjacentPartNumbers.Add(partNumber);
                        }
                    }
                }
                // Check right
                if (symbol.Index < input[symbol.Row].Length - 1)
                {
                    foreach (PartNumber partNumber in partNumbers2)
                    {
                        if (partNumber.Index == symbol.Index + 1 && partNumber.Row == symbol.Row)
                        {
                            adjacentPartNumbers.Add(partNumber);
                        }
                    }
                }
                // Check above
                if (symbol.Row > 0)
                {
                    int startingPosition = symbol.Index > 0 ? (symbol.Index - 1) : (symbol.Index);
                    int endingPosition = symbol.Index < input[symbol.Row - 1].Length - 1 ? (symbol.Index + 1) : (symbol.Index);
                    foreach (PartNumber partNumber in partNumbers2)
                    {
                        int partNumberRangeStart = partNumber.Index;
                        int partNumberRangeEnd = partNumber.Index + partNumber.Length - 1;
                        if (partNumberRangeStart <= endingPosition && partNumberRangeEnd >= startingPosition && partNumber.Row == symbol.Row - 1)
                        {
                            adjacentPartNumbers.Add(partNumber);
                        }
                    }
                }
                // Check below
                if (symbol.Row < input.Length - 1)
                {
                    int startingPosition = symbol.Index > 0 ? (symbol.Index - 1) : (symbol.Index);
                    int endingPosition = symbol.Index < input[symbol.Row + 1].Length - 1 ? (symbol.Index + 1) : (symbol.Index);
                    foreach (PartNumber partNumber in partNumbers2)
                    {
                        int partNumberRangeStart = partNumber.Index;
                        int partNumberRangeEnd = partNumber.Index + partNumber.Length - 1;
                        if (partNumberRangeStart <= endingPosition && partNumberRangeEnd >= startingPosition && partNumber.Row == symbol.Row + 1)
                        {
                            adjacentPartNumbers.Add(partNumber);
                        }
                    }
                }
                if (adjacentPartNumbers.Count == 2)
                {
                    sum+= int.Parse(adjacentPartNumbers.First().Value) * int.Parse(adjacentPartNumbers.Last().Value);
                }
            }

            Console.WriteLine($"Part Two: {sum}");
        }
    }
}
