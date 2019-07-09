using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName {
            get {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }
        public string IdentityNumber { get; set; }
        public Gender Gender { get; set; }

        public DateTime DateofBirth { get; set; }
        public int Age { get {
                int age = DateTime.Today.Year - DateofBirth.Year;
                if (DateTime.Today.DayOfYear < DateofBirth.DayOfYear)
                {
                    age--;
                }
                return age;
            }
        }

        public Person(string firstname, string lastname)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            

        }
    }
}
