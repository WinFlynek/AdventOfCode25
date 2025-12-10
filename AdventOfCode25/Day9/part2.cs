using System.IO.Pipelines;
using System;
using System.Collections.Generic;
using System.Data;

namespace AdventOfCode25
{
    public class Day9_Part2
    {
        public static void Part1()
        {
            string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day9\puzzleInput.txt";
            List<long[]> points = GetInputFromFile(filePath);
            long result = 0;
            for (int lineIndex = 0; lineIndex <= points.Count - 1; lineIndex++)
            {

                for (int index = 1; index <= points.Count - 1; index++)
                {

                    long count = CharactersBetween(points[lineIndex], points[index]);
                    if (result < count)
                    {
                        result = count;
                    }
                }
            }
            
            Console.WriteLine(result);
        }

        static long CharactersBetween(long[] A, long[] B)
        {
            long x1 = A[0], y1 = A[1];
            long x2 = B[0], y2 = B[1];

            // Horizontal
            long row = Math.Abs(x1 - x2) + 1;

            // Vertical
            long column = Math.Abs(y1 - y2) + 1;

            return row * column;
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
