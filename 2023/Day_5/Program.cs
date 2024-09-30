
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Day_5
{
    internal class Interval(long start, long length)
    {
        public long Start { get; set; } = start;
        public long Length { get; set; } = length;
    }

    internal class Map(long destinationRangeStart, long sourceRangeStart, long rangeLength)
    {
        public long DestinationRangeStart { get; } = destinationRangeStart;
        public long SourceRangeStart { get; } = sourceRangeStart;
        public long RangeLength { get; } = rangeLength;
    }

    internal class Maps(string[] input)
    {
        public List<Map> SeedToSoil { get; } = GetMap(input, "seed-to-soil map:");
        public List<Map> SoilToFertilizer { get; } = GetMap(input, "soil-to-fertilizer map:");
        public List<Map> FertilizerToWater { get; } = GetMap(input, "fertilizer-to-water map:");
        public List<Map> WaterToLight { get; } = GetMap(input, "water-to-light map:");
        public List<Map> LightToTemperature { get; } = GetMap(input, "light-to-temperature map:");
        public List<Map> TemperatureToHumidity { get; } = GetMap(input, "temperature-to-humidity map:");
        public List<Map> HumidityToLocation { get; } = GetMap(input, "humidity-to-location map:");

        private static List<Map> GetMap(string[] input, string mapName)
        {
            long index = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Equals(mapName))
                {
                    index = i;
                    break;
                }
            }
            List<Map> map = [];
            for (long i = index + 1; i < input.Length; i++)
            {
                if (input[i].Equals(""))
                {
                    break;
                }
                MatchCollection matches = Regex.Matches(input[i], @"\d+");
                map.Add(new Map(long.Parse(matches[0].Value), long.Parse(matches[1].Value), long.Parse(matches[2].Value)));
            }
            return map;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            string[] input = [
                "seeds: 79 14 55 13",
                "",
                "seed-to-soil map:",
                "50 98 2",
                "52 50 48",
                "",
                "soil-to-fertilizer map:",
                "0 15 37",
                "37 52 2",
                "39 0 15",
                "",
                "fertilizer-to-water map:",
                "49 53 8",
                "0 11 42",
                "42 0 7",
                "57 7 4",
                "",
                "water-to-light map:",
                "88 18 7",
                "18 25 70",
                "",
                "light-to-temperature map:",
                "45 77 23",
                "81 45 19",
                "68 64 13",
                "",
                "temperature-to-humidity map:",
                "0 69 1",
                "1 0 69",
                "",
                "humidity-to-location map:",
                "60 56 37",
                "56 93 4"];

            MatchCollection matches = Regex.Matches(input[0], @"\d+");
            long[] seeds = new long[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                seeds[i] = long.Parse(matches[i].Value);
            }

            Maps maps = new(input);

            long lowestLocation = long.MaxValue;

            foreach (long seed in seeds)
            {
                long soil = seed;
                foreach (Map map in maps.SeedToSoil)
                {
                    if (seed >= map.SourceRangeStart && seed < map.SourceRangeStart + map.RangeLength)
                    {
                        soil = map.DestinationRangeStart + seed - map.SourceRangeStart;
                        break;
                    }
                }
                long fertilizer = soil;
                foreach (Map map in maps.SoilToFertilizer)
                {
                    if (soil >= map.SourceRangeStart && soil < map.SourceRangeStart + map.RangeLength)
                    {
                        fertilizer = map.DestinationRangeStart + soil - map.SourceRangeStart;
                        break;
                    }
                }
                long water = fertilizer;
                foreach (Map map in maps.FertilizerToWater)
                {
                    if (fertilizer >= map.SourceRangeStart && fertilizer < map.SourceRangeStart + map.RangeLength)
                    {
                        water = map.DestinationRangeStart + fertilizer - map.SourceRangeStart;
                        break;
                    }
                }
                long light = water;
                foreach (Map map in maps.WaterToLight)
                {
                    if (water >= map.SourceRangeStart && water < map.SourceRangeStart + map.RangeLength)
                    {
                        light = map.DestinationRangeStart + water - map.SourceRangeStart;
                        break;
                    }
                }
                long temperature = light;
                foreach (Map map in maps.LightToTemperature)
                {
                    if (light >= map.SourceRangeStart && light < map.SourceRangeStart + map.RangeLength)
                    {
                        temperature = map.DestinationRangeStart + light - map.SourceRangeStart;
                        break;
                    }
                }
                long humidity = temperature;
                foreach (Map map in maps.TemperatureToHumidity)
                {
                    if (temperature >= map.SourceRangeStart && temperature < map.SourceRangeStart + map.RangeLength)
                    {
                        humidity = map.DestinationRangeStart + temperature - map.SourceRangeStart;
                        break;
                    }
                }
                long location = humidity;
                foreach (Map map in maps.HumidityToLocation)
                {
                    if (humidity >= map.SourceRangeStart && humidity < map.SourceRangeStart + map.RangeLength)
                    {
                        location = map.DestinationRangeStart + humidity - map.SourceRangeStart;
                        break;
                    }
                }
                if (location < lowestLocation)
                {
                    lowestLocation = location;
                }
            }

            Console.WriteLine($"Part One: {lowestLocation}");
            Console.WriteLine();


            List<Interval> intervals = [];
            for (int i = 0; i < matches.Count; i += 2)
            {
                intervals.Add(new Interval(seeds[i], seeds[i + 1]));
            }

            Console.WriteLine($"Seed to Soil:");
            intervals = UpdateIntervals(intervals, maps.SeedToSoil);
            PrintIntervals(intervals);
            Console.WriteLine("\n");

            Console.WriteLine($"Soil to Fertilizer:");
            intervals = UpdateIntervals(intervals, maps.SoilToFertilizer);
            PrintIntervals(intervals);
            Console.WriteLine("\n");

            Console.WriteLine($"Fertilizer to Water:");
            intervals = UpdateIntervals(intervals, maps.FertilizerToWater);
            PrintIntervals(intervals);
            Console.WriteLine("\n");

            Console.WriteLine($"Water to Light:");
            intervals = UpdateIntervals(intervals, maps.WaterToLight);
            PrintIntervals(intervals);
            Console.WriteLine("\n");

            Console.WriteLine($"Light to Temperature:");
            intervals = UpdateIntervals(intervals, maps.LightToTemperature);
            PrintIntervals(intervals);
            Console.WriteLine("\n");

            Console.WriteLine($"Temperature to Humidity:");
            intervals = UpdateIntervals(intervals, maps.TemperatureToHumidity);
            PrintIntervals(intervals);
            Console.WriteLine("\n");

            Console.WriteLine($"Humidity to Location:");
            intervals = UpdateIntervals(intervals, maps.HumidityToLocation);
            PrintIntervals(intervals);
            Console.WriteLine("\n");

            long lowestLocationStart = long.MaxValue;
            foreach (Interval interval in intervals)
            {
                if (interval.Start < lowestLocationStart)
                {
                    lowestLocationStart = interval.Start;
                }
            }

            Console.WriteLine($"Part Two: {lowestLocationStart}");
        }

        static List<Interval> UpdateIntervals(List<Interval> intervals, List<Map> maps)
        {
            List<Interval> newIntervals = [];
            //foreach (Interval interval in intervals)
            int count = intervals.Count;
            for (int j = 0; j < count; j++)
            {
                Interval interval = intervals[j];


                Console.WriteLine($"Interval {intervals.IndexOf(interval) + 1}:");
                Console.WriteLine($"Start: {interval.Start}, End: {interval.Start + interval.Length - 1}");

                bool added = false;

                for (int i = 0; i < maps.Count; i++)
                {
                    long sourceIntervalStart = maps[i].SourceRangeStart;
                    long sourceIntervalEnd = maps[i].SourceRangeStart + maps[i].RangeLength - 1;
                    long destinationIntervalStart = maps[i].DestinationRangeStart;
                    long destinationIntervalEnd = maps[i].DestinationRangeStart + maps[i].RangeLength - 1;

                    Console.WriteLine($"Map: {i + 1}");
                    Console.WriteLine($"Source Start: {sourceIntervalStart}, Source End: {sourceIntervalEnd}");
                    Console.WriteLine($"Destination Start: {destinationIntervalStart}, Destination End: {destinationIntervalEnd}");

                    // If it within the mapping interval, shift it
                    if (!added && interval.Start >= sourceIntervalStart && interval.Start + interval.Length - 1 <= sourceIntervalEnd)
                    {
                        Console.WriteLine($"Interval is within the mapping interval");
                        //long intervalStart = destinationIntervalStart + interval.Start - sourceIntervalStart;
                        //long intervalEnd = intervalStart + interval.Length - 1;
                        //newIntervals.Add(new Interval(intervalStart, interval.Length));
                        newIntervals.Add(new Interval((destinationIntervalStart - sourceIntervalStart) + interval.Start, interval.Length));
                        added = true;
                        PrintIntervals(newIntervals);
                    }
                    // If the start and only a portion of the interval excluding the end is within the mapping interval
                    // Split it into two intervals and shift the portion that is within the mapping interval
                    else if (!added && interval.Start >= sourceIntervalStart && interval.Start + interval.Length - 1 > sourceIntervalEnd && interval.Start <= sourceIntervalEnd)
                    {
                        Console.WriteLine($"Start and a portion of the interval is within the mapping interval, now splitting...");
                        newIntervals.Add(new Interval((destinationIntervalStart - sourceIntervalStart) + interval.Start, sourceIntervalEnd - interval.Start + 1));
                        // Add the remaining portion of the interval to the existing interval list to be reevaluated
                        intervals.Add(new Interval(sourceIntervalEnd + 1, (interval.Start + interval.Length - 1) - sourceIntervalEnd));
                        count++;
                        added = true;
                        PrintIntervals(newIntervals);
                    }
                    // If the end and only a portion of the interval excluding the start is within the mapping interval
                    // Split it into two intervals and shift the portion that is within the mapping interval
                    else if (!added && interval.Start < sourceIntervalStart && interval.Start + interval.Length - 1 <= sourceIntervalEnd && interval.Start + interval.Length - 1 >= sourceIntervalStart)
                    {
                        Console.WriteLine($"End and a portion of the interval is within the mapping interval, now splitting...");
                        newIntervals.Add(new Interval(destinationIntervalStart, (interval.Start + interval.Length - 1) - sourceIntervalStart + 1));
                        // Add the remaining portion of the interval to the existing interval list to be reevaluated
                        intervals.Add(new Interval(interval.Start, sourceIntervalStart - interval.Start));
                        count++;
                        added = true;
                        PrintIntervals(newIntervals);
                    }
                    // or the interval spans the mapping interval
                    else if (!added && interval.Start < sourceIntervalStart && interval.Start + interval.Length - 1 > sourceIntervalEnd)
                    {
                        Console.WriteLine($"Interval spans the mapping interval");
                        // Add three intervals
                        // One from interval start to source interval start
                        // ADD THIS TO THE EXISTING INTERVAL LIST TO BE REEVALUATED
                        intervals.Add(new Interval(destinationIntervalStart, sourceIntervalStart - interval.Start));
                        count++;
                        // One from destination interval start to destination interval end
                        newIntervals.Add(new Interval(destinationIntervalStart, destinationIntervalEnd - destinationIntervalStart + 1));
                        // One from source interval end to interval end
                        // ADD THIS TO THE EXISTING INTERVAL LIST TO BE REEVALUATED
                        intervals.Add(new Interval(sourceIntervalEnd + 1, (interval.Start + interval.Length - 1) - sourceIntervalEnd));
                        count++;
                        added = true;
                        PrintIntervals(newIntervals);
                    }
                    Console.WriteLine(added);
                }
                if (!added)
                {
                    Console.WriteLine("Interval not within any mapping interval, adding original interval");
                    newIntervals.Add(interval);
                }
                Console.WriteLine();
            }

            return newIntervals;
        }

        static void PrintIntervals(List<Interval> intervals)
        {
            foreach (Interval interval in intervals)
            {
                Console.WriteLine($"Start: {interval.Start}, End: {interval.Start + interval.Length - 1}");
            }
        }
    }
}
