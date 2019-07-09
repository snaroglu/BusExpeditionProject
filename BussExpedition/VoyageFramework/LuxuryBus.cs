using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    public class LuxuryBus:Bus
    {
        private const int luxuryCapacity = 20;
        private const bool luxHasToilet = true;
        public LuxuryBus(string make, string plate) : base(make, plate,luxHasToilet){}
        

        //public LuxuryBus(string make, string plate) : base(make, plate, luxHasToilet) { }
        public override int Capacity { get => luxuryCapacity; }

    }
}
