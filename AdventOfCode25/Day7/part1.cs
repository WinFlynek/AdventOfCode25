using System.IO.Pipelines;
using System;
using System.Collections.Generic;

namespace AdventOfCode25
{
    public class Day7_Part1
    {
        public static void Part1()
        {
            string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day7\puzzleInputTest.txt";
            var inputAsCharArray = GetInputFromFile(filePath);
            int result = 0;

            for (int row = 0; row < inputAsCharArray.Length; row++)
            {
                for (int col = 0; col < inputAsCharArray[row].Length; col++)
                {
                    if (inputAsCharArray[row][col] == 'S')
                    {
                        for (int r = row+1; r < inputAsCharArray.Length; r++)
                        {
                            if (col >= 0 && col < inputAsCharArray[r].Length)
                            {
                                if (inputAsCharArray[r][col] == '.'){
                                    inputAsCharArray[r][col] = '|';
                                    //result = result+2;
                                }
                                else
                                    break; // stop if blocked
                            }
                            else break; // out of bounds
                        }
                    }
                    if (inputAsCharArray[row][col] == '^' && inputAsCharArray[row-1][col] == '|')
                    {
                        result++;
                        bool keepLeft = true;
                        bool keepRight = true;
                        bool[] branched = [false, false];

                        for (int r = row; r < inputAsCharArray.Length; r++)
                        {
                            
                            // stop whole loop if neither side needs processing
                            if (!keepLeft && !keepRight) break;

                            // LEFT side
                            if (keepLeft)
                            {
                                int leftCol = col - 1;
                                if (leftCol < 0 || leftCol >= inputAsCharArray[r].Length)
                                {
                                    keepLeft = false; // out of bounds -> stop left
                                }
                                else if (inputAsCharArray[r][leftCol] == '.')
                                {
                                    inputAsCharArray[r][leftCol] = '|';
                                    if(r==row) branched[0] = true;
                                }
                                else
                                {
                                    keepLeft = false; // encountered non-dot -> stop left
                                }
                            }

                            // RIGHT side
                            if (keepRight)
                            {
                                int rightCol = col + 1;
                                if (rightCol < 0 || rightCol >= inputAsCharArray[r].Length)
                                {
                                    keepRight = false; // out of bounds -> stop right
                                }
                                else if (inputAsCharArray[r][rightCol] == '.')
                                {
                                    inputAsCharArray[r][rightCol] = '|';
                                    if(r==row) branched[1] = true;
                                }
                                else
                                {
                                    keepRight = false; // encountered non-dot -> stop right
                                }
                            }

                        }
                        // for (int show = 0; show < inputAsCharArray.Length; show++)
                        //     {
                        //          Console.WriteLine(new string(inputAsCharArray[show]));
                        //     }
                        //if(branched[0] && branched[1]) result=result+2;
                        //else if (branched[0] || branched[1] ) result=result+1;
                        //if (branched[1]) result=result+1;
                        //1307 -> too low 
                        //1659 -> too high
                        Console.WriteLine($"row: {row}: Split: {result}");
                    
                    }
                }
            }

            for (int row = 0; row < inputAsCharArray.Length; row++)
            {
                Console.WriteLine(new string(inputAsCharArray[row]));
            }

            Console.WriteLine($"Day7_Part1: {result}");
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
