
using System.Text.RegularExpressions;

namespace Day_5
{
    internal class Map(long destinationRangeStart, long sourceRangeStart, long rangeLength)
    {
        public long DestinationRangeStart { get; } = destinationRangeStart;
        public long SourceRangeStart { get; } = sourceRangeStart;
        public long RangeLength { get; } = rangeLength;
    }

    internal class Maps
    {
        public List<Map> SeedToSoil { get; }
        public List<Map> SoilToFertilizer { get; }
        public List<Map> FertilizerToWater { get; }
        public List<Map> WaterToLight { get; }
        public List<Map> LightToTemperature { get; }
        public List<Map> TemperatureToHumidity { get; }
        public List<Map> HumidityToLocation { get; }

        public Maps(string[] input)
        {
            SeedToSoil = GetMap(input, "seed-to-soil map:");
            SoilToFertilizer = GetMap(input, "soil-to-fertilizer map:");
            FertilizerToWater = GetMap(input, "fertilizer-to-water map:");
            WaterToLight = GetMap(input, "water-to-light map:");
            LightToTemperature = GetMap(input, "light-to-temperature map:");
            TemperatureToHumidity = GetMap(input, "temperature-to-humidity map:");
            HumidityToLocation = GetMap(input, "humidity-to-location map:");
        }

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
        }
    }
}
