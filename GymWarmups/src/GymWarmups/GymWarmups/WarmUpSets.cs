using System;
using System.Collections.Generic;
using System.Text;

namespace GymWarmups
{
    class WarmUpSets
    {
        /// <summary>
        /// Loads a default list of Warm-up sets.
        /// </summary>
        /// <returns></returns>
        public static List<WarmUpSet> LoadWarmUps() // TODO replace with proper file loading and saving system.
        {
            var warmUpSetsCollection = new List<WarmUpSet>
            {
                new WarmUpSet { Repititions = 8, UsePercentage = false, Weight = 20 },
                new WarmUpSet { Repititions = 5, UsePercentage = true, Weight = 50},
                new WarmUpSet { Repititions = 4, UsePercentage = true, Weight = 60},
                new WarmUpSet { Repititions = 3, UsePercentage = true, Weight = 70},
                new WarmUpSet { Repititions = 2, UsePercentage = true, Weight = 80},
                new WarmUpSet { Repititions = 1, UsePercentage = true, Weight = 90},
                new WarmUpSet { Repititions = 1, UsePercentage = true, Weight = 95},
            };

            return warmUpSetsCollection;
        }
    }
}
