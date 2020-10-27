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

            var isEndgameNear = heaps.Count(x => x > 1) <= 1;

            if (isEndgameNear)
            {
                var isOdd = (heaps.Count(x => x > 0) % 2) == 1;
                var maxHeapSize = heaps.Max();
                var indexOfMaxHeapSize = heaps.ToList().FindIndex(x => x == maxHeapSize);
                if (maxHeapSize == 1 && isOdd)
                {
                    return null;
                }

                return (indexOfMaxHeapSize, maxHeapSize - (isOdd ? 1 : 0));
            }

            var nimSum = NimSum(heaps);

            if (nimSum == 0)
            {
                return null;
            }

            for (var i = 0; i < heaps.Length; i++)
            {
                var heapSize = heaps[i];
                var targetSize = Convert.ToInt32(heapSize ^ nimSum);
                if (targetSize < heapSize)
                {
                    return (i, heapSize - targetSize);
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
