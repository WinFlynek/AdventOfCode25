
using System.IO.Pipelines;

namespace AdventOfCode25;

public class Day3_Part1
{
    public static void Part1()
    {
        string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day3\puzzleInput.txt";
        var lines = GetInputFromFile(filePath);
        long result = 0;
        List<string> joltage = new List<string> (); 
        foreach (string line in lines){
            int firstMax = 0;
            int indexOfMax = 0;
            int countInForeach = 0;
            foreach(char charNumber in line.Take(line.Length-1))
            {
                countInForeach++;
                if (int.Parse(charNumber.ToString()) > firstMax){
                    firstMax = int.Parse(charNumber.ToString());
                    indexOfMax = countInForeach;
                };
            }
            int secondMax = 0;
            foreach (char charNumber in line.Skip(indexOfMax))
            {
                if (int.Parse(charNumber.ToString()) > secondMax){
                    secondMax = int.Parse(charNumber.ToString());
                };
            }

            joltage.Add(firstMax.ToString() + secondMax.ToString());         
        }
        foreach (string jolt in joltage)
        {
            result += int.Parse(jolt);
        }
        Console.WriteLine($"Day3_Part2: {result}");
    }

    static string[] GetInputFromFile(string filePath)
        {
            var lines = new List<string>();

            foreach (string line in File.ReadLines(filePath))
            {
                string trimmRow = line.Trim();

                lines.Add(trimmRow);
            }

            return lines.ToArray();
        }
}