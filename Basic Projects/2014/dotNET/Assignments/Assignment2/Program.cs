using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Assignment2
{
    class Passenger
    {
        protected int id; 
        protected string firstName;
        protected string lastName;
        protected string phoneNumber;

        protected ArrayList tickets = new ArrayList();

        public Passenger (int id, string firstName, string lastName, string phoneNumber)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
        }

        public Passenger(int id, string firstName, string lastName, string phoneNumber, Flight flight, string ticketID)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;

            AddNewTicket(id, flight, ticketID, 800);
        }

        public Passenger(int id, string firstName, string lastName, string phoneNumber, Flight flight, string ticketID, float price)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;

            AddNewTicket(id, flight, ticketID, price);
        }

        public virtual void AddNewTicket(int pessengerID, Flight flight, string ticketID, float price)
        {
            tickets.Add(new Ticket(id, flight, ticketID, price));
        }

        public string GetInfo(int id)
        {
            if (id == this.id)
            {
                return "Passenger ID: " + id + "\nFirst Name: " + firstName + "; Last Name: " + lastName + "; Phone Number: " + phoneNumber + ";";
            }

            return "";
        }

        public bool customerFound(int id)
        {
            return id == this.id;
        }
    }

    class EconomyPassenger : Passenger
    {
        protected int luggageWeight;

        public EconomyPassenger (int id, string firstName, string lastName, string phoneNumber)
            : base(id, firstName, lastName, phoneNumber)
        {
            this.luggageWeight = 24;
        }

        public EconomyPassenger(int id, string firstName, string lastName, string phoneNumber, int luggageWeight)
            : base(id, firstName, lastName, phoneNumber)
        {
            this.luggageWeight = luggageWeight;
        }

        public EconomyPassenger(int id, string firstName, string lastName, string phoneNumber, Flight flight, string ticketID)
            : base(id, firstName, lastName, phoneNumber, flight, ticketID)
        {
            this.luggageWeight = 24;
        }

        public EconomyPassenger(int id, string firstName, string lastName, string phoneNumber, Flight flight, string ticketID, float price)
            : base(id, firstName, lastName, phoneNumber, flight, ticketID, price)
        {
            this.luggageWeight = 24;
        }

        public EconomyPassenger(int id, string firstName, string lastName, string phoneNumber, Flight flight, string ticketID, int luggageWeight)
            : base(id, firstName, lastName, phoneNumber, flight, ticketID)
        {
            this.luggageWeight = luggageWeight;
        }

        public EconomyPassenger(int id, string firstName, string lastName, string phoneNumber, Flight flight, string ticketID, float price, int luggageWeight)
            : base(id, firstName, lastName, phoneNumber, flight, ticketID, price)
        {
            this.luggageWeight = luggageWeight;
        }

        public virtual string GetTicketInfo(int id, Passenger[] passengers, Flight[] flights)
        {
            string txt = "";

            if (id == this.id)
            {
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (txt != "") txt += "\n";

                    txt += ((Ticket)tickets[i]).getInfo(passengers, flights);

                    txt += "\nTicket type: Economy";
                    txt += "\nLuggage limit: " + luggageWeight + " [kg]";
                    txt += "\n ----------- ";
                }
            }

            return txt;
        }

        public string GetTicketInfo(int id, Passenger[] passengers, Flight[] flights, string ticketID)
        {
            if (id == this.id)
            {
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (((Ticket)tickets[i]).ticketFound(ticketID))
                    {
                        return ((Ticket)tickets[i]).getInfo(passengers, flights, ticketID);
                    }
                }
            }

            return "Ticket not found!\n";
        }
    }

    class FirstClassPassenger : EconomyPassenger
    {
        private float bonus = 0;
        private string mealMenu;

        public FirstClassPassenger(int id, string firstName, string lastName, string phoneNumber)
            : base(id, firstName, lastName, phoneNumber)
        {
            this.mealMenu = "specials of the day";
        }

        public FirstClassPassenger(int id, string firstName, string lastName, string phoneNumber, int luggageWeight)
            : base(id, firstName, lastName, phoneNumber, luggageWeight)
        {
            this.mealMenu = "specials of the day";
        }

        public FirstClassPassenger(int id, string firstName, string lastName, string phoneNumber, int luggageWeight, string mealMenu)
            : base(id, firstName, lastName, phoneNumber, luggageWeight)
        {
            this.mealMenu = mealMenu;
        }

        public FirstClassPassenger(int id, string firstName, string lastName, string phoneNumber, Flight flight, string ticketID)
            : base(id, firstName, lastName, phoneNumber, flight, ticketID)
        {
            this.mealMenu = "specials of the day";
        }

        public FirstClassPassenger(int id, string firstName, string lastName, string phoneNumber, Flight flight, string ticketID, float price)
            : base(id, firstName, lastName, phoneNumber, flight, ticketID, price)
        {
            this.mealMenu = "specials of the day";
        }

        public FirstClassPassenger(int id, string firstName, string lastName, string phoneNumber, Flight flight, string ticketID, int luggageWeight)
            : base(id, firstName, lastName, phoneNumber, flight, ticketID, luggageWeight)
        {
            this.mealMenu = "specials of the day";
        }

        public FirstClassPassenger(int id, string firstName, string lastName, string phoneNumber, Flight flight, string ticketID, float price, int luggageWeight)
            : base(id, firstName, lastName, phoneNumber, flight, ticketID, price, luggageWeight)
        {
            this.mealMenu = "specials of the day";
        }

        public FirstClassPassenger(int id, string firstName, string lastName, string phoneNumber, Flight flight, string ticketID, int luggageWeight, string mealMenu)
            : base(id, firstName, lastName, phoneNumber, flight, ticketID, luggageWeight)
        {
            this.mealMenu = mealMenu;
        }

        public FirstClassPassenger(int id, string firstName, string lastName, string phoneNumber, Flight flight, string ticketID, float price, int luggageWeight, string mealMenu)
            : base(id, firstName, lastName, phoneNumber, flight, ticketID, price, luggageWeight)
        {
            this.mealMenu = mealMenu;
        }

        public override string GetTicketInfo(int id, Passenger[] passengers, Flight[] flights)
        {
            string txt = "";

            if (id == this.id)
            {
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (txt != "") txt += "\n";

                    txt += ((Ticket)tickets[i]).getInfo(passengers, flights);

                    txt += "\nTicket type: First Class";
                    txt += "\nLuggage limit: " + luggageWeight + " [kg]";
                    txt += "\nMeal menu: " + mealMenu;
                    txt += "\n ----------- ";
                }
            }

            return txt;
        }

        public override void AddNewTicket(int pessengerID, Flight flight, string ticketID, float price)
        {
            tickets.Add(new Ticket(id, flight, ticketID, price - price*bonus));
            bonus += 0.02f;
        }
    }

    class Ticket
    {
        private string ticketID;
        private int passengerID;
        private Flight flight;
        private float price;
        private readonly float extraTax;

        public Ticket(int passengerID, Flight flight, string ticketID)
        {
            this.passengerID = passengerID;
            this.flight = flight;
            this.ticketID = ticketID;
            price = 800;

            int[] date = flight.getFlightDate();

            DateTime dt = new DateTime(date[2],date[1],date[0]);
            if (dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Saturday)
            {
                extraTax = 0.07f;
            }
            else
            {
                extraTax = 0.05f;
            }
        }

        public Ticket(int passengerID, Flight flight, string ticketID, float price)
        {
            this.passengerID = passengerID;
            this.flight = flight;
            this.price = price;
            this.ticketID = ticketID;

            int[] date = flight.getFlightDate();

            DateTime dt = new DateTime(date[2], date[1], date[0]);
            if (dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Saturday)
            {
                extraTax = 0.07f;
            }
            else
            {
                extraTax = 0.05f;
            }
        }

        public float getPrice(string ticketID)
        {
            if (ticketID == this.ticketID)
                return price+(price*extraTax);

            return -1;
        }

        public string getInfo(Passenger [] passangers, Flight [] flights)
        {
            string text = "";

            for (int i = 0; i < passangers.Length; i++)
            {
                if (((Passenger)passangers[i]).customerFound(passengerID))
                {
                    text += "Ticket ID : " + this.ticketID + "\n";
                    text += (((Passenger)passangers[i]).GetInfo(passengerID)) + "\n";
                }
            }

            for (int i = 0; i < flights.Length; i++)
            {
                if (((Flight)flights[i]).flightFound(flight.getFlightID()))
                {
                    text += (((Flight)flights[i]).getFlightInfo(flight.getFlightID()));
                }
            }

            float price = getPrice(this.ticketID);
            if (price != -1)
                text += "\nPrice: " + price + " Euro";

            return text;
        }

        public string getInfo(Passenger[] passangers, Flight[] flights, string ticketID)
        {
            string text = "";

            if (ticketID == this.ticketID)
            {
                for (int i = 0; i < passangers.Length; i++)
                {
                    if (((Passenger)passangers[i]).customerFound(passengerID))
                    {
                        text += "Ticket ID : " + this.ticketID + "\n";
                        text += (((Passenger)passangers[i]).GetInfo(passengerID)) + "\n";
                    }
                }
                    
                for (int i = 0; i < flights.Length; i++)
                {
                    if (((Flight)flights[i]).flightFound(flight.getFlightID()))
                    {
                        text += (((Flight)flights[i]).getFlightInfo(flight.getFlightID()));
                    }
                }

                float price = getPrice(ticketID);
                if (price != -1)
                    text += "\nPrice: " + price + " Euro";
            }

            return text;
        }

        public bool ticketFound(string id)
        {
            return id == this.ticketID;
        }
    }

    class Flight
    {
        private string id;
        private string flightOrigin, flightDestination;
        private int day, month, year;

        public Flight(string fOrigin, string fDestination, int day, int month, int year, string id)
        {
            this.id = id;
            this.flightOrigin = fOrigin;
            this.flightDestination = fDestination;

            this.day = day;
            this.month = month;
            this.year = year;
        }

        public string getFlightInfo(string flightID)
        {
            if (flightID == this.id)
            {
                string sDay;
                string sMonth;

                if (day < 10) sDay = "0" + day;
                else sDay = day.ToString();

                if (month < 10) sMonth = "0" + month;
                else sMonth = month.ToString();

                return "Flight ID: " + id + "; Origin: " + flightOrigin + "; Destination: " + flightDestination + "; Date: " + sDay + "/" + sMonth + "/" + year + ";";
            }

            return "";
        }

        public int[] getFlightDate()
        {
            int[] arr = new int[3];

            arr[0] = day;
            arr[1] = month;
            arr[2] = year;

            return arr;
        }

        public string getFlightID()
        {
            return id;
        }

        public bool flightFound(string id)
        {
            return id == this.id;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int idPessenger = 0;
            
            Flight[] flights = new Flight[5];
            flights[0] = new Flight("Paris", "Kopenhaga", 1, 5, 2014, "SL321");
            flights[1] = new Flight("Tokyo", "Osaka", 3, 6, 2014, "JA451");
            flights[2] = new Flight("Osaka", "Barcelona", 4, 6, 2014, "EA551");
            flights[3] = new Flight("Rome", "Warsaw", 21, 7, 2014, "WR767");
            flights[4] = new Flight("Warsaw", "Tokyo", 17, 5, 2014, "LOT333");

            Console.WriteLine("Flights List: ");
            Console.WriteLine(flights[0].getFlightInfo("SL321"));
            Console.WriteLine(flights[1].getFlightInfo("JA451"));
            Console.WriteLine(flights[2].getFlightInfo("EA551"));
            Console.WriteLine(flights[3].getFlightInfo("WR767"));
            Console.WriteLine(flights[4].getFlightInfo("LOT333"));
            Console.WriteLine();


            EconomyPassenger[] economyPassengers = new EconomyPassenger [3];
            FirstClassPassenger[] firstClassPassengers = new FirstClassPassenger[3];
            
            Hashtable passengers = new Hashtable();

            economyPassengers[0] = new EconomyPassenger(idPessenger, "Lionel", "Messi", "+54 444312663", flights[0], "00001"); // ID = 0
            passengers.Add(idPessenger, "EP");
            idPessenger++;

            firstClassPassengers[0] = new FirstClassPassenger(idPessenger, "Yagami", "Akira", "+321 433533222"); // ID = 1
            passengers.Add(idPessenger, "FCP");
            idPessenger++;

            firstClassPassengers[1] = new FirstClassPassenger(idPessenger, "Andrzej", "Kowalski", "+48 723277322", flights[0], "00002"); // ID = 2
            passengers.Add(idPessenger, "FCP");
            idPessenger++;

            firstClassPassengers[1].AddNewTicket(2, flights[1], "00003", 800.0f);
            firstClassPassengers[1].AddNewTicket(2, flights[2], "00004", 1200.0f);


            firstClassPassengers[2] = new FirstClassPassenger(idPessenger, "Jackie", "Chan", "+532 43223444", flights[2], "00005", 32000.0f, 62, "vegetarian dishes"); // ID = 3
            passengers.Add(idPessenger, "FCP");
            idPessenger++;

            economyPassengers[1] = new EconomyPassenger(idPessenger, "Yuna", "Kim", "+442 677727722", flights[1], "00006", 500.0f, 48); // ID = 4
            passengers.Add(idPessenger, "EP");
            idPessenger++;

            economyPassengers[1].AddNewTicket(4, flights[3], "00007", 700.0f);


            economyPassengers[2] = new EconomyPassenger(idPessenger, "Tomasz", "Dziuba", "+48 723333333", flights[4], "00008", 400.0f); // ID = 5
            passengers.Add(idPessenger, "EP");
            idPessenger++;

            economyPassengers[2].AddNewTicket(5, flights[1], "00009", 300.0f);
            economyPassengers[2].AddNewTicket(5, flights[2], "00010", 600.0f);


            Console.WriteLine("Passengers List: ");

            string txt = "";
            IDictionaryEnumerator en = passengers.GetEnumerator();
            while (en.MoveNext())
            {
                if (en.Value.ToString() == "EP")
                {
                    for (int i = 0; i < economyPassengers.Length; i++)
                        txt += economyPassengers[i].GetInfo((int)en.Key); 
                }
                else if (en.Value.ToString() == "FCP")
                {
                    for (int i = 0; i < firstClassPassengers.Length; i++)
                        txt += firstClassPassengers[i].GetInfo((int)en.Key);
                }

                if (txt != "")
                {
                    Console.WriteLine(txt);
                    txt = "";
                }
            }
            Console.WriteLine();

            Console.WriteLine("Check Ticket ID: 00001");
            Console.WriteLine(" ----------- ");
            Console.WriteLine(economyPassengers[0].GetTicketInfo(0, economyPassengers, flights, "00001"));
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Tickets List: ");
            Console.WriteLine(" ----------- ");

            en.Reset();
            while (en.MoveNext())
            {
                if (en.Value.ToString() == "EP")
                {
                    for (int i = 0; i < economyPassengers.Length; i++)
                        txt += economyPassengers[i].GetTicketInfo((int)en.Key, economyPassengers, flights);
                }
                else if (en.Value.ToString() == "FCP")
                {
                    for (int i = 0; i < firstClassPassengers.Length; i++)
                        txt += firstClassPassengers[i].GetTicketInfo((int)en.Key, firstClassPassengers, flights);
                }

                if (txt != "")
                {
                    Console.WriteLine(txt);
                    txt = "";
                }
            }

            Console.ReadLine();
        }
    }
}
