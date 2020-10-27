// <copyright file="Move.cs" company="AggieEngineer2K">
// Copyright (c) AggieEngineer2K. All rights reserved.
// </copyright>

namespace Nim.Solver
{
    /// <summary>
    /// Represents a move.
    /// </summary>
    public class Move
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Move"/> class.
        /// </summary>
        /// <param name="heap">The zero-indexed heap number.</param>
        /// <param name="number">The number of objects to remove.</param>
        public Move(int heap, int number)
        {
            this.Heap = heap;
            this.Number = number;
        }

        /// <summary>
        /// Gets the zero-indexed heap number.
        /// </summary>
        public int Heap { get; private set; }

        /// <summary>
        /// Gets the number of objects to remove.
        /// </summary>
        public int Number { get; private set; }
    }
}
