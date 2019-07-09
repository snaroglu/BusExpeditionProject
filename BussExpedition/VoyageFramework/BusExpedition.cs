using System;
using System.Collections.Generic;

namespace VoyageFramework
{
    /*
     * constant fields
     * private fields
     * private property
     * constructuor
     * public property
     * private methods
     * public static methods
     * public methods
     */

    public class BusExpedition
    {
        private string _code;
        private const string _luxBusCode = "LX";
        private const string _standardBusCode = "ST";
        private const int _perkmfordriver = 400;
        private const int _perkmforhost = 600;
        private DateTime _estimatedDepartureTime;
        public BusExpedition(Route route, DateTime departuretime, Bus bus)
        {
            Route = route;
            DepartureTime = departuretime;
            Bus = bus;
            Drivers = new List<Driver>();
            Hosts = new List<Host>();
            Tickets = new List<Ticket>();
        }


        public string Code
        {
            get
            {
                _code = String.Format("{0}{1}-{2}-{3}",
                    Route.DepartureLocation[0].ToString(),
                    DepartureTime.ToString("yyMMdd"),
                    Bus is LuxuryBus ? _luxBusCode : _standardBusCode,
                    new Random().Next(1000, 10000));

                return _code;
            }
        }
        public Bus Bus { get; set; }
        public List<Driver> Drivers { get; }
        public List<Host> Hosts { get; }
        public List<Ticket> Tickets { get; }
        public Route Route { get; set; }
        public DateTime DepartureTime { get; }
        public DateTime EstimatedDepartureTime
        {
            get
            {
                return _estimatedDepartureTime;
            }
            set
            {
                _estimatedDepartureTime = value > DepartureTime ? value : DepartureTime;
            }
        }
        public DateTime EstimatedArrivalTime
        {
            get
            {
                return EstimatedDepartureTime.AddMinutes(Route.Duration);
            }
        }
        public bool HasDelay
        {
            get
            {
                return EstimatedDepartureTime != DepartureTime ? true : false;
            }
        }
        public bool HasSnackService { get; set; }

        public void AddDriver(Driver driver)
        {
            if (driver is null)
            {
                throw new ArgumentNullException(nameof(driver));
            }
            else if (Bus is LuxuryBus && driver.LicenseType != LicenseType.HighLicense
                    ||
                    Bus is StandardBus && driver.LicenseType == LicenseType.None)
            {
                throw new InvalidOperationException("Driver License Type is not suitable");
            }

            else
            {
                decimal maxdriver = Math.Ceiling((decimal)Route.Distance / _perkmfordriver);
                if (Drivers.Count < maxdriver)
                {
                    Drivers.Add(driver);
                }
            }
        }

