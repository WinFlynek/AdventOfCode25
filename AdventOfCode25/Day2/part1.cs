
using System.IO.Pipelines;

namespace AdventOfCode25.Day1;

public class Day2_Part1
{
    public static void Part1()
    {
        string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day2\puzzleInput.txt";
        var (startInterval, endInterval) = GetInputFromFile(filePath);
        long result = 0;
        for (int range = 0; range <= startInterval.Length - 1; range++){
            for (long numberInRange = startInterval[range]; numberInRange <= endInterval[range]; numberInRange++)
            {
                string numberToString = numberInRange.ToString();
                if (numberToString.Length % 2 == 0)
                {
                    int mid = numberToString.Length / 2;
                    string part1 = numberToString.Remove(0, mid);
                    string part2 = numberToString.Remove(mid);
                    if(part1 == part2)
                    {
                        result += numberInRange;
                    }
                }
            }

        }
        Console.WriteLine($"Day2_Part1: {result}");

    }

    static (long[] startInterval, long[] endInterval) GetInputFromFile(string filePath)
        {
            var startInterval = new List<long>();
            var endInterval = new List<long>();

         string line = File.ReadAllText(filePath).Trim();
         var ranges = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var range in ranges)
        {
            var parts = range.Split('-', StringSplitOptions.RemoveEmptyEntries);

            long start = long.Parse(parts[0]);
            long end = long.Parse(parts[1]);

            startInterval.Add(start);
            endInterval.Add(end);
        }

            return (startInterval.ToArray(), endInterval.ToArray());
        }
}