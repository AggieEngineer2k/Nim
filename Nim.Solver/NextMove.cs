// <copyright file="NextMove.cs" company="AggieEngineer2K">
// Copyright (c) AggieEngineer2K. All rights reserved.
// </copyright>

namespace Nim.Solver
{
    using System;
    using System.Collections.Generic;
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
        public static Move Solve(ICollection<int> heaps)
        {
            if (heaps == null)
            {
                throw new ArgumentNullException(nameof(heaps));
            }

            // When at most one heap has more than one object left, use the endgame logic.
            var isEndgameNear = heaps.Count(x => x > 1) <= 1;
            if (isEndgameNear)
            {
                var isOdd = (heaps.Count(x => x > 0) % 2) == 1;
                var maxHeapSize = heaps.Max();
                var indexOfMaxHeapSize = heaps.ToList().FindIndex(x => x == maxHeapSize);

                // An odd number of heaps with at most one object is a losing position.
                if (maxHeapSize == 1 && isOdd)
                {
                    return null;
                }

                // The winning move is to leave an even number of heaps with one object.
                return new Move(indexOfMaxHeapSize, maxHeapSize - (isOdd ? 1 : 0));
            }

            // Calculate the "Nim sum" of the heap sizes.
            var nimSum = NimSum(heaps);

            // A Nim sum of zero is a losing position.
            if (nimSum == 0)
            {
                return null;
            }

            // Check each heap size to see if its binary digital sum with the Nim sum would be smaller.
            // Once a heap is found, the winning move is to reduce that heap to the binary digital sum.
            for (var i = 0; i < heaps.Count; i++)
            {
                var heapSize = heaps.ElementAt(i);
                var targetSize = Convert.ToInt32(heapSize ^ nimSum);
                if (targetSize < heapSize)
                {
                    return new Move(i, heapSize - targetSize);
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
        private static uint NimSum(ICollection<int> heaps)
        {
            uint nimSum = 0;

            for (var i = 0; i < heaps.Count; i++)
            {
                nimSum ^= Convert.ToUInt32(heaps.ElementAt(i));
            }

            return nimSum;
        }
    }
}
