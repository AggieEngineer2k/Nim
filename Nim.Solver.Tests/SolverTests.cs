// <copyright file="SolverTests.cs" company="AggieEngineer2K">
// Copyright (c) AggieEngineer2K. All rights reserved.
// </copyright>

namespace Nim.Solver.Tests
{
    using System;
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
            Assert.IsNull(actual);
        }

        /// <summary>
        /// [2] returns a move.
        /// </summary>
        [TestMethod]
        public void AssertThatOneHeapWithMoreThanOneObjectReturnsOneLess()
        {
            // Arrange
            var heaps = new[] { 2 };
            var expected = (0, 1);

            // Act
            var actual = NextMove.Solve(heaps);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// [1,1] returns a move.
        /// </summary>
        [TestMethod]
        public void AssertThatTwoHeapsWithOneObjectEachReturnsOneFromOne()
        {
            // Arrange
            var heaps = new[] { 1, 1 };
            var expected = (0, 1);

            // Act
            var actual = NextMove.Solve(heaps);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// [1,2] returns a move.
        /// </summary>
        [TestMethod]
        public void AssertThatTwoHeapsWithOneObjectAndMoreThanOneObjectReturnsAllObjectsFromTheSecondHeap()
        {
            // Arrange
            var heaps = new[] { 1, 2 };
            var expected = (1, 2);

            // Act
            var actual = NextMove.Solve(heaps);

            // Assert
            Assert.AreEqual(expected, actual);
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
            Assert.IsNull(actual);
        }

        /// <summary>
        /// [1,1,2] returns a move.
        /// </summary>
        [TestMethod]
        public void AssertThatTwoHeapsWithOneObjectAndAThirdWithMoreReturnsAllObjectsFromTheThird()
        {
            // Arrange
            var heaps = new[] { 1, 1, 2 };
            var expected = (2, 2);

            // Act
            var actual = NextMove.Solve(heaps);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// [3,4,5] returns a move.
        /// </summary>
        [TestMethod]
        public void AssertThatSolverReturnsWinningMove()
        {
            // Arrange
            var heaps = new[] { 3, 4, 5 };
            var expected = (0, 2);

            // Act
            var actual = NextMove.Solve(heaps);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}