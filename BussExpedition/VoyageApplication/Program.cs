using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyageFramework;

namespace VoyageApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //Person salim = new Person("Salim", "Naroğlu");
            //Console.WriteLine(salim.FirstName);
            //salim.DateofBirth = new DateTime(1992, 11, 3);
            //Console.WriteLine(salim.DateofBirth.Year);
            //Console.WriteLine(DateTime.Today.Year);
            //Console.WriteLine(salim.Age);

            //StandardBus sbus = new StandardBus("merce","53");
            StandardBus bus = new StandardBus("merc", "5353");
            //Console.WriteLine(sbus.Capacity);
            //Console.WriteLine(sbus.HasToilet);
            //Console.WriteLine(lbus.Capacity);
            //Console.WriteLine(lbus.HasToilet);
            //Console.WriteLine(DateTime.Today.DayOfYear);
            Driver Salim = new Driver("salim", "nar",LicenseType.HighLicense, new DateTime(1992, 11, 3));
            //Console.WriteLine(Salim.Age);

            Person mehmetali = new Person("mehmetali", "cakir");
            Person mehmetali2 = new Person("mehmetali", "cakir");
            Person mehmetali3 = new Person("mehmetali", "cakir");
            Route rotayeni = new Route("rize", "istanbul", 1430);
            rotayeni.BreakCount = 1;
            Console.WriteLine(rotayeni.Name);
            Console.WriteLine(rotayeni.Duration);
            Console.WriteLine(rotayeni.Baseprice);
            BusExpedition sefer = new BusExpedition(rotayeni,new DateTime(2019,10,10),bus);
            sefer.AddDriver(Salim);
            Console.WriteLine(sefer.Code);
            Console.WriteLine(sefer.GetPriceOf(2));
            sefer.SellTicket(mehmetali, 3, 270);
            sefer.SellDoubleTickets(mehmetali2, mehmetali3, 5, 550);
            SeatSection section = sefer.Tickets[0].SeatInformation.Section;
            Console.ReadLine();

        }
    }
}
