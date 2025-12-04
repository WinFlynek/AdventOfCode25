
using System.IO.Pipelines;

namespace AdventOfCode25;

public class Day3_Part2
{
public static void Part2()
{
    string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day3\puzzleInput.txt";
    var lines = GetInputFromFile(filePath);
    long result = 0;
    int numberOfBatteries = 12;
    List<string> maxDigitsString = new List<string>();
    foreach (string line in lines)
    {
        List<int> maxDigits = new List<int>();
        int startIndex = 0;

        for (int battery = 0; battery < numberOfBatteries; battery++)
        {
            int remainingBatteries = numberOfBatteries - battery - 1;

            // compute sliding window end
            int endIndex = line.Length - remainingBatteries;

            int bestDigit = -1;
            int bestIndex = startIndex;

            // scan window
            for (int i = startIndex; i < endIndex; i++)
            {
                int digit = line[i] - '0';
                if (digit > bestDigit)
                {
                    bestDigit = digit;
                    bestIndex = i;
                }
            }

            maxDigits.Add(bestDigit);

            // next window starts AFTER best digit
            startIndex = bestIndex + 1;
        }
        //Console.WriteLine(string.Join("", maxDigits));
        maxDigitsString.Add(string.Join("", maxDigits));
    }
    foreach (string digit in maxDigitsString)
        {
            result += long.Parse(digit);
        }
        Console.WriteLine(result);
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