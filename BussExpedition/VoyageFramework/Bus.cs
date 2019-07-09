using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    public abstract class Bus
    {

        

        public Bus(string make, string plate,bool hastoilet)
        {
            Make = make;
            Plate = plate;
            HasToilet = hastoilet;
        }
        public Bus(string make, bool hastoilet) : this(make, string.Empty,hastoilet) { }
        

        public abstract int Capacity { get; }
        public string Make { get; }
        public string Plate { get; set; }
        public bool HasToilet { get; }

    }
}
