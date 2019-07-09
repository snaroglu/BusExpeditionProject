using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    public class Host:Person
    {
        public Host(string firstname, string lastname, DateTime dateofbirth) : base(firstname, lastname) {
            DateofBirth = dateofbirth;
            if (Age<18)
            {
                throw new ArgumentOutOfRangeException(nameof(Age));
            }
        }
    }
}
