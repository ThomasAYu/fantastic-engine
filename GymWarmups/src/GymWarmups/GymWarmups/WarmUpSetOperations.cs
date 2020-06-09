using System;
using System.Collections.Generic;
using System.Linq;

namespace GymWarmups
{
    public static class WarmUpSetOperations
    {
        /// <summary>
        /// Takes user input and sends to the correct parser based on it.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<WarmUpSet> WarmUpSetModifier(string input, List<WarmUpSet> warmUpSets)
        {
            // TODO refactor into subclasses?
            // TODO create string parser class.

            if (input.Contains("Add"))
            {
                warmUpSets = ParseAddWarmUpSetInputIfValid(input, warmUpSets);
                return warmUpSets;
            }

            if (input.Contains("Insert"))
            {
                warmUpSets = ParseInsertWarmUpSetInputIfValid(input, warmUpSets);
                return warmUpSets;
            }

            if (input.Contains("Remove"))
            {
                warmUpSets = ParseRemoveWarmUpSetInputIfValid(input, warmUpSets);
                return warmUpSets;
            }

            if (input.Contains("Move"))
            {
                warmUpSets = ParseMoveWarmUpSetInputIfValid(input, warmUpSets);
                return warmUpSets;
            }

            Console.WriteLine("Please enter a valid Warm-up Set Editor Option");
            return warmUpSets;
        }

        /// <summary>
        /// Validates add Warm-up set input then sends to the parser. Returns original Warm-up list if there's no valid input.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="warmUpSets"></param>
        /// <returns></returns>
        public static List<WarmUpSet> ParseAddWarmUpSetInputIfValid(string input, List<WarmUpSet> warmUpSets)
        {

            // TODO refactor separate reusable methods from repeated conditionals and checks. - 
            const int addWordLength = 3;
            const int kgWordLength = 2;

            if (input.Length > addWordLength)
                input = input.Substring(addWordLength);

            var usePercentage = true;

            if (input.Contains("kg") && input.Length > kgWordLength)
            {
                input = input.Trim();
                input = input.Remove(input.Length - kgWordLength);
                usePercentage = false;
            }

            var parts = input.Split('@');

            if (parts.Length == 2)
            {
                var repParsePass = int.TryParse(parts[0].Trim(), out int repititions);
                var weightParsePass = double.TryParse(parts[1].Trim(), out double weight);

                if (repParsePass == true && weightParsePass == true && repititions > 0 && weight > 0)
                {
                    warmUpSets = AddValidWarmUpSet(warmUpSets, repititions, weight, usePercentage);
                    return warmUpSets;
                }
            }

            Console.WriteLine("Please input valid 'Add' values using the correct format.");
            return warmUpSets;
        }

        /// <summary>
        /// Adds an already validated Warm-up set onto our current Warm-up set list.
        /// </summary>
        /// <param name="warmUpSets"></param>
        /// <param name="repititions"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        private static List<WarmUpSet> AddValidWarmUpSet(List<WarmUpSet> warmUpSets, int repititions, double weight, bool usePercentage)
        {
            // TODO simplify parameters by creating the WarmUpSet separately and passing that through.
            var set = new WarmUpSet
            {
                Repititions = repititions,
                Weight = weight,
                UsePercentage = usePercentage
            };

            warmUpSets.Add(set);

            return warmUpSets;
        }

        /// <summary>
        /// Validates insert Warm-up set input then sends to the parser. Returns the original Warm-up list if the input isn't valid.
        /// </summary>
        /// <param name="input"></param>
        private static List<WarmUpSet> ParseInsertWarmUpSetInputIfValid(string input, List<WarmUpSet> warmUpSets)
        {
            var parts = input.Split(',');

            if (parts.Length == 2)
            {
                const int insertWordLength = 6;
                const int kgWordLength = 2;

                var inputInsertSection = parts[0];

                if (inputInsertSection.Length > insertWordLength)
                    inputInsertSection = inputInsertSection.Substring(insertWordLength);

                var usePercentage = true;

                if (parts[1].Contains("kg") && parts[1].Length > kgWordLength)
                {
                    parts[1] = parts[1].Trim();
                    parts[1] = parts[1].Remove(parts[1].Length - kgWordLength);
                    usePercentage = false;
                }

                var insertParsePass = int.TryParse(inputInsertSection.Trim(), out int insertWarmUpSetNumber);
                var partsSet = parts[1].Split('@');

                if (partsSet.Length == 2 && insertParsePass == true && insertWarmUpSetNumber <= warmUpSets.Count && insertWarmUpSetNumber > 0)
                {
                    var repParsePass = int.TryParse(partsSet[0].Trim(), out int repititions);
                    var weightParsePass = double.TryParse(partsSet[1].Trim(), out double weight);

                    if (repParsePass == true && weightParsePass == true && repititions > 0 && weight > 0)
                    {
                        warmUpSets = InsertValidWarmUpSet(warmUpSets, insertWarmUpSetNumber, repititions, weight, usePercentage);
                        return warmUpSets;
                    }
                }
            }
            Console.WriteLine("Please input valid 'Insert' values using the correct format.");
            return warmUpSets;
        }

