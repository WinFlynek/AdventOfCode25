using System.IO.Pipelines;
using System;
using System.Collections.Generic;

namespace AdventOfCode25
{
    public class Day5_Part1
    {
        public static void Part1()
        {
            string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day5\puzzleInput.txt";
            var (startInterval, endInterval, number) = GetInputFromFile(filePath);
            int result = 0;

            foreach (long checkNumber in number){
                for (int lineInterval = 0; lineInterval <= startInterval.Length-1; lineInterval++)
                if(checkNumber >= startInterval[lineInterval] && checkNumber <= endInterval[lineInterval])
                {
                    result++;
                    break;
                }
            }

    Console.WriteLine($"Day4_Part1: {result}");
}
       static (long[] startInterval, long[] endInterval, long[] number) GetInputFromFile(string filePath)
{
    var startInterval = new List<long>();
    var endInterval = new List<long>();
    var numbers = new List<long>();

    foreach (string line in File.ReadLines(filePath))
    {
        string row = line.Trim();
        if (string.IsNullOrWhiteSpace(row))
            continue;

        // If the row contains "-", it is an interval
        if (row.Contains('-'))
        {
            string[] parts = row.Split('-');

            long startNumber = long.Parse(parts[0]);
            long endNumber = long.Parse(parts[1]);

            startInterval.Add(startNumber);
            endInterval.Add(endNumber);
        }
        else
        {
            // Otherwise it is a single number
            numbers.Add(long.Parse(row));
        }
    }

    return (startInterval.ToArray(), endInterval.ToArray(), numbers.ToArray());
}
    }
}
