using System;
using System.Collections.Generic;
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
        public static WorkingSet SetWorkingSet(string input) 
        {
            // TODO refactor - abstract out for clarity and reuse. Consider moving into a different class.
            var roundingParsePass = false;
            double? roundingNumber = null;

            if (input.Contains(','))
            {
                var roundingSplit = input.Split( "," );
                if (roundingSplit.Length == 2)
                {
                    roundingParsePass = double.TryParse(roundingSplit[1].Trim(), out double roundingNumberTemp);
                    roundingNumber = roundingNumberTemp;
                    input = roundingSplit[0];
                }
            }

            var parts = input.Split(new char[] { '@' });

            if (parts.Length == 2 && (roundingNumber == null || roundingNumber > 0))
            {
                var repParsePass = int.TryParse(parts[0].Trim(), out int repititions);
                var weightParsePass = double.TryParse(parts[1].Trim(), out double weight);

                if (repParsePass == true && weightParsePass == true && repititions > 0 && weight > 0)
                {
                    var workingSet = CreateValidWorkingSet(repititions, weight, roundingNumber);
                    return workingSet;
                }
                else
                {
                    Console.WriteLine("Invalid repitition or weight input.");
                    return null;
                }
            }
            Console.WriteLine("Invalid input. Please follow the correct format.");
            return null;
        }

        /// <summary>
        /// Takes in repitition and weight values and returns a <see cref="Set"/> object.
        /// </summary>
        /// <param name="repititions"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        private static WorkingSet CreateValidWorkingSet(int repititions, double weight, double? roundingNumber)
        {
            var workingSet = new WorkingSet
            {
                Repititions = repititions,
                Weight = weight
            };

            // possibly create a bool for the rounding check to add clarity.
            if(roundingNumber != null)
            workingSet.RoundToNearestNumber = (double)roundingNumber; 

            return workingSet;
        }

        /// <summary>
        /// Prints out the Warm-up sets given the input Working set.
        /// </summary>
        /// <param name="workingSet"></param>
        /// <param name="warmUpSets"></param>
        public static void PrintWarmUpSetsFromWorkingSet(WorkingSet workingSet, List<WarmUpSet> warmUpSets)
        {
            // TODO refactor. Create a weight calculator method and rep v reps word assigner.
            Console.WriteLine($"You set the working set reps to {workingSet.Repititions} and the weight to {workingSet.Weight}kgs.\n");

            Console.WriteLine("Your warm-up sets for the given working set are:\n");
            foreach (var warmUpSet in warmUpSets)
            {
                if (warmUpSet.UsePercentage == true) 
                {
                    if (warmUpSet.Repititions > 1)
                        Console.WriteLine($" - {Math.Floor(workingSet.Weight * warmUpSet.Weight * 0.01 / workingSet.RoundToNearestNumber) * workingSet.RoundToNearestNumber}kgs for {warmUpSet.Repititions} reps.");
                    else
                        Console.WriteLine($" - {Math.Floor(workingSet.Weight * warmUpSet.Weight * 0.01 / workingSet.RoundToNearestNumber) * workingSet.RoundToNearestNumber}kgs for {warmUpSet.Repititions} rep.");
                }
                else
                {
                    if (warmUpSet.Repititions > 1)
                        Console.WriteLine($" - {warmUpSet.Weight}kgs for {warmUpSet.Repititions} reps.");
                    else
                        Console.WriteLine($" - {warmUpSet.Weight}kgs for {warmUpSet.Repititions} rep.");
                }
            }
        }
    }
}