        /// <summary>
        /// Inserts an already validated Warm-up set into the Warm-up sets list at the given location.
        /// </summary>
        /// <param name="warmUpSets"></param>
        /// <param name="warmUpSetNumber"></param>
        /// <param name="repititions"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        private static List<WarmUpSet> InsertValidWarmUpSet(List<WarmUpSet> warmUpSets, int warmUpSetNumber, int repititions, double weight, bool usePercentage)
        {
            var set = new WarmUpSet
            {
                Repititions = repititions,
                Weight = weight,
                UsePercentage = usePercentage
            };
            warmUpSets.Insert((warmUpSetNumber - 1), set);

            return warmUpSets;
        }

        /// <summary>
        /// Validates remove Warm-up set input and then sends to the parser. Returns the original Warm-up set if the input is invalid.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="warmUpSets"></param>
        private static List<WarmUpSet> ParseRemoveWarmUpSetInputIfValid(string input, List<WarmUpSet> warmUpSets)
        {
            if (input.Length > 6)
                input = input.Substring(6);

            var removeParsePass = int.TryParse(input.Trim(), out int removeWarmUpSetNumber);

            if (removeParsePass == true && removeWarmUpSetNumber <= warmUpSets.Count && removeWarmUpSetNumber > 0)
            {
                warmUpSets = RemoveValidWarmUpSet(warmUpSets, removeWarmUpSetNumber);
                return warmUpSets;
            }

            Console.WriteLine("Please input valid 'Remove' values using the correct format.");
            return warmUpSets;
        }

        /// <summary>
        /// Removes an already validated Warm-up set from the Warm-up set list. 
        /// </summary>
        /// <param name="warmUpSets"></param>
        /// <param name="removeWarmUpSetNumber"></param>
        /// <returns></returns>
        private static List<WarmUpSet> RemoveValidWarmUpSet(List<WarmUpSet> warmUpSets, int removeWarmUpSetNumber)
        {
            warmUpSets.RemoveAt(removeWarmUpSetNumber - 1);
            return warmUpSets;
        }

        /// <summary>
        /// Validates move Warm-up set input and then sends it to the parser. Returns original Warm-up set if input is invalid.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="warmUpSets"></param>
        /// <returns></returns>
        private static List<WarmUpSet> ParseMoveWarmUpSetInputIfValid(string input, List<WarmUpSet> warmUpSets)
        {
            var parts = input.Split(',');

            if (parts.Length == 2)
            {
                var moveInitialNumberString = parts[0];
                if (moveInitialNumberString.Length > 4)
                    moveInitialNumberString = moveInitialNumberString.Substring(4);

                var moveInitialNumberParsePass = int.TryParse(moveInitialNumberString.Trim(), out int moveInitialNumber);
                var moveEndNumberParsePass = int.TryParse(parts[1].Trim(), out int moveEndNumber);

                if (moveInitialNumberParsePass == true && moveEndNumberParsePass == true && moveInitialNumber > 0 && moveInitialNumber <= warmUpSets.Count && moveEndNumber > 0 && moveEndNumber < warmUpSets.Count)
                {
                    warmUpSets = MoveValidWarmUpSet(warmUpSets, moveInitialNumber, moveEndNumber);
                    return warmUpSets;
                }
            }
            Console.WriteLine("Please input valie 'Move' values using the correct format.");
            return warmUpSets;
        }

        /// <summary>
        /// Moves a Warm-up set based on already validated initial and ending index numbers.
        /// </summary>
        /// <param name="warmUpSets"></param>
        /// <param name="initialNumber"></param>
        /// <param name="endNumber"></param>
        /// <returns></returns>
        public static List<WarmUpSet> MoveValidWarmUpSet(List<WarmUpSet> warmUpSets, int initialNumber, int endNumber)
        {
            if (initialNumber > endNumber)
            {
                var tempWarmUpSet = warmUpSets[initialNumber - 1];
                warmUpSets.RemoveAt(initialNumber - 1);
                warmUpSets.Insert(endNumber - 1, tempWarmUpSet);
            }
            else if (initialNumber < endNumber)
            {
                var tempWarmUpSet = warmUpSets[initialNumber - 1];
                warmUpSets.RemoveAt(initialNumber - 1);
                if (endNumber < warmUpSets.Count)
                {
                    warmUpSets.Insert(endNumber - 1, tempWarmUpSet);
                }
                else
                {
                    warmUpSets.Add(tempWarmUpSet);
                }
            }

            return warmUpSets;
        }
    }
}