using System;
using System.Collections.Generic;

namespace GymWarmups
{
    static class UISimple
    {
        public static List<WarmUpSet> UIWarmUpSets { get; set; }

        /// <summary>
        /// Prints out the welcome message and Warm-up sets.
        /// </summary>
        public static void PrintWelcome()
        {
            Console.WriteLine("Hello! Welcome to The Gym App! (Working Title)\n");

            // TODO also print out current saved Warm-up sets.
        }

        /// <summary>
        /// Sets up the Main Menu for the application.
        /// </summary>
        public static void MenuBase(List<WarmUpSet> warmUpSetsDefault) 
        {
            UIWarmUpSets = warmUpSetsDefault;

            while (true)
            {
                PrintMainMenuUserOptions();
                var userInputForMenuSelection = Console.ReadLine();

                switch(userInputForMenuSelection)
                {
                    case "SetWork":
                        WorkingSetMenu();
                        break;
                    case "SetWarm":
                        WarmUpSetMenu();
                        break;
                    case "q":
                        return;
                    default:
                        Console.WriteLine("Please enter a valid option\n");
                        break;
                }
            }
        }

        /// <summary>
        /// Sets up the Working set Menu.
        /// </summary>
        /// <returns></returns>
        private static void WorkingSetMenu()
        {
            while (true)
            {
                PrintWorkingSetUserOptions();
                var userInputForWorkingSet = Console.ReadLine();

                if (userInputForWorkingSet == "b") return;
                
                bool parseToWorkingSetSuccessful = WorkingSetOperations.TryParseToWorkingSet(userInputForWorkingSet, out WorkingSet workingSet);
                if (parseToWorkingSetSuccessful) WorkingSetOperations.PrintWarmUpSetsFromWorkingSet(workingSet, UIWarmUpSets);
            }
        }

        /// <summary>
        ///  Sets up the Warm-up set Menu.
        /// </summary>
        private static void WarmUpSetMenu()
        {
            while (true)
            {
                PrintWarmUpSetsWithoutWorkingSet();
                PrintWarmUpSetUserOptions();
                var userInputForWarmUpSetModification = Console.ReadLine();

                if (userInputForWarmUpSetModification == "b") return;

                UIWarmUpSets = WarmUpSetOperations.WarmUpSetModifier(userInputForWarmUpSetModification, UIWarmUpSets);
            }
        }

        private static void PrintWarmUpSetsWithoutWorkingSet()
        {
            Console.WriteLine("\nYour current warm-up sets are:");
            for (var i = 0; i < UIWarmUpSets.Count; i++)
            {
                var repsWord = "rep";

                bool repsWordisPlural = UIWarmUpSets[i].Repititions > 1;
                if (repsWordisPlural) repsWord = "reps";

                if (UIWarmUpSets[i].UsePercentage)
                {

                    Console.WriteLine($"{i + 1}) {UIWarmUpSets[i].Repititions} {repsWord} at {UIWarmUpSets[i].Weight}% of the working weight.");
                    continue;

                }
                Console.WriteLine($"{i + 1}) {UIWarmUpSets[i].Repititions} {repsWord} at {UIWarmUpSets[i].Weight}kgs.");
            }
        }

        /// <summary>
        /// Prints out possible input options when at the insert Working set Menu.
        /// </summary>
        public static void PrintMainMenuUserOptions()
        {
            Console.WriteLine("\nMain Menu Options:");
            Console.WriteLine("\nSetWork : Set a working weight.");
            Console.WriteLine("SetWarm : Edit warm-up weights and repititions.");
            Console.WriteLine("q : Quit the application.\n");
        }

        /// <summary>
        /// Prints out options and format while at the Working set Menu.
        /// </summary>
        private static void PrintWorkingSetUserOptions()
        {
            Console.WriteLine("\nWorking Set Setter Options:\n");
            Console.WriteLine("Please enter your first working set. (Format: 'RepNumber' @ 'WorkingWeightNumber' eg. 4 @ 105)"); 
            Console.WriteLine("Or 'b' to go back to the Main Menu.\n");
            Console.WriteLine("Note: Add ', x' at the end to adjust rounding for warm-up sets. Default is 2.5kg. (eg. 4 @ 105, 1)");
        }


        /// <summary>
        /// Prints out possible input options when at the Edit Warm-up sets Menu.
        /// </summary>
        public static void PrintWarmUpSetUserOptions()
        {
            Console.WriteLine("\nWarm-up Set Editor Options:\n");
            Console.WriteLine("Add 'RepNumber' @ 'PercentageNumber': Adds a new warm-up set to the end of the list.");
            Console.WriteLine("Insert 'Y', 'RepNumber' @ 'PercentageNumber': Inserts a new warm-up set at position 'Y'.");
            Console.WriteLine("Remove 'Z': Removes the warm-up set at position 'Z'.");
            Console.WriteLine("Move 'A', 'B': Moves the warm-up set currently in position 'A' to position 'B'.");
            Console.WriteLine("Or 'b' to go back to the Main Menu.");
            Console.WriteLine("\nNote: Use 'kg' at the end of any weight value to set an explicit weight.");
            Console.WriteLine("\n(eg. 'Add 3 @ 60' | 'Remove 3' | 'Move 2, 5')\n");
        }
    }
}
