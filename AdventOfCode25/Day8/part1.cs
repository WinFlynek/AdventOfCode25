using System.IO.Pipelines;
using System;
using System.Collections.Generic;

namespace AdventOfCode25
{
    public class Day8_Part1
    {
        public record DistanceEntry(double Distance, int[] A, int[] B);

        public static void Part1()
        {
            string filePath = @"C:\Users\michal.skocik\source\repos\AdventOfCode25\AdventOfCode25\Day8\puzzleInputTest.txt";
             List<int[]> points = GetInputFromFile(filePath);
            List<DistanceEntry> allDistances = new List<DistanceEntry>();
            List<HashSet<int>> circuits = new List<HashSet<int>>();


double bestDistance = double.MaxValue;
int[] bestA = null!;
int[] bestB = null!;
int[] c = points[0];

for (int i = 0; i < points.Count; i++)
{
    for (int j = i + 1; j < points.Count; j++)
    {
        double d = Distance3D(points[i], points[j]);

        allDistances.Add(new DistanceEntry(d, points[i], points[j]));

        if (d < bestDistance)
        {
            bestDistance = d;
            bestA = points[i];
            bestB = points[j];
        }
    }
}

// sort
var sortedDistances = allDistances.OrderBy(x => x.Distance).ToList();



// sample output
foreach (var entry in sortedDistances)
{
    Console.WriteLine(
        $"d={entry.Distance:F3} | A=[{entry.A[0]}, {entry.A[1]}, {entry.A[2]}] | B=[{entry.B[0]}, {entry.B[1]}, {entry.B[2]}]");
}

        // Console.WriteLine(IsPointBetween(bestA, bestB,c));
        // Console.WriteLine($"Closest distance = {bestDistance}");
        // Console.WriteLine($"Point A = [{bestA[0]}, {bestA[1]}, {bestA[2]}]");
        // Console.WriteLine($"Point B = [{bestB[0]}, {bestB[1]}, {bestB[2]}]");
    }

    //Fist attemt
    static double Distance3D(int[] a, int[] b)
    {
        long dx = a[0] - b[0];
        long dy = a[1] - b[1];
        long dz = a[2] - b[2];

        return Math.Sqrt(dx*dx + dy*dy + dz*dz);
    }

    static bool IsPointBetween(int[] A, int[] B, int[] C)
{
    // Convert to vector differences
    int ABx = B[0] - A[0];
    int ABy = B[1] - A[1];
    int ABz = B[2] - A[2];

    int ACx = C[0] - A[0];
    int ACy = C[1] - A[1];
    int ACz = C[2] - A[2];

    // --- 1) Check collinearity using cross product ---
    int crossX = ABy * ACz - ABz * ACy;
    int crossY = ABz * ACx - ABx * ACz;
    int crossZ = ABx * ACy - ABy * ACx;

    // If cross != 0 → not on same line
    if (crossX != 0 || crossY != 0 || crossZ != 0)
        return false;

    // --- 2) Dot product should be within segment range ---
    int dot = ACx * ABx + ACy * ABy + ACz * ABz;

    // C is before A
    if (dot < 0)
        return false;

    int AB_len_sq = ABx * ABx + ABy * ABy + ABz * ABz;

    // C is past B
    if (dot > AB_len_sq)
        return false;

    return true;
}


        static List<int[]> GetInputFromFile(string filePath)
        {
            var inputList = new List<int[]>();

            foreach (string line in File.ReadLines(filePath))
            {
                int[] numbers = line
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x.Trim()))
            .ToArray();

                inputList.Add(numbers);
            }

            return inputList;
        }
    }
}
