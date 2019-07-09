using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    public class StandardBus:Bus
    {
        private const int standartCapacity = 30;
        private const bool standartHasToilet = false;
        public StandardBus(string make, string plate) : base(make, plate, standartHasToilet) { }


        //public LuxuryBus(string make, string plate) : base(make, plate, luxHasToilet) { }
        public override int Capacity { get => standartCapacity; }

    }
}
