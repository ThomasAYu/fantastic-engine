using System;

namespace GymWarmups
{
    class Program
    {
        static void Main(string[] args)
        {
            var warmUpSetsCollection = WarmUpSets.LoadWarmUps();

            // TODO add a load file system that will auto load saved Warm-up settings.

            UISimple.PrintWelcome();
            UISimple.MenuBase(warmUpSetsCollection);
            Console.WriteLine("\nThank you for using The Gym Warmups App (Working Title)!");
            Console.ReadLine();
        }
    }
}
