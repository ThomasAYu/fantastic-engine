using System;
using System.Collections.Generic;
using System.Text;

namespace GymWarmups
{
    public class WorkingSet : Set 
    {
        public WorkingSet()
        {
            const double defaultRounding = 2.5;
            RoundingNumber = defaultRounding;
        }
        public double? RoundingNumber { get; set; }
    }
}
