using System.IO.Pipelines;
using System;
using System.Collections.Generic;

namespace AdventOfCode25
{
    public class Day4_Part1
    {
        public static void Part1()
        {
            string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day4\puzzleInputtest.txt";
            List<string> lines = GetInputFromFile(filePath);
            int result = 0;
            int maxDistance = 8; // number of characters to check downward
    int numRows = lines.Count;

    for (int line = 0; line < lines.Count; line++)
    {
        char[] chars = lines[line].ToCharArray(); // make line editable
        for (int charIndexInLine = 0; charIndexInLine < lines[line].Length; charIndexInLine++)
        {
            char charValue = chars[charIndexInLine];

            if (charValue == '@')
            {
                bool changeChar = false;
                
                //Verify right
                if(chars.Length-charIndexInLine > 8){
                    for(int rightCheckIndex = charIndexInLine; rightCheckIndex < charIndexInLine+8; rightCheckIndex++)
                    {
                        char charRightValue = chars[rightCheckIndex];
                        if(charRightValue != '@')
                        {
                            changeChar = false;
                            break;
                        }
                        else
                        {
                            changeChar = true;
                        }
                    }
                        if (changeChar)
                        {
                            chars[charIndexInLine] = '#';
                        }
                }
                //Verify Left
                if(charIndexInLine-8 >= 0){
                    for(int leftCheckIndex = charIndexInLine; leftCheckIndex > charIndexInLine-8; leftCheckIndex--)
                    {
                        char charRightValue = chars[leftCheckIndex];
                        if(charRightValue != '@')
                        {
                            changeChar = false;
                            break;
                        }
                        else
                        {
                            changeChar = true;
                        }
                    }
                        if (changeChar)
                        {
                            chars[charIndexInLine] = '#';
                        }
                }
                //Verify Down
                if (line + 8 < numRows)
                {
                    changeChar = true;

                    for (int downCheckIndex = 0; downCheckIndex < 8; downCheckIndex++)
                    {
                        char charDownValue = lines[line + downCheckIndex][charIndexInLine];

                        if (charDownValue != '@')
                        {
                            changeChar = false;
                            break;
                        }
                    }

                    if (changeChar)
                    {
                        chars[charIndexInLine] = '#';
                    }
                }
                // Verify Up
                if (line - 8 >= 0)
                {
                    changeChar = true;

                    for (int offset = 0; offset < 8; offset++)
                    {
                        char charUpValue = lines[line - offset][charIndexInLine];

                        if (charUpValue != '@')
                        {
                            changeChar = false;
                            break;
                        }
                    }

                    if (changeChar)
                    {
                        chars[charIndexInLine] = '#';
                    }
                }
                // Verify Down-Right (↘)
                if (line + 8 <= numRows - 1 && charIndexInLine + 8 <= chars.Length - 1)
{
    changeChar = true;

    for (int offset = 0; offset < 8; offset++)
    {
        char c = lines[line + offset][charIndexInLine + offset];
        if (c != '@')
        {
            changeChar = false;
            break;
        }
    }

    if (changeChar)
    {
        chars[charIndexInLine] = '#';
    }
}
                // Verify Down-Left (↙)
                if (line + 8 <= numRows - 1 && charIndexInLine - 8 >= 0)
{
    changeChar = true;

    for (int offset = 0; offset < 8; offset++)
    {
        char c = lines[line + offset][charIndexInLine - offset];
        if (c != '@')
        {
            changeChar = false;
            break;
        }
    }

    if (changeChar)
    {
        chars[charIndexInLine] = '#';
    }
}
                // Verify Up-Right (↗)
                if (line - 8 >= 0 && charIndexInLine + 8 <= chars.Length - 1)
{
    changeChar = true;

    for (int offset = 0; offset < 8; offset++)
    {
        char c = lines[line - offset][charIndexInLine + offset];
        if (c != '@')
        {
            changeChar = false;
            break;
        }
    }

    if (changeChar)
    {
        chars[charIndexInLine] = '#';
    }
}
                // Verify Up-Left (↖)
                if (line - 8 >= 0 && charIndexInLine - 8 >= 0)
{
    changeChar = true;

    for (int offset = 0; offset < 8; offset++)
    {
        char c = lines[line - offset][charIndexInLine - offset];
        if (c != '@')
        {
            changeChar = false;
            break;
        }
    }

    if (changeChar)
    {
        chars[charIndexInLine] = '#';
    }
}

            }
        }

        // Save changes back to line
        lines[line] = new string(chars);
    }

    // Print modified lines
    foreach (var modifiedLine in lines)
    {
        Console.WriteLine(modifiedLine);
    }

    Console.WriteLine($"Day4_Part1: {result}");
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
