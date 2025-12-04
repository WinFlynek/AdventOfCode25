
using System.Collections;
using System.ComponentModel;
using System.IO.Pipelines;

namespace AdventOfCode25;

public class Day2_Part2
{
    public static void Part2()
    {
        string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day2\puzzleInput.txt";
        var (startInterval, endInterval) = GetInputFromFile(filePath);
        long result = 0;
        string checkSequence = null;
        for (int range = 0; range <= startInterval.Length - 1; range++){
            for (long numberInRange = startInterval[range]; numberInRange <= endInterval[range]; numberInRange++)
            {
                string numberToString = numberInRange.ToString();
                for (int charPosition = 1; charPosition <= numberToString.Length/2; charPosition++)
                {
                    string checkString = numberToString.Substring(0, charPosition);
                    int repeats = numberToString.Length / checkString.Length;

                    if (string.Concat(Enumerable.Repeat(checkString, repeats)) == numberToString)
                    {
                        checkSequence = checkString;
                        break;
                    }
                }
                if(checkSequence != null && checkSequence.Length != numberToString.Length && numberToString.Length % checkSequence.Length == 0){
                    {
                        bool isInvalid = true;
                        int index = 0;
                       while (index + checkSequence.Length <= numberToString.Length)
                        {

                           string part = numberToString.Substring(index, checkSequence.Length);
                            if (part != checkSequence)
                            {
                                isInvalid = false;
                                break;
                            }

                            index += checkSequence.Length;
                        }
                        if (isInvalid)
                        {
                            result +=numberInRange;
                        }     
                    }
                }
            }
        }
            Console.WriteLine($"Day2_Part2: {result}");
       
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