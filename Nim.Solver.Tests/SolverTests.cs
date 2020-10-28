// <copyright file="SolverTests.cs" company="AggieEngineer2K">
// Copyright (c) AggieEngineer2K. All rights reserved.
// </copyright>

namespace Nim.Solver.Tests
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for the <see cref="NextMove"/> class.
    /// </summary>
    [TestClass]
    public class SolverTests
    {
        /// <summary>
        /// Passing a null heaps collection will return null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AssertThatNullHeapsThrowAnException()
        {
            NextMove.Solve(null);
        }

        /// <summary>
        /// Assert that the solver returns no next moves for losing positions.
        /// </summary>
        /// <param name="heaps">The heap sizes.</param>
        [DataRow(new[] { 0, 0, 1 }, DisplayName = "Losing { 0, 0, 1 }")]
        [DataRow(new[] { 0, 2, 2 }, DisplayName = "Losing { 0, 2, 2 }")]
        [DataRow(new[] { 1, 1, 1 }, DisplayName = "Losing { 1, 1, 1 }")]
        [DataRow(new[] { 1, 2, 3 }, DisplayName = "Losing { 1, 2, 3 }")]
        [DataRow(new[] { 1, 4, 5 }, DisplayName = "Losing { 1, 4, 5 }")]
        [DataRow(new[] { 1, 6, 7 }, DisplayName = "Losing { 1, 6, 7 }")]
        [DataRow(new[] { 1, 8, 9 }, DisplayName = "Losing { 1, 8, 9 }")]
        [DataRow(new[] { 2, 4, 6 }, DisplayName = "Losing { 2, 4, 6 }")]
        [DataRow(new[] { 2, 5, 7 }, DisplayName = "Losing { 2, 5, 7 }")]
        [DataRow(new[] { 3, 4, 7 }, DisplayName = "Losing { 3, 4, 7 }")]
        [DataRow(new[] { 3, 5, 6 }, DisplayName = "Losing { 3, 5, 6 }")]
        [DataRow(new[] { 4, 8, 12 }, DisplayName = "Losing { 4, 8, 12 }")]
        [DataRow(new[] { 4, 9, 13 }, DisplayName = "Losing { 4, 9, 13 }")]
        [DataRow(new[] { 5, 8, 13 }, DisplayName = "Losing { 5, 8, 13 }")]
        [DataRow(new[] { 5, 9, 12 }, DisplayName = "Losing { 5, 9, 12 }")]
        [DataRow(new[] { 1, 1, 2, 2 }, DisplayName = "Losing { 1, 1, 2, 2 }")]
        [DataRow(new[] { 1, 2, 4, 7 }, DisplayName = "Losing { 1, 2, 4, 7 }")]
        [DataRow(new[] { 1, 2, 5, 6 }, DisplayName = "Losing { 1, 2, 5, 6 }")]
        [DataRow(new[] { 1, 3, 4, 6 }, DisplayName = "Losing { 1, 3, 4, 6 }")]
        [DataRow(new[] { 1, 3, 5, 7 }, DisplayName = "Losing { 1, 3, 5, 7 }")]
        [DataRow(new[] { 2, 2, 2, 2 }, DisplayName = "Losing { 2, 2, 2, 2 }")]
        [DataRow(new[] { 2, 2, 3, 3 }, DisplayName = "Losing { 2, 2, 3, 3 }")]
        [DataRow(new[] { 2, 3, 4, 5 }, DisplayName = "Losing { 2, 3, 4, 5 }")]
        [DataRow(new[] { 2, 3, 6, 7 }, DisplayName = "Losing { 2, 3, 6, 7 }")]
        [DataRow(new[] { 2, 3, 8, 9 }, DisplayName = "Losing { 2, 3, 8, 9 }")]
        [DataRow(new[] { 4, 5, 6, 7 }, DisplayName = "Losing { 4, 5, 6, 7 }")]
        [DataRow(new[] { 4, 5, 8, 9 }, DisplayName = "Losing { 4, 5, 8, 9 }")]
        [DataTestMethod]
        public void AssertThatSolverReturnsNoMove(int[] heaps)
        {
            // Arrange

            // Act
            var actual = NextMove.Solve(heaps);

            // Assert
            actual.Should().BeNull();
        }

        /// <summary>
        /// Assert that the solver returns the expected next moves for winning positions.
        /// </summary>
        /// <param name="heaps">The heap sizes.</param>
        /// <param name="heap">The next move's heap.</param>
        /// <param name="number">The next move's number.</param>
        [DataRow(new[] { 0, 1, 1 }, 1, 1, DisplayName = "Winning { 0, 1, 1 }")]
        [DataRow(new[] { 0, 0, 2 }, 2, 1, DisplayName = "Winning { 0, 0, 2 }")]
        [DataRow(new[] { 0, 1, 2 }, 2, 2, DisplayName = "Winning { 0, 1, 2 }")]
        [DataRow(new[] { 1, 1, 2 }, 2, 1, DisplayName = "Winning { 1, 1, 2 }")]
        [DataRow(new[] { 1, 2, 2 }, 0, 1, DisplayName = "Winning { 1, 2, 2 }")]
        [DataRow(new[] { 2, 2, 2 }, 0, 2, DisplayName = "Winning { 2, 2, 2 }")]
        [DataRow(new[] { 3, 4, 5 }, 0, 2, DisplayName = "Winning { 3, 4, 5 }")]
        [DataRow(new[] { 3, 5, 7 }, 0, 1, DisplayName = "Winning { 3, 5, 7 }")]
        [DataRow(new[] { 1, 1, 1, 1 }, 0, 1, DisplayName = "Winning { 1, 1, 1, 1 }")]
        [DataRow(new[] { 1, 1, 1, 2 }, 3, 2, DisplayName = "Winning { 1, 1, 1, 2 }")]
        [DataRow(new[] { 1, 2, 2, 2 }, 1, 1, DisplayName = "Winning { 1, 2, 2, 2 }")]
        [DataTestMethod]
        public void AssertThatSolverReturnsWinningMove(int[] heaps, int heap, int number)
        {
            // Arrange
            var expected = new Move(heap: heap, number: number);

            // Act
            var actual = NextMove.Solve(heaps);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}