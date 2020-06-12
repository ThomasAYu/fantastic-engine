using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GymWarmups
{
    public static class WorkingSetOperations
    {
        /// <summary>
        /// Validates incoming string before passing it on to be parsed. Returns exception for non-valid strings.
        /// </summary>
        /// <param name="input"></param>
        public static bool TryParseToWorkingSet(string userInputForWorkingSet, out WorkingSet workingSet)
        {
            workingSet = new WorkingSet();

            string[] userInputForWorkingSetSplit = SplitUserInputForWorkingSet(userInputForWorkingSet);
            if (userInputForWorkingSetSplit == null) return false;

            workingSet = ParseUserInputSplitToWorkingSet(userInputForWorkingSetSplit);

            return true;

        }


        /// <summary>
        /// Prints out the Warm-up sets given the input Working set.
        /// </summary>
        /// <param name="workingSet"></param>
        /// <param name="warmUpSets"></param>
        public static void PrintWarmUpSetsFromWorkingSet(WorkingSet workingSet, List<WarmUpSet> warmUpSets)
        {
            Console.WriteLine($"You set the working set reps to {workingSet.Repititions} and the weight to {workingSet.Weight}kgs.\n");

            Console.WriteLine("Your warm-up sets for the given working set are:\n");
            foreach (var warmUpSet in warmUpSets)
            {
                string repsWord = RepOrRepsAssigner(warmUpSet);
                double warmUpWeight = CalculateWeight(workingSet, warmUpSet);

                Console.WriteLine($" - {warmUpWeight}kgs for {warmUpSet.Repititions} {repsWord}");
            }
        }

        private static string[] SplitUserInputForWorkingSet(string userInputForWorkingSet)
        {
            if (!UserInputContainsCorrectSyntax(userInputForWorkingSet)) return null;

            if (UserInputContainsCorrectSyntaxWithRounding(userInputForWorkingSet))
            {
                string[] userInputForWorkingSetSplitWithRounding = SplitUserInputContainingRounding(userInputForWorkingSet);
                if (userInputForWorkingSetSplitWithRounding.Length != 3) return null;
                return userInputForWorkingSetSplitWithRounding;
            }

            string[] userInputForWorkingSetSplit = userInputForWorkingSet.Split('@');
            if (userInputForWorkingSetSplit.Length != 2) return null;
            return userInputForWorkingSetSplit;

        }
        private static WorkingSet ParseUserInputSplitToWorkingSet(string[] userInputForWorkingSetSplit)
        {
            var workingSet = new WorkingSet();

            if (UserInputSplitContainsRoundingNumber(userInputForWorkingSetSplit))
            {
                workingSet.RoundingNumber = GetRoundingNumber(userInputForWorkingSetSplit[2]);
                if (workingSet.RoundingNumber == null) return null;
            }

            workingSet.Repititions = GetRepititions(userInputForWorkingSetSplit[0]);
            if (workingSet.Repititions == null) return null;

            workingSet.Weight = GetWeight(userInputForWorkingSetSplit[1]);
            if (workingSet.Weight == null) return null;

            return workingSet;
        }
        private static bool UserInputSplitContainsRoundingNumber(string[] userInputForWorkingSetSplit)
        {
            if (userInputForWorkingSetSplit.Length == 3) return true;
            return false;
        }
        private static double? GetRoundingNumber(string roundingNumberString)
        {
            var parseToRoundingNumberSuccessful = double.TryParse(roundingNumberString.Trim(), out double roundingNumber);
            if (!parseToRoundingNumberSuccessful) return null;
            if (!ValidRoundingNumber(roundingNumber)) return null;

            return roundingNumber;

        }
        private static int? GetRepititions(string repititionsString)
        {
            var parseToRepititionsSuccessful = int.TryParse(repititionsString.Trim(), out int repititions);
            if (!parseToRepititionsSuccessful) return null;
            if (!ValidRepititionsValue(repititions)) return null;

            return repititions;

        }
        private static double? GetWeight(string weightString)
        {
            var parseToWeightSuccessful = double.TryParse(weightString.Trim(), out double weight);
            if (!parseToWeightSuccessful) return null;
            if (!ValidWeightValue(weight)) return null;

            return weight;

        }
        private static string[] SplitUserInputContainingRounding(string userInputForWorkingSet)
        {
            userInputForWorkingSet = userInputForWorkingSet.Replace(',', '@');
            string[] userInputForWorkingSetSplit = userInputForWorkingSet.Split('@');
            return userInputForWorkingSetSplit;
        }
        private static bool UserInputContainsCorrectSyntax(string userInputForWorkingSet)
        {
            if (UserInputContainsCorrectSyntaxWithRounding(userInputForWorkingSet)) return true;
            if (UserInputContainsCorrectSyntaxWithoutRounding(userInputForWorkingSet)) return true;
            return false;
        }
        private static bool UserInputContainsCorrectSyntaxWithoutRounding(string userInputForWorkingSet)
        {
            return userInputForWorkingSet.Contains('@');
        }
        private static bool UserInputContainsCorrectSyntaxWithRounding(string userInputForWorkingSet)
        {
            return userInputForWorkingSet.Contains('@') && userInputForWorkingSet.Contains(',');
        }
        private static bool ValidRoundingNumber(double roundingNumber)
        {
            if (roundingNumber > 0) return true;
            return false;
        }
        private static bool ValidRepititionsValue(int repititions)
        {
            if (repititions > 0) return true;
            return false;
        }
        private static bool ValidWeightValue(double weight)
        {
            if (weight > 0) return true;
            return false;
        }
        private static double CalculateWeight(WorkingSet workingSet, WarmUpSet warmUpSet)
        {
            var warmUpWeight = (double)warmUpSet.Weight;

            if (warmUpSet.UsePercentage) warmUpWeight = CalculateWeightUsingPercentage(warmUpWeight, workingSet);

            return warmUpWeight;
        }
        private static double CalculateWeightUsingPercentage(double warmUpWeight, WorkingSet workingSet)
        {
            return Math.Floor((double)workingSet.Weight * warmUpWeight * 0.01 / (double)workingSet.RoundingNumber) * (double)workingSet.RoundingNumber;
        }
        private static string RepOrRepsAssigner(WarmUpSet warmUpSet)
        {
            var repsWord = "rep";
            bool repsWordisPlural = warmUpSet.Repititions > 1;
            if (repsWordisPlural) repsWord = "reps";
            return repsWord;
        }
    }
}
