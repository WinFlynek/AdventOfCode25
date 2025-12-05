using System.IO.Pipelines;
using System;
using System.Collections.Generic;

namespace AdventOfCode25
{
    public class Day5_Part2
    {
        public static void Part2()
        {
            string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day5\puzzleInputTest.txt";
            var (startInterval, endInterval, number) = GetInputFromFile(filePath);
            long result = 0;

            var intervals = new List<(long start, long end)>();
            for (int i = 0; i < startInterval.Length; i++)
            {
                intervals.Add((startInterval[i], endInterval[i]));
            }
            intervals = intervals.OrderBy(i => i.start).ToList();

            List<(long start, long end)> mergeIntervalsTobeUnique = new();

            foreach (var interval in intervals)
            {
                if (mergeIntervalsTobeUnique.Count == 0 || interval.start > mergeIntervalsTobeUnique[mergeIntervalsTobeUnique.Count - 1].end + 1)
                {
                    mergeIntervalsTobeUnique.Add(interval);
                }
                else
                {
                    int lastIndex = mergeIntervalsTobeUnique.Count - 1;

                    mergeIntervalsTobeUnique[lastIndex] =
                        (mergeIntervalsTobeUnique[lastIndex].start,
                         Math.Max(mergeIntervalsTobeUnique[lastIndex].end, interval.end));
                }
            }

            foreach (var uniqueInterval in mergeIntervalsTobeUnique)
            {
                Console.WriteLine(uniqueInterval);
                result += uniqueInterval.end - uniqueInterval.start + 1;
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
