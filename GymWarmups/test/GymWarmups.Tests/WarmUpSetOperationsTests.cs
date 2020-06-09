using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GymWarmups.Tests
{
    [TestClass]
    public class WarmUpSetOperationsTests
    {
        [TestMethod]
        public void MoveValidWarmUpSet_MovesSetCorrectly_SetMovesToDesiredLocation()
        {
            // Arrange
            var tempWarmUpSet = new List<WarmUpSet>
            {
                new WarmUpSet { Repititions = 8, UsePercentage = false, Weight = 20 },
                new WarmUpSet { Repititions = 5, UsePercentage = true, Weight = 50 },
                new WarmUpSet { Repititions = 4, UsePercentage = true, Weight = 60 },
                new WarmUpSet { Repititions = 3, UsePercentage = true, Weight = 70 },
                new WarmUpSet { Repititions = 2, UsePercentage = true, Weight = 80 },
                new WarmUpSet { Repititions = 1, UsePercentage = true, Weight = 90 },
                new WarmUpSet { Repititions = 1, UsePercentage = true, Weight = 95 },
            };
            var expected = new List<WarmUpSet>
            {
                new WarmUpSet { Repititions = 8, UsePercentage = false, Weight = 20 },
                new WarmUpSet { Repititions = 4, UsePercentage = true, Weight = 60 },
                new WarmUpSet { Repititions = 3, UsePercentage = true, Weight = 70 },
                new WarmUpSet { Repititions = 2, UsePercentage = true, Weight = 80 },
                new WarmUpSet { Repititions = 5, UsePercentage = true, Weight = 50 },
                new WarmUpSet { Repititions = 1, UsePercentage = true, Weight = 90 },
                new WarmUpSet { Repititions = 1, UsePercentage = true, Weight = 95 },
            };

            int initalIndex = 2;
            int endIndex = 5;

            // Act 
            var result = WarmUpSetOperations.MoveValidWarmUpSet(tempWarmUpSet, initalIndex, endIndex);
            
            // Assert
            for (var i = 0; i < tempWarmUpSet.Count; i++)
            {
                Assert.AreEqual(expected[i].Repititions, tempWarmUpSet[i].Repititions);
                Assert.AreEqual(expected[i].Weight, tempWarmUpSet[i].Weight);
            }
        }

        [TestMethod]
        public void ParseAddWarmUpSetInputIfValid_AssignExplicitKgWeightsCorrecly_SetsUsePercentageToTrue()
        {
            // Arrange
            var tempWarmUpSet = new List<WarmUpSet>();

            // Act 
            var result = WarmUpSetOperations.ParseAddWarmUpSetInputIfValid("Add 3 @ 60kg", tempWarmUpSet);

            // Assert
            Assert.AreEqual(false, result[0].UsePercentage);
        }
    }
}
