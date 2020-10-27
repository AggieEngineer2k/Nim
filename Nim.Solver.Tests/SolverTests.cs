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
        /// [1] returns no move.
        /// </summary>
        [TestMethod]
        public void AssertThatOneHeapWithOneObjectReturnsNull()
        {
            // Arrange
            var heaps = new[] { 1 };

            // Act
            var actual = NextMove.Solve(heaps);

            // Assert
            actual.Should().BeNull();
        }

        /// <summary>
        /// [2] returns a move.
        /// </summary>
        [TestMethod]
        public void AssertThatOneHeapWithMoreThanOneObjectReturnsOneLess()
        {
            // Arrange
            var heaps = new[] { 2 };
            var expected = new Move(heap: 0, number: 1);

            // Act
            var actual = NextMove.Solve(heaps);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// [1,1] returns a move.
        /// </summary>
        [TestMethod]
        public void AssertThatTwoHeapsWithOneObjectEachReturnsOneFromOne()
        {
            // Arrange
            var heaps = new[] { 1, 1 };
            var expected = new Move(heap: 0, number: 1);

            // Act
            var actual = NextMove.Solve(heaps);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// [1,2] returns a move.
        /// </summary>
        [TestMethod]
        public void AssertThatTwoHeapsWithOneObjectAndMoreThanOneObjectReturnsAllObjectsFromTheSecondHeap()
        {
            // Arrange
            var heaps = new[] { 1, 2 };
            var expected = new Move(heap: 1, number: 2);

            // Act
            var actual = NextMove.Solve(heaps);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// [1,1,1] returns no move.
        /// </summary>
        [TestMethod]
        public void AssertThatThreeHeapsWithOneObjectReturnsNoMove()
        {
            // Arrange
            var heaps = new[] { 1, 1, 1 };

            // Act
            var actual = NextMove.Solve(heaps);

            // Assert
            actual.Should().BeNull();
        }

        /// <summary>
        /// [1,1,2] returns a move.
        /// </summary>
        [TestMethod]
        public void AssertThatTwoHeapsWithOneObjectAndAThirdWithMoreReturnsAllButOneObjectsFromTheThird()
        {
            // Arrange
            var heaps = new[] { 1, 1, 2 };
            var expected = new Move(heap: 2, number: 1);

            // Act
            var actual = NextMove.Solve(heaps);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// [3,4,5] returns a move.
        /// </summary>
        [TestMethod]
        public void AssertThatSolverReturnsWinningMove()
        {
            // Arrange
            var heaps = new[] { 3, 4, 5 };
            var expected = new Move(heap: 0, number: 2);

            // Act
            var actual = NextMove.Solve(heaps);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}