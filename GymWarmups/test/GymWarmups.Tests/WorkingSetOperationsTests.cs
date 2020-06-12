using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GymWarmups.Tests
{
    [TestClass]
    public class WorkingSetOperationsTests
    {
        [TestMethod]
        public void TryParseToWorkingSet_ValidInputWithoutRoundingNumber_ReturnsTrueAndOutCorrectWorkingSet()
        {
            // Arrange
            var testString = "3 @ 45 ";
            var expectedWorkingSet = new WorkingSet() { Repititions = 3, Weight = 45, RoundingNumber = 2.5 };

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(expectedWorkingSet.Repititions, workingSet.Repititions);
            Assert.AreEqual(expectedWorkingSet.Weight, workingSet.Weight);
            Assert.AreEqual(expectedWorkingSet.RoundingNumber, workingSet.RoundingNumber);
        }
        [TestMethod]
        public void TryParseToWorkingSet_ValidInputWithRoundingNumber_ReturnsTrueAndOutCorrectWorkingSet()
        {
            // Arrange
            var testString = "3 @ 45 , 3";
            var expectedWorkingSet = new WorkingSet() { Repititions = 3, Weight = 45, RoundingNumber = 3 };

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(expectedWorkingSet.Repititions, workingSet.Repititions);
            Assert.AreEqual(expectedWorkingSet.Weight, workingSet.Weight);
            Assert.AreEqual(expectedWorkingSet.RoundingNumber, workingSet.RoundingNumber);
        }
        [TestMethod]
        public void TryParseToWorkingSet_ValidInputWithWeightDecimalWithoutRoundingNumber_ReturnsTrueAndOutCorrectWorkingSet()
        {
            // Arrange
            var testString = "3 @ 45.5";
            var expectedWorkingSet = new WorkingSet() { Repititions = 3, Weight = 45.5, RoundingNumber = 2.5 };

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(expectedWorkingSet.Repititions, workingSet.Repititions);
            Assert.AreEqual(expectedWorkingSet.Weight, workingSet.Weight);
            Assert.AreEqual(expectedWorkingSet.RoundingNumber, workingSet.RoundingNumber);
        }
        [TestMethod]
        public void TryParseToWorkingSet_ValidInputWithRoundingNumberDecimal_ReturnsTrueAndOutCorrectWorkingSet()
        {
            // Arrange
            var testString = "3 @ 45, 4.5 ";
            var expectedWorkingSet = new WorkingSet() { Repititions = 3, Weight = 45, RoundingNumber = 4.5 };

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(expectedWorkingSet.Repititions, workingSet.Repititions);
            Assert.AreEqual(expectedWorkingSet.Weight, workingSet.Weight);
            Assert.AreEqual(expectedWorkingSet.RoundingNumber, workingSet.RoundingNumber);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidStringIsEmpty_ReturnsFalseAndOutNullAndOutNull()
        {
            // Arrange
            var testString = "";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidStringIsWhiteSpace_ReturnsFalseAndOutNull()
        {
            // Arrange
            var testString = "       ";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidDecimalForReps_ReturnsFalseAndOutNull()
        {
            // Arrange
            var testString = "3.5 @ 105, 3 ";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidMultipleAtSyntax_ReturnsFalseAndOutNull()
        {
            // Arrange
            var testString = "3 @ 44 @ 32";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidMultipleCommasSyntax_ReturnsFalseAndOutNull()
        {
            // Arrange
            var testString = "3 @ 44 , 5 , 32";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidMultipleAtAndCommasSyntax_ReturnsFalseAndOutNull()
        {
            // Arrange
            var testString = "3 @ 44 @ 52 , 5 , 32";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidLettersForReps_ReturnsFalseAndOutNull()
        {
            // Arrange
            var testString = "a @ 105";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidLettersForWeight_ReturnsFalseAndOutNull()
        {
            // Arrange
            var testString = "3 @ a";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidLettersForRoundingNumber_ReturnsFalseAndOutNull()
        {
            // Arrange
            var testString = "3 @ 105, a";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidNegativeNumberForReps_ReturnsFalseAndOutNull()
        {
            // Arrange
            var testString = "-3 @ 45";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidNegativeNumberForWeight_ReturnsFalseAndOutNull()
        {
            // Arrange
            var testString = "3 @ -45";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidNegativeNumberForRoundingNumber_ReturnsFalseAndOutNull()
        {
            // Arrange
            var testString = "3 @ 45, -2.5";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidWhiteSpaceForReps_ReturnsFalseAndOutNull()
        {
            // Arrange
            var testString = "   @ 45";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidWhiteSpaceForWeight_ReturnsFalseAndOutNull()
        {
            // Arrange
            var testString = "3 @   ";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }
        [TestMethod]
        public void TryParseToWorkingSet_InvalidWhiteSpaceForRoundingNumber_ReturnsFalseAndOutNull()
        {
            // Arrange
            var testString = "3 @ 45,   ";

            // Act
            bool result = WorkingSetOperations.TryParseToWorkingSet(testString, out WorkingSet workingSet);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, workingSet);
        }


    }
}
