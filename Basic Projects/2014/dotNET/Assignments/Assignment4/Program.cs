using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectReader or = new ObjectReader();
            or.clearFiles();

            ObjectSerialization os = new ObjectSerialization();

            // PART 1 TEST
            // Create Hotel Class Test Data 
            Console.WriteLine("HOTEL SERIALIZE PART ->");
            Console.WriteLine("Hotel Data Created:");
            Console.WriteLine("---------------------------");

            Hotel [] hotel = new Hotel [2];

            hotel[0] = new Hotel(0, "Avalon", "The Palm 32, Dubai");
            hotel[0].addRoom(0, "A1", 2, 1000.0f);
            hotel[0].addRoom(0, "A2", 3, 1500.0f);

            hotel[1] = new Hotel(1, "Kira", "Yasumi 12, Kyoto");
            hotel[1].addRoom(1, "B1", 1, 3200.0f);

            for (int i = 0; i < hotel.Length; i++)
                Console.WriteLine(hotel[i].getInfo());


            // Serialize single Hotel Object
            os.serializeObject(hotel[0], @"t1.xml");


            // Deserialize and display single Hotel Object
            Console.WriteLine("Single Hotel Data After Deserlialization: (ID = 0)");
            Console.WriteLine("---------------------------");

            Hotel hotel2 = null;
            if (os.deserializeObject(ref hotel2, @"t1.xml"))
            {
                Console.WriteLine(hotel2.getInfo());
            }
            else
            {
                Console.WriteLine("Deserlize function problem!");
            }


            // Serialize Hotel Object array
            os.serializeObjectArray(hotel, @"t2.xml");


            // Deserialize and display Hotel Object array
            Console.WriteLine("Hotel Array After Deserlialization:");
            Console.WriteLine("---------------------------");

            Hotel [] hotel3 = null;
            if (os.deserializeObjectArray(ref hotel3, @"t2.xml"))
            {
                for (int i = 0; i < hotel3.Length; i++)
                    Console.WriteLine(hotel3[i].getInfo());
            }
            else
            {
                Console.WriteLine("Deserlize function problem!");
            }


            // PART 2 TEST
            // Create Customer Class Test Data 
            Console.WriteLine("CUSTOMER SERIALIZE PART ->");
            Console.WriteLine("Customer Data Created:");
            Console.WriteLine("---------------------------");

            ArrayList list = new ArrayList();

            list.Add(new Customer(0, "Lionel Messi", "Royal Residence 3, Barcelona", "+48 832333222", 5, 0.5f));
            list.Add(new Customer(1, "Mao Asada", "Ice Palace 14, Tokyo", "+321 2243332223", 3, 0.05f));

            Customer[] customer = new Customer[list.Count];
            for (int i = 0; i < list.Count; i++)
                customer[i] = (Customer)list[i];

            for (int i = 0; i < customer.Length; i++)
                Console.WriteLine(customer[i].getInfo());


            // Serialize single Customer Object
            os.serializeObject(customer[0], @"t3.xml");


            // Deserialize and display single Customer Object
            Console.WriteLine("Single Customer Data After Deserlialization: (ID = 0)");
            Console.WriteLine("---------------------------");

            Customer customer2 = null;
            if (os.deserializeObject(ref customer2, @"t3.xml"))
            {
                Console.WriteLine(customer2.getInfo());
            }
            else
            {
                Console.WriteLine("Deserlize function problem!");
            }


            // Serialize Customer Object array
            os.serializeObjectArray(customer, @"t4.xml");


            // Deserialize and display Customer Object array
            Console.WriteLine("Customer Array After Deserlialization:");
            Console.WriteLine("---------------------------");

            Customer[] customer3 = null;
            if (os.deserializeObjectArray(ref customer3, @"t4.xml"))
            {
                for (int i = 0; i < customer3.Length; i++)
                    Console.WriteLine(customer3[i].getInfo());
            }
            else
            {
                Console.WriteLine("Deserlize function problem!");
            }


            // PART 3 TEST
            // Get Room Class Array Data (created in Hotel)
            Console.WriteLine("ROOM SERIALIZE PART ->");
            Console.WriteLine("Room Data Created:");
            Console.WriteLine("---------------------------");

            list = new ArrayList();
            list = or.getRoomsList();

            Room[] room = new Room[list.Count];
            for (int i = 0; i < list.Count; i++)
                room[i] = (Room)list[i];

            for (int i = 0; i < room.Length; i++)
                Console.WriteLine(room[i].getInfo());


            // Serialize single Room Object
            os.serializeObject(room[0], @"t5.xml");


            // Deserialize and display single Room Object
            Console.WriteLine("Single Room Data After Deserlialization: (ID = 0)");
            Console.WriteLine("---------------------------");

            Room room2 = null;
            if (os.deserializeObject(ref room2, @"t5.xml"))
            {
                Console.WriteLine(room2.getInfo());
            }
            else
            {
                Console.WriteLine("Deserlize function problem!");
            }


            // Serialize Room Object array
            os.serializeObjectArray(room, @"t6.xml");


            // Deserialize and display Room Object array
            Console.WriteLine("Room Array After Deserlialization:");
            Console.WriteLine("---------------------------");

            Room[] room3 = null;
            if (os.deserializeObjectArray(ref room3, @"t6.xml"))
            {
                for (int i = 0; i < room3.Length; i++)
                    Console.WriteLine(room3[i].getInfo());
            }
            else
            {
                Console.WriteLine("Deserlize function problem!");
            }


            //
            // PART 4 TEST
            // Serialize All Object array to one file
            os.serializeAllObjectArray(customer, hotel, room, os.getSamplePath(4));


            // Deserialize and display All Object array
            Console.WriteLine("ALL OBJECT SERIALIZE PART ->");
            Console.WriteLine("All Objects Array (from one file) After Deserlialization:");
            Console.WriteLine("---------------------------");

            Customer[] customer4 = null;
            Hotel[] hotel4 = null;
            Room[] room4 = null;
            if (os.deserializeAllObjectArray(ref customer4, ref hotel4, ref room4, os.getSamplePath(4)))
            {
                for (int i = 0; i < customer4.Length; i++)
                    Console.WriteLine(customer4[i].getInfo());

                for (int i = 0; i < hotel4.Length; i++)
                    Console.WriteLine(hotel4[i].getInfo());

                for (int i = 0; i < room4.Length; i++)
                    Console.WriteLine(room4[i].getInfo());
            }
            else
            {
                Console.WriteLine("Deserlize function problem!");
            }
        }
    }
}
