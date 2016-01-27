using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectReader or = new ObjectReader();
            or.clearFiles();

            // Test Hotel Class Functions
            Console.WriteLine("Hotel Data Create:");
            Console.WriteLine("---------------------------");

            Hotel hotel = new Hotel(0, "Avalon", "The Palm 32, Dubai");

            hotel.addRoom(0, "A1", 2, 1000.0f);
            hotel.addRoom(0, "A2", 3, 1500.0f);

            Console.WriteLine(hotel.getInfo(0));

            // Test Read Room Class From File
            ArrayList rooms = or.getRoomsList(); // get objects from file

            Console.WriteLine("READ/ WRITE FROM FILE PART:");
            Console.WriteLine("Room Array writen from file:");
            Console.WriteLine("---------------------------");
            for (int i = 0; i < rooms.Count; i++)
                Console.WriteLine(((Room)rooms[i]).getInfo());

            // Test Read Room Class From File (after some changes) - test changeInFile() function
            ((Room)rooms[0]).setRoomNotAvailable("A1");
            ((Room)rooms[1]).setPricePerNight("A2", 2100.0f);

            ArrayList rooms2 = or.getRoomsList();

            Console.WriteLine("Room Array writen from file after changes:");
            Console.WriteLine("---------------------------");
            for (int i = 0; i < rooms.Count; i++)
                Console.WriteLine(((Room)rooms2[i]).getInfo());

            // Create Customer Data to read /write from file 
            ArrayList customers = new ArrayList();

            customers.Add(new Customer(0, "Lionel Messi", "Royal Residence 3, Barcelona", "+48 832333222", 5, 0.5f));
            customers.Add(new Customer(1, "Mao Asada", "Ice Palace 14, Tokyo", "+321 2243332223", 3, 0.05f));

            // Test Read Customers Class From File
            ArrayList customers2 = or.getCustomersList(rooms); // get objects from file

            Console.WriteLine("Customer Array writen from file:");
            Console.WriteLine("---------------------------");
            for (int i = 0; i < customers2.Count; i++)
                Console.WriteLine(((Customer)customers2[i]).getInfo());

            // Test Read Customers Class Array and Room Class Array From File (after some changes in Customer Class) - test changeInFile() function
            ((Customer)customers2[0]).bookRoom(0, rooms, "A1");
                //((Customer)customers2[0]).bookRoom(0, rooms, "A2");

            Console.WriteLine("Room and Customer Array after changes:");
            Console.WriteLine("---------------------------");
            for (int i = 0; i < rooms.Count; i++)
                Console.WriteLine(((Room)rooms[i]).getInfo());

            for (int i = 0; i < customers2.Count; i++)
                Console.WriteLine(((Customer)customers2[i]).getInfo());

            // Customers from file test
            ArrayList customers3 = or.getCustomersList(or.getRoomsList()); // get objects from file

            Console.WriteLine("Customer Array after changes - writen from file:");
            Console.WriteLine("---------------------------");
            for (int i = 0; i < customers3.Count; i++)
                Console.WriteLine(((Customer)customers3[i]).getInfo());


            // Serialization Part TEST
            hotel.actualizeRooms(0, or.getRoomsList());
            ((Customer)customers3[1]).bookRoom(1, hotel, 0, "A2"); // make some changes 

            Console.WriteLine("SERIALIZE PART:");
            Console.WriteLine("Hotel before serialization:");
            Console.WriteLine("---------------------------");
            Console.WriteLine(hotel.getInfo(0));

            or.serializeHotel(hotel);
            Console.WriteLine("Hotel after deserialization:");
            Console.WriteLine("---------------------------");
            Hotel hotel2 = or.deserializeHotel();
            Console.WriteLine(hotel2.getInfo(0));
            
            or.serializeHotel(hotel);
            Console.WriteLine("Hotel after deserialization to array:");
            Console.WriteLine("---------------------------");
            Hotel [] hotel3 = or.deserializeHotelAsArray();
            
            if (hotel3 != null)
                for (int i = 0; i < hotel3.Length; i++)
                    Console.WriteLine(hotel3[i].getInfo(i));
        }
    }
}
