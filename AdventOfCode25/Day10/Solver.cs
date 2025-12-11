public static class MachineSolver
{
    public static int SolveMachine(List<char> pattern, List<int[]> buttons)
    {
        int n = pattern.Count;      // number of lights
        int m = buttons.Count;      // number of buttons

        // Convert pattern [.##.] → vector [0,1,1,0]
        int[] target = pattern.Select(c => c == '#' ? 1 : 0).ToArray();

        // Build matrix A (m buttons × n lights)
        int[,] A = new int[m, n];
        for (int i = 0; i < m; i++)
            foreach (int idx in buttons[i])
                A[i, idx] = 1;

        // Solve Aᵀ * x = target   over GF(2)
        return SolveGF2MinPress(A, target);
    }

    // Solve binary linear system and return minimal number of presses
    private static int SolveGF2MinPress(int[,] A, int[] target)
    {
        int m = A.GetLength(0);
        int n = A.GetLength(1);

        // Build augmented matrix [A | target]
        int[,] M = new int[n, m + 1];

        // Transpose A so system is lights × buttons
        for (int i = 0; i < n; i++)
            for (int b = 0; b < m; b++)
                M[i, b] = A[b, i];

        for (int i = 0; i < n; i++)
            M[i, m] = target[i];

        // Gaussian elimination over GF(2)
        int row = 0;
        List<int> pivotCol = new List<int>();

        for (int col = 0; col < m && row < n; col++)
        {
            int sel = row;
            while (sel < n && M[sel, col] == 0) sel++;

            if (sel == n) continue;

            // Swap
            for (int k = col; k <= m; k++)
                (M[row, k], M[sel, k]) = (M[sel, k], M[row, k]);

            pivotCol.Add(col);

            // Eliminate
            for (int r = 0; r < n; r++)
            {
                if (r != row && M[r, col] == 1)
                {
                    for (int k = col; k <= m; k++)
                        M[r, k] ^= M[row, k];
                }
            }
            row++;
        }

        // Check consistency
        for (int r = row; r < n; r++)
            if (M[r, m] == 1)
                return int.MaxValue; // no solution

        // Identify free variables
        HashSet<int> pivotSet = pivotCol.ToHashSet();
        List<int> freeVars = new List<int>();
        for (int col = 0; col < m; col++)
            if (!pivotSet.Contains(col))
                freeVars.Add(col);

        // Evaluate all combinations of free vars → find minimal presses
        int best = int.MaxValue;
        int freeCount = freeVars.Count;
        int combos = 1 << freeCount;

        for (int mask = 0; mask < combos; mask++)
        {
            int[] x = new int[m];

            // Assign free vars
            for (int i = 0; i < freeCount; i++)
                x[freeVars[i]] = (mask >> i) & 1;

            // Back-substitute
            for (int i = pivotCol.Count - 1; i >= 0; i--)
            {
                int col = pivotCol[i];

                int sum = M[i, m];
                for (int c = col + 1; c < m; c++)
                    if (M[i, c] == 1)
                        sum ^= x[c];

                x[col] = sum;
            }

            // Count presses
            int presses = x.Sum();

            if (presses < best)
                best = presses;
        }

        return best;
    }
}
