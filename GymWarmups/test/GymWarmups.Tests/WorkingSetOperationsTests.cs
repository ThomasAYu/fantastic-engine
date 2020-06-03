using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GymWarmups.Tests
{
    [TestClass]
    public class WorkingSetOperationsTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void WorkingSetParseIfValid_NegativeNumberInString_ExceptionThrown()
        {
            // Arrange
            var testString = "-3 @ 45";

            // Act
            WorkingSetOperations.WorkingSetParseIfValid(testString, new System.Collections.Generic.List<WarmUpSet>());

            // Assert - done using an attribute
        }
    }
}
