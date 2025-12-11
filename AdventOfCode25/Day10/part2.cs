using System.IO.Pipelines;
using System;
using System.Collections.Generic;
using System.Data;

namespace AdventOfCode25
{
    public class Day10_Part2
    {
        public static void Part1()
        {
            string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day10\puzzleInputTest.txt";
            List<long[]> points = GetInputFromFile(filePath);
            long result = 0;
            
            Console.WriteLine(result);
        }

        static List<long[]> GetInputFromFile(string filePath)
        {
            var list = new List<long[]>();

            foreach (string line in File.ReadLines(filePath))
            {
                long[] nums = line
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => long.Parse(s.Trim()))
                    .ToArray();

                list.Add(nums);
            }

            return list;
        }
    }
}