        public void AddHost(Host host)
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }
            else
            {
                decimal maxhost = Math.Ceiling((decimal)Route.Distance / _perkmforhost);
                if (Hosts.Count < maxhost)
                {
                    Hosts.Add(host);
                }
            }
        }
        public void RemoveDriver(Driver driver)
        {
            if (Drivers.Contains(driver))
            {
                Drivers.Remove(driver);
            }
        }

        public void RemoveHost(Host host)
        {
            if (Hosts.Contains(host))
            {
                Hosts.Remove(host);
            }
        }

        public decimal GetPriceOf(int SeatNumber)
        {
            decimal ticketprice;
            if (Bus is LuxuryBus)
            {
                ticketprice = Route.Baseprice * 1.35m;
            }
            else
            {
                ticketprice = GetSeatInformation(SeatNumber).Category == SeatCategory.Singular ?
                    Route.Baseprice * 1.25m : Route.Baseprice * 1.20m;
            }
            return ticketprice;
        }
        //SellTicket(person : Person, seatNumber : int, fee : decimal) : Ticket
        public Ticket SellTicket(Person person, int SeatNumber, decimal fee)
        {
            Ticket ticket = null;
            decimal minAmount = (person is Host ||
                person is Driver) ? Route.Baseprice : Route.Baseprice * 1.05m;
            if (fee < minAmount)
            {
                throw new ArgumentOutOfRangeException(nameof(fee));
            }
            else
            {
                if (IsSeatAvailableFor(SeatNumber, person.Gender))
                {
                    ticket = new Ticket(this, GetSeatInformation(SeatNumber), person, fee);
                    Tickets.Add(ticket);
                }
            }
            return ticket;

        }
        //SellDoubleTickets(person01 : Person, person02 : Person, int seatNumber, fee decimal) : Ticket[]
        public Ticket[] SellDoubleTickets(Person person01, Person person02, int seatNumber, decimal fee)
        {
            Ticket[] doubleTickets = new Ticket[2];
            decimal minAmount = (IsEmployee(person01) || IsEmployee(person02)) ?
                Route.Baseprice : Route.Baseprice * 1.05m;
            if (fee < minAmount * 2)
            {
                throw new ArgumentOutOfRangeException(nameof(fee));

            }
            if (isAvalailableForDouble(seatNumber))
            {
                if (seatNumber % 3 == 0)
                {
                    doubleTickets[0] = new Ticket(this, GetSeatInformation(seatNumber - 1), person01, fee / 2);
                    doubleTickets[1] = new Ticket(this, GetSeatInformation(seatNumber), person02, fee / 2);

                }
                else
                {
                    doubleTickets[0] = new Ticket(this, GetSeatInformation(seatNumber), person01, fee / 2);
                    doubleTickets[1] = new Ticket(this, GetSeatInformation(seatNumber + 1), person02, fee / 2);

                }
                Tickets.Add(doubleTickets[0]);
                Tickets.Add(doubleTickets[1]);

            }
            return doubleTickets;

        }
        private bool isAvalailableForDouble(int seatNumber)
        {
            if (GetSeatInformation(seatNumber).Category == SeatCategory.Singular)
            {
                throw new ArgumentOutOfRangeException(nameof(seatNumber));
            }
            if (GetSeatInformation(seatNumber).Category == SeatCategory.Corridor)
            {
                return IsSeatEmpty(seatNumber) && IsSeatEmpty(seatNumber + 1);
            }
            if (GetSeatInformation(seatNumber).Category == SeatCategory.Window)
            {
                return IsSeatEmpty(seatNumber) && IsSeatEmpty(seatNumber - 1);
            }
            return false;
        }
        private bool IsEmployee(Person person)
        {
            return person is Host || person is Driver;
        }
        public bool IsSeatAvailableFor(int SeatNumber, Gender gender)
        {
            if (IsSeatEmpty(SeatNumber) == false)
            {
                return false;
            }
            if (GetSeatInformation(SeatNumber).Category != SeatCategory.Singular)
            {
                if (GetSeatInformation(SeatNumber).Category == SeatCategory.Corridor)
                {
                    if (!IsSeatEmpty(SeatNumber + 1))
                    {
                        return Tickets[SeatNumber + 1].Passenger.Gender == gender;
                    }
                }
                else
                {
                    if (!IsSeatEmpty(SeatNumber - 1))
                    {
                        return Tickets[SeatNumber - 1].Passenger.Gender == gender;
                    }
                }
            }
            return true;
        }


        public bool IsSeatEmpty(int SeatNumber)
        {
            //return Tickets[SeatNumber] is null ? true : false;
            for (int i = 0; i < Tickets.Count; i++)
            {
                if (Tickets[i].SeatInformation.Number == SeatNumber)
                {
                    return false;
                }
            }
            return true;
        }
        public SeatInformation GetSeatInformation(int SeatNumber)
        {

            SeatInformation seatInformation;
            if (SeatNumber < 0 || SeatNumber > Bus.Capacity)
            {
                throw new ArgumentOutOfRangeException(nameof(SeatNumber));
            }
            if (Bus is StandardBus)
            {
                switch (SeatNumber % 3)
                {
                    case 1:
                        seatInformation = new SeatInformation(SeatNumber, SeatSection.LeftSide, SeatCategory.Singular);
                        break;
                    case 2:
                        seatInformation = new SeatInformation(SeatNumber, SeatSection.None, SeatCategory.Corridor);
                        break;
                    case 0:
                        seatInformation = new SeatInformation(SeatNumber, SeatSection.RightSide, SeatCategory.Window);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(SeatNumber));
                }
            }
            else
            {

                switch (SeatNumber % 2)
                {
                    case 1:
                        seatInformation = new SeatInformation(SeatNumber, SeatSection.LeftSide, SeatCategory.Singular);
                        break;
                    case 0:
                        seatInformation = new SeatInformation(SeatNumber, SeatSection.RightSide, SeatCategory.Singular);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(SeatNumber));
                }
            }
            return seatInformation;
        }
    }
}