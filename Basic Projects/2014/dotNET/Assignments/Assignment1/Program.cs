using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Assignment1
{
    class Customer
    {
        private int id;
        private string name;
        private int idFlight;

        public Customer(string name, int id, int idFlight)
        {
            this.name = name;
            this.id = id;
            this.idFlight = idFlight;
        }

        public string getCustomerInfo()
        {
            return "ID: " + id + "\n Name: " + name;
        }

        public int getFlightID()
        {
            return idFlight;
        }

        public int getID()
        {
            return id;
        }

        public bool customerFound(string name)
        {
            return name == this.name;
        }

        public bool customerFound(int id)
        {
            return id == this.id;
        }

        public bool customerFoundByFlight(int id)
        {
            return id == this.idFlight;
        }
    }

    class Flight
    {
        private int id;
        private string flightOrigin, flightDestination;
        private int day, month, year;

        public Flight(string fOrigin, string fDestination, int day, int month, int year, int id)
        {
            this.id = id;
            this.flightOrigin = fOrigin;
            this.flightDestination = fDestination;

            this.day = day;
            this.month = month;
            this.year = year;
        }

        public string getFlightInfo()
        {
            string sDay;
            string sMonth;

            if (day < 10) sDay = "0" + day;
            else sDay = day.ToString();

            if (month < 10) sMonth = "0" + month;
            else sMonth = month.ToString();

            return "\tFlight ID: " + id + "\n\tOrigin: " + flightOrigin + "\n\tDestination: " + flightDestination + "\n\tDate: " +sDay + "/" + sMonth + "/" + year;
        }

        public bool flightFound(int id)
        {
            return id == this.id;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string name, flightOrigin, flightDestination;
            string check;
            int day, month, year;
            int idCustomer = 0;
            int idFlight = 0;
            int menu = 0, tmp;

            ArrayList customers = new ArrayList();
            ArrayList flight = new ArrayList();

            //customers.Add(new Customer("Kira", 100));

            for (; ; )
            {
                if (menu == 0)
                {
                    try
                    {
                        Console.WriteLine("SELECT OPTION:");
                        Console.WriteLine("-------------");
                        Console.WriteLine("1. Add new flight. ");
                        Console.WriteLine("2. Add new customer. ");
                        Console.WriteLine("3. Check customer's list. ");
                        Console.WriteLine("4. Search customer. ");
                        Console.WriteLine("5. Search flight. ");
                        Console.WriteLine();
                        Console.WriteLine("0. Exit program. ");

                        tmp = Convert.ToInt32(Console.ReadLine());
                        if (tmp >= 0 && tmp <= 5)
                        {
                            if (tmp == 0) { break; }
                           
                            menu = tmp;
                        }
                        else
                        {
                            Console.WriteLine("Invalide data type!");
                        }
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Invalide data type!");
                    }
                }
                else if (menu == 1)
                {
                    try
                    {
                        Console.Write("Please type flight origin: ");

                        flightOrigin = Console.ReadLine();

                        Console.Write("Please type flight destination: ");

                        flightDestination = Console.ReadLine();


                        //
                        Console.WriteLine("Please type flight date: ");
                        Console.Write("Day: ");

                        day = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Month: ");

                        month = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Year: ");

                        year = Convert.ToInt32(Console.ReadLine());


                        //

                        idFlight++;
                        flight.Add(new Flight(flightOrigin, flightDestination, day, month, year, idFlight));

                        for (; ; )
                        {
                            try
                            {
                                Console.Write("Continue? [Y/N]: ");

                                check = Console.ReadLine();

                                if (check == "Y" || check == "y") break;
                                else if (check == "N" || check == "n")
                                {
                                    menu = 0;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalide data type!");
                                }
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Invalide data type!");
                            }
                        }
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Invalide data type!");
                    }
                }
                else if (menu == 2)
                {
                    try
                    {
                        Console.Write("Please type customer name: ");

                        name = Console.ReadLine();

                        for (; ; )
                        {
                            try
                            {
                                Console.Write("Add new customer to existing flight? [Y/N]: ");

                                check = Console.ReadLine();

                                if (check == "Y" || check == "y")
                                {
                                    Console.Write("Please type flight ID: ");

                                    tmp = Convert.ToInt32(Console.ReadLine());

                                    bool flyCheck = false;
                                    int j = 0;
                                    for (; j < flight.Count; j++)
                                    {
                                        if (((Flight)flight[j]).flightFound(tmp))
                                        {
                                            flyCheck = true;
                                            break;
                                        }
                                    }

                                    if (flyCheck)
                                    {
                                        idCustomer++;
                                        customers.Add(new Customer(name, idCustomer, tmp));

                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Flight ID: " + tmp + " isn't exist.");
                                    }
                                }
                                else if (check == "N" || check == "n")
                                {
                                    Console.WriteLine("Please create new flight.");

                                    Console.Write("Please type flight origin: ");

                                    flightOrigin = Console.ReadLine();

                                    Console.Write("Please type flight destination: ");

                                    flightDestination = Console.ReadLine();


                                    //
                                    Console.WriteLine("Please type flight date: ");
                                    Console.Write("Day: ");

                                    day = Convert.ToInt32(Console.ReadLine());

                                    Console.Write("Month: ");

                                    month = Convert.ToInt32(Console.ReadLine());

                                    Console.Write("Year: ");

                                    year = Convert.ToInt32(Console.ReadLine());


                                    //

                                    idFlight++;
                                    idCustomer++;

                                    flight.Add(new Flight(flightOrigin, flightDestination, day, month, year, idFlight));
                                    customers.Add(new Customer(name, idCustomer, idFlight));

                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalide data type!");
                                }
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Invalide data type!");
                            }
                        }

                        for (; ; )
                        {
                            try
                            {
                                Console.Write("Continue? [Y/N]: ");

                                check = Console.ReadLine();

                                if (check == "Y" || check == "y") break;
                                else if (check == "N" || check == "n")
                                {
                                    menu = 0;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalide data type!");
                                }
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Invalide data type!");
                            }
                        }
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Invalide data type!");
                    }
                }
                else if (menu == 3)
                {
                    Console.WriteLine("All customers list: ");

                    if (customers.Count == 0) Console.WriteLine("No customers found!");
                    else
                    {
                        for (int i = 0; i < customers.Count; i++)
                        {
                            Console.WriteLine(((Customer)customers[i]).getCustomerInfo());

                            bool flightFound = false;
                            int j = 0;
                            for (; j < flight.Count; j++)
                            {
                                if (((Flight)flight[j]).flightFound(((Customer)customers[i]).getFlightID()))
                                {
                                    Console.WriteLine(((Flight)flight[j]).getFlightInfo());
                                    flightFound = true;

                                    break;
                                }
                            }

                            if (!flightFound) Console.WriteLine("\t No flight found!");
                        }
                    }

                    for (; ; )
                        try
                        {
                            Console.Write("End? [Y/N]: ");

                            check = Console.ReadLine();

                            if (check == "Y" || check == "y")
                            {
                                menu = 0;
                                break;
                            }
                            else if (check == "N" || check == "n") break;
                            else
                            {
                                Console.WriteLine("Invalide data type!");
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Invalide data type!");
                        }
                }
                else if (menu == 4)
                {
                    int option = 0;

                    for (; ; )
                        try
                        {
                            Console.WriteLine("Search by: ");
                            Console.WriteLine("1. Name.");
                            Console.WriteLine("2. ID.");
                            Console.WriteLine();
                            Console.WriteLine("0. Back. ");

                            tmp = Convert.ToInt32(Console.ReadLine());
                            if (tmp >= 0 && tmp <= 2)
                            {
                                if (tmp == 0) 
                                {
                                    menu = 0;
                                    break; 
                                }

                                option = tmp;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalide data type!");
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Invalide data type!");
                        }

                    if (option == 1)
                    {
                        string nameCheck = "";

                        for (; ; )
                        {
                            Console.Write("Type name : ");

                            try
                            {
                                nameCheck = Console.ReadLine();
                                break;
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Invalide data type!");
                            }
                        }

                        Console.WriteLine("Search result: ");

                        bool customerFound = false;
                        for (int j = 0; j < customers.Count; j++)
                        {
                            if (((Customer)customers[j]).customerFound(nameCheck))
                            {
                                Console.WriteLine(((Customer)customers[j]).getCustomerInfo());

                                for (int k = 0; k < flight.Count; k++)
                                {
                                    if (((Flight)flight[k]).flightFound(((Customer)customers[j]).getFlightID()))
                                    {
                                        Console.WriteLine(((Flight)flight[k]).getFlightInfo());
                                        break;
                                    }
                                }

                                customerFound = true;
                            }
                        }

                        if (!customerFound)
                            Console.WriteLine("Customer: " + nameCheck + " not found!");
                    }
                    else if (option == 2)
                    {
                        int idCheck = 0;

                        for (; ; )
                        {
                            Console.Write("Type ID : ");

                            try
                            {
                                idCheck = Convert.ToInt32(Console.ReadLine());
                                break;
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Invalide data type!");
                            }
                        }

                        Console.WriteLine("Search result: ");

                        bool customerFound = false;
                        for (int j = 0; j < customers.Count; j++)
                        {
                            if (((Customer)customers[j]).customerFound(idCheck))
                            {
                                Console.WriteLine(((Customer)customers[j]).getCustomerInfo());

                                for (int k = 0; k < flight.Count; k++)
                                {
                                    if (((Flight)flight[k]).flightFound(((Customer)customers[j]).getFlightID()))
                                    {
                                        Console.WriteLine(((Flight)flight[k]).getFlightInfo());
                                        break;
                                    }
                                }

                                customerFound = true;
                            }
                        }

                        if (!customerFound)
                            Console.WriteLine("Customer ID: " + idCheck + "not found!");
                    }

                    for (; ; )
                        try
                        {
                            Console.Write("End? [Y/N]: ");

                            check = Console.ReadLine();

                            if (check == "Y" || check == "y")
                            {
                                menu = 0;
                                break;
                            }
                            else if (check == "N" || check == "n") break;
                            else
                            {
                                Console.WriteLine("Invalide data type!");
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Invalide data type!");
                        }
                }
                else if (menu == 5)
                {
                    int idCheck = 0;

                    for (; ; )
                    {
                        Console.Write("Type ID : ");

                        try
                        {
                            idCheck = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Invalide data type!");
                        }
                    }

                    Console.WriteLine("Search result: ");

                    bool flightFound = false;
                    int count = 1;
                    for (int j = 0; j < flight.Count; j++)
                    {
                        if (((Flight)flight[j]).flightFound(idCheck))
                        {
                            Console.WriteLine(((Flight)flight[j]).getFlightInfo());

                            Console.WriteLine("Customers list: ");

                            bool customersFound = false;
                            for (int k = 0; k < customers.Count; k++)
                            {
                                if (((Customer)customers[k]).customerFoundByFlight(idCheck))
                                {
                                    Console.WriteLine(count + ". " + ((Customer)customers[k]).getCustomerInfo());
                                    count++;

                                    customersFound = true;
                                }
                            }

                            if (!customersFound)
                            {
                                Console.WriteLine("Customers not found!");
                            }

                            flightFound = true;
                            break;
                        }
                    }

                    if (!flightFound)
                        Console.WriteLine("Flight ID: " + idCheck + " not found!");

                    for (; ; )
                        try
                        {
                            Console.Write("End? [Y/N]: ");

                            check = Console.ReadLine();

                            if (check == "Y" || check == "y")
                            {
                                menu = 0;
                                break;
                            }
                            else if (check == "N" || check == "n") break;
                            else
                            {
                                Console.WriteLine("Invalide data type!");
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Invalide data type!");
                        }
                }
            }
        }
    }
}
