using System.IO.Pipelines;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode25
{
    public class Day4_Part1
    {
        public static void Part1()
        {
            string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day4\puzzleInput.txt";
            List<string> lines = GetInputFromFile(filePath);
            int result = 0;
            int maxDistance = 1; // number of characters to check downward

            int numRows = lines.Count;

            for (int line = 0; line < lines.Count; line++)
            {
                char[] chars = lines[line].ToCharArray();
                for (int charIndexInLine = 0; charIndexInLine < lines[line].Length; charIndexInLine++)
                {
                    char charValue = chars[charIndexInLine];
                    int count = 0;
                    if (charValue == '@')
                    {
                        //Verify right
                        if (chars.Length - charIndexInLine > maxDistance)
                        {
                                char charRightValue = lines[line][charIndexInLine+maxDistance];
                                if (verifyValue(charRightValue))
                                {
                                    count++;
                                }
                        }
                        //Verify Left
                        if (charIndexInLine - maxDistance >= 0)
                        {
                                char charRightValue = lines[line][charIndexInLine-maxDistance];
                                if (verifyValue(charRightValue))
                                {
                                    count++;
                                }
                        }
                        //Verify Down
                        if (line + maxDistance < numRows)
                        {
                                char charDownValue = lines[line + maxDistance][charIndexInLine];
                                if (verifyValue(charDownValue))
                                {
                                    count++;
                                }
                        }
                        // Verify Up
                        if (line - maxDistance >= 0)
                        {
                                char charUpValue = lines[line - maxDistance][charIndexInLine];

                                if (verifyValue(charUpValue))
                                {
                                    count++;
                                }                         
                        }
                        // Verify Down-Right (↘)
                        if (line + maxDistance <= numRows - 1 && charIndexInLine + maxDistance <= chars.Length - 1)
                        {
                                char c = lines[line + maxDistance][charIndexInLine + maxDistance];
                                if (verifyValue(c))
                                {
                                    count++;
                                }
                        }
                        // Verify Down-Left (↙)
                        if (line + maxDistance <= numRows - 1 && charIndexInLine - maxDistance >= 0)
                        {
                                char c = lines[line + maxDistance][charIndexInLine - maxDistance];
                                if (verifyValue(c))
                                {
                                    count++;
                                }
                        }
                        // Verify Up-Right (↗)
                        if (line - maxDistance >= 0 && charIndexInLine + maxDistance <= chars.Length -1)
                        {
                                char c = lines[line - maxDistance][charIndexInLine + maxDistance];
                                if (verifyValue(c))
                                {
                                    count++;
                                }
                        }
                        // Verify Up-Left (↖)
                        if (line - maxDistance >= 0 && charIndexInLine - maxDistance >= 0)
                        {
                                char c = lines[line - maxDistance][charIndexInLine - maxDistance];
                                if (verifyValue(c))
                                {
                                    count++;
                                }
                        }
                        Console.WriteLine(count);
                        if(count < 4)
                        {
                            result++;
                            chars[charIndexInLine] = 'x';
                        }
                    }
                }
                // Save changes back to line
                lines[line] = new string(chars);
            }

            // Print modified lines
            // foreach (var modifiedLine in lines)
            // {
            //     Console.WriteLine(modifiedLine);
            // }

            Console.WriteLine($"Day4_Part1: {result}");
        }

        static bool verifyValue(char charCheck)
        {
            bool changeChar = false;
            if (charCheck == '@' || charCheck == 'x')
            {
                changeChar = true;
            }
            else
            {
                changeChar = false;
            }
            return changeChar;
        }

        static List<string> GetInputFromFile(string filePath)
        {
            var lines = new List<string>();

            foreach (string line in File.ReadLines(filePath))
            {
                lines.Add(line.Trim());
            }

            return lines;
        }
    }
}
