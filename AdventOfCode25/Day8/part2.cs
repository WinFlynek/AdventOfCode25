using System.IO.Pipelines;
using System;
using System.Collections.Generic;

namespace AdventOfCode25
{
    public class Day8_Part2
    {
        public static void Part2()
        {
            string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day8\puzzleInputTest.txt";
            var inputAsCharArray = GetInputFromFile(filePath);
            
            Console.WriteLine($"Day7_Part1: ");
        }
        static char[][] GetInputFromFile(string filePath)
        {
            var inputList = new List<char[]>();

            foreach (string line in File.ReadLines(filePath))
            {
                inputList.Add(line.ToCharArray());
            }

            return inputList.ToArray();
        }
    }
}
