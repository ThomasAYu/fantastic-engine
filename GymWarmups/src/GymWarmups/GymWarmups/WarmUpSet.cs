using System;
using System.Collections.Generic;
using System.Text;

namespace GymWarmups
{
    public class WarmUpSet : Set
    {
        /// <summary>
        /// Determies whether the Warm-up set contains the explicit weight value, or a percentage value to use with the Working set.
        /// </summary>
        public bool UsePercentage { get; set; }
    }
}
