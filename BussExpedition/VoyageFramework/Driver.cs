using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    public class Driver:Person
    {
        public Driver(string firstname, string lastname,LicenseType licenseType, DateTime dateofBirth) : base(firstname, lastname) {

            LicenseType = licenseType;
            DateofBirth = dateofBirth;
            if (Age < 25)
            {
                throw new ArgumentOutOfRangeException(nameof(Age));
            }
            if (licenseType == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(licenseType));
            }
        }

        public LicenseType LicenseType { get; set; }
    }
}
