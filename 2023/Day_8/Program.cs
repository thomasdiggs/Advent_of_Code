//string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

string[] input = [
    "LLR",
    "AAA = (BBB, BBB)",
    "BBB = (AAA, ZZZ)",
    "ZZZ = (ZZZ, ZZZ)"
];

char[] instructions = input[0].ToCharArray();

Dictionary<string, (string, string)> rules = [];

for (int i = 1; i < input.Length; i++)
{
    if (input[i].Equals(""))
    {
        continue;
    }
    string[] parts = input[i].Split(" = ");
    string key = parts[0];
    string[] value = parts[1].Split(", ");
    rules[key] = (value[0].Replace("(", ""), value[1].Replace(")", ""));
}

int index = 0;
int accumulator = 0;
string tracker = "AAA";

while (!tracker.Equals("ZZZ"))
{
    if (index >= instructions.Length)
    {
        index = 0;
    }
    char instruction = instructions[index];
    switch (instruction)
    {
        case 'L':
            tracker = rules[tracker].Item1;
            accumulator++;
            break;
        case 'R':
            tracker = rules[tracker].Item2;
            accumulator++;
            break;
        default:
            Console.Error.WriteLine("Invalid instruction");
            return;
    }
    index++;
}

Console.WriteLine($"Part One: {accumulator}");