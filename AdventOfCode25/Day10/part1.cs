using System.IO.Pipelines;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace AdventOfCode25
{
    public class Day10_Part1
    {
        public static void Part1()
        {
            string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day10\puzzleInput.txt";
            var (patterns, groups, finals) = GetInputFromFile(filePath);
            var result = 0;
            int indexOfLine = 0;

            long total = 0;

            for (int i = 0; i < patterns.Count; i++)
            {
                int presses = MachineSolver.SolveMachine(patterns[i], groups[i]);
                Console.WriteLine($"Machine {i} → min presses = {presses}");
                total += presses;
            }

            Console.WriteLine($"TOTAL = {total}");





            // for(int line = 0; line <= patterns.Count()-1; line++)
            // {
            //     Console.Write($"Line: {line}, Array: ");
            //     Console.Write("[");
            //     foreach (char character in patterns[line])
            //     {

            //         Console.Write(character);
            //     }
            //     Console.WriteLine("]");
            // }

            // for(int line = 0; line <= groups.Count()-1; line++)
            // {
            //     Console.Write($"Line: {line}, Array: ");
            //     Console.Write("[");
            //     foreach (int[] character in groups[line])
            //     {   
            //         foreach(int number in character){
            //         Console.Write(number);
            //         }
            //     }
            //     Console.WriteLine("]");
            // }
        }

        public static int PatternToMask(List<char> pattern)
        {
            int mask = 0;
            for (int i = 0; i < pattern.Count; i++)
            {
                if (pattern[i] == '#')
                    mask |= (1 << i);
            }
            return mask;
        }

        // Converts click (e.g. [1,3]) to bitmask 1010
        static int ClickToMask(int[] click)
        {
            int mask = 0;
            foreach (int pos in click)
                mask |= (1 << pos);
            return mask;
        }

        // Solve: find subset of clicks whose XOR makes pattern all dots
        public static List<int> Solve(List<char> startPattern, List<int[]> clicks)
        {
            int target = 0; // all dots
            int startMask = PatternToMask(startPattern);

            int n = clicks.Count;
            int[] clickMasks = clicks.Select(ClickToMask).ToArray();

            // Try all subsets: 2^n
            for (int subset = 0; subset < (1 << n); subset++)
            {
                int combined = startMask;

                for (int i = 0; i < n; i++)
                {
                    if ((subset & (1 << i)) != 0)
                    {
                        combined ^= clickMasks[i];
                    }
                }

                if (combined == target)
                {
                    // We found a valid set of clicks
                    List<int> result = new List<int>();
                    for (int i = 0; i < n; i++)
                        if ((subset & (1 << i)) != 0)
                            result.Add(i);

                    return result;
                }
            }

            // No solution
            return new List<int>();
        }


        public static (List<List<char>> patterns, List<List<int[]>> groups, List<int[]> finals) GetInputFromFile(
      string path)
        {
            List<List<char>> patterns = new List<List<char>>();
            List<List<int[]>> groups = new List<List<int[]>>();
            List<int[]> finals = new List<int[]>();

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                // --- 1) Pattern inside [ ... ] ---
                int a = line.IndexOf('[') + 1;
                int b = line.IndexOf(']');
                string pattern = line.Substring(a, b - a);
                patterns.Add(pattern.ToCharArray().ToList());

                // Remaining text
                string rest = line.Substring(b + 1).Trim();

                // --- 2) Groups inside (...) ---
                var listOfGroups = new List<int[]>();
                var matches = Regex.Matches(rest, @"\((.*?)\)");

                foreach (Match m in matches)
                {
                    string content = m.Groups[1].Value.Trim();

                    if (content == "")
                    {
                        listOfGroups.Add(Array.Empty<int>());
                    }
                    else
                    {
                        int[] nums = content.Split(',')
                                            .Select(x => int.Parse(x.Trim()))
                                            .ToArray();
                        listOfGroups.Add(nums);
                    }
                }
                groups.Add(listOfGroups);

                // --- 3) Final block inside { ... } ---
                var lastMatch = Regex.Match(rest, @"\{(.*?)\}");
                int[] finalNums = lastMatch.Groups[1].Value
                                           .Split(',')
                                           .Select(x => int.Parse(x.Trim()))
                                           .ToArray();
                finals.Add(finalNums);
            }
            return (patterns, groups, finals);
        }
    }
}
