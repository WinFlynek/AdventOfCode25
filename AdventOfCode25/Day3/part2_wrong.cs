
using System.IO.Pipelines;

namespace AdventOfCode25;

public class Day3_Part2_Wrong
{
    public static void Part2()
    {
        string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day3\puzzleInputTest.txt";
        var lines = GetInputFromFile(filePath);
        long result = 0;
        List<string> joltage = new List<string> (); 

        int numberOfBatteries = 12;
        foreach (string line in lines){
            List<int> max = new List<int>(new int[numberOfBatteries]);
            int indexOfMax = 0;

            for (int batteryIndex = 0; batteryIndex < numberOfBatteries; batteryIndex++){
                int maxIndex = 0;

                foreach (var (charNumber, indexInLine) in 
                    line.Select((c, i) => (c, i)).Skip(indexOfMax).Take(line.Length - (numberOfBatteries - batteryIndex - 1) - indexOfMax))
                {     
                    if (int.Parse(charNumber.ToString()) > max[batteryIndex]){
                        max[batteryIndex] = int.Parse(charNumber.ToString());
                        maxIndex = indexInLine;
                    };
                }
                indexOfMax = maxIndex+1;
        }
        joltage.Add(string.Join("", max));
    }
        
        foreach (string jolt in joltage)
        {
            result += long.Parse(jolt);
        }
        
        //Part 1 result: 17193
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