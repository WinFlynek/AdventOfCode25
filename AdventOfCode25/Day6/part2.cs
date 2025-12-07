using System.IO.Pipelines;
using System;
using System.Collections.Generic;

namespace AdventOfCode25
{
    public class Day6_Part2
    {
        public static void Part2()
        {
            string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day6\puzzleInput.txt";
            var (numbers, operators) = GetInputFromFile(filePath);
            long finalResult = 0;
            int rows = numbers.Length;
            int cols = numbers[0].Length;

            List<List<string>> result = new List<List<string>>();
            //  Console.WriteLine("Parsed numbers:");
            // for (int row = 0; row < numbers.Length; row++)
            // {

            //     for (int col = 0; col < numbers[row].Length; col++)
            //     {
            //         Console.Write($"'{numbers[row][col]}' ");
            //     }
            //     Console.WriteLine();
            // }

            for (int col = 0; col < cols; col++)
            {
                int maxLength = 0;
                for (int row = 0; row < rows; row++)
                    maxLength = Math.Max(maxLength, numbers[row][col].Length);

                var columnOutput = new List<string>();

                for (int charIndex = 0; charIndex < maxLength; charIndex++)
                {
                    string built = "";

                    for (int row = 0; row < rows; row++)
                    {
                        string num = numbers[row][col];
                        char c = charIndex < num.Length ? num[charIndex] : '0';
                        if (c != '0')
                        {
                            built += c;
                        }
                    }

                    columnOutput.Add(built);
                }

                result.Add(columnOutput);
            }

            for (int col = 0; col < result.Count; col++)
            {
                char op = operators[col][0];

                long total = (op == '+') ? 0 : 1;

                foreach (string numStr in result[col])
                {
                    long value = long.Parse(numStr);

                    if (op == '+')
                        total += value;
                    else
                        total *= value;
                }
                finalResult += total;
            }

            Console.WriteLine($"Result: {finalResult}");
        }

        static (string[][] numbers, string[] operators) GetInputFromFile(string filePath)
        {
            var numberLines = new List<string[]>();
            string[] operators = null;
            int lineNumber = 0;
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                string trimmed = line;

                // Operators row
                if (trimmed.StartsWith("*") || trimmed.StartsWith("+"))
                {
                    operators = trimmed.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    continue;
                }

                var row = new List<string>();
                int rowCount = 0;
                string currentNumber = "";
                int startPosition = 0;
                int position = 0;
                foreach (char number in line)
                {
                    if (lineNumber == 0 && number == ' ')
                    {
                        for (int rowNumber = 1; rowNumber <= lines.Length - 2; rowNumber++)
                        {
                            {
                                char c = lines[rowNumber][position];

                                if (c != ' ')
                                {
                                    currentNumber += '0';
                                    break;
                                }
                                if (rowNumber == lines.Length - 2)
                                {
                                    row.Add(currentNumber);
                                    currentNumber = "";
                                }
                            }
                        }
                    }
                    else if (number != ' ')
                    {
                        currentNumber += number;
                        if (position == line.Length - 1)
                        {
                            row.Add(currentNumber);
                            currentNumber = "";
                        }
                    }
                    else if (lineNumber > 0)
                    {
                        if (numberLines[0][row.Count].Length == currentNumber.Length)
                        {

                            row.Add(currentNumber);
                            currentNumber = "";
                        }
                        else
                        {
                            currentNumber += '0';
                        }
                    }
                    //End of the line
                    if (line.Length - 1 == position)
                    {
                        if (position != line.Length - 1)
                        {

                            row.Add(currentNumber);
                            currentNumber = "";
                        }
                    }
                    position++;
                }

                lineNumber++;
                numberLines.Add(row.ToArray());
            }

            return (numberLines.ToArray(), operators);
        }
    }
}
