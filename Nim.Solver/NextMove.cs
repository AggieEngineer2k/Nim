// <copyright file="NextMove.cs" company="AggieEngineer2K">
// Copyright (c) AggieEngineer2K. All rights reserved.
// </copyright>

namespace Nim.Solver
{
    using System;
    using System.Linq;

    /// <summary>
    /// A next-move solver.
    /// </summary>
    public static class NextMove
    {
        /// <summary>
        /// Solves the heap.
        /// </summary>
        /// <param name="heaps">The remaining heaps.</param>
        /// <returns>The number to take from a heap.</returns>
        public static (int heap, int number)? Solve(int[] heaps)
        {
            if (heaps == null)
            {
                throw new ArgumentNullException(nameof(heaps));
            }

            var nimSum = NimSum(heaps);

            // Return winning moves for known winning positions.
            if (heaps.Length == 1 && heaps[0] > 1)
            {
                return (0, heaps[0] - 1);
            }
            else if (heaps.All(x => x == 1) && heaps.Length % 2 == 0)
            {
                return (0, 1);
            }
            else if (heaps.Length == 2 && heaps.Where(x => x == 1).Count() == 1)
            {
                for (var i = 0; i < heaps.Length; i++)
                {
                    if (heaps[i] > 1)
                    {
                        return (i, heaps[i]);
                    }
                }
            }

            // Return null on a losing position.
            if (nimSum == 0)
            {
                return null;
            }
            else if (heaps.All(x => x == 1) && heaps.Length % 2 == 1)
            {
                return null;
            }

            // Determine a winning move by removing the Nim sum from a heap.
            for (var i = 0; i < heaps.Length; i++)
            {
                var remove = Convert.ToInt32(nimSum);
                if (heaps[i] < remove)
                {
                    continue;
                }

                var modified = (int[])heaps.Clone();
                modified[i] -= remove;
                if (NimSum(modified) == 0)
                {
                    return (i, remove);
                }
            }

            // A move could not be found.
            return null;
        }

        /// <summary>
        /// Computes the binary digital sum of the heap sizes.
        /// </summary>
        /// <param name="heaps">The number of objects in each heap.</param>
        /// <returns>The Nim sum.</returns>
        private static uint NimSum(int[] heaps)
        {
            uint nimSum = 0;

            for (var i = 0; i < heaps.Length; i++)
            {
                nimSum ^= Convert.ToUInt32(heaps[i]);
            }

            return nimSum;
        }
    }
}
