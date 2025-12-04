
namespace AdventOfCode25;

public class Day1_Part1
{
    public static void Part1()
    {
        string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day1\puzzleInput.txt";

        int startPoint = 50;
        int passwordNumber = 0;
        var (rotations, numbers) = GetInputFromFile(filePath);
        for (int row=0; row <= rotations.Length-1; row++)
        {
            if (rotations[row] == "L")
            {
                startPoint -= numbers[row];
                while (startPoint < 0)
                    startPoint += 100;
            }
            if (rotations[row] == "R")
            {
                startPoint += numbers[row];
                while (startPoint >= 100)
                    startPoint -= 100;
            }

            if(startPoint == 0)
            {
                passwordNumber++;
            }
        }
       Console.WriteLine($"Day1_Part1: {passwordNumber}");
    }

    static (string[] rotation, int[] number) GetInputFromFile(string filePath)
        {
            var rotations = new List<string>();
            var numbers = new List<int>();

            foreach (string line in File.ReadLines(filePath))
            {
                string trimmRow = line.Trim();

                string rotation = trimmRow[0].ToString();
                int number = int.Parse(trimmRow.Substring(1));

                    rotations.Add(rotation);
                    numbers.Add(number);
                
            }

            return (rotations.ToArray(), numbers.ToArray());
        }
}