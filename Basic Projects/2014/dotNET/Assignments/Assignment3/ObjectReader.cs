using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Collections;
using System.IO;

namespace Assignment3
{
    class ObjectReader
    {
        string filePathRoom = @"rooms.dat";
        string filePathCustomer = @"customers.dat";

        string filePathHotel = @"hotel.xml";

        public ArrayList getRoomsList()
        {
            ArrayList rooms = new ArrayList();

            BinaryReader binaryReader;

            string idNumber;
            int numPerson;
            float pricePerNight;
            string description;
            bool available;

            try
            {
                binaryReader = new BinaryReader(new FileStream(filePathRoom, FileMode.Open));
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message + "\nCannot open " + filePathRoom);
                return rooms;
            }

            try
            {
                for (; ; )
                {
                    idNumber = binaryReader.ReadString();
                    numPerson = binaryReader.ReadInt32();
                    pricePerNight = binaryReader.ReadSingle();
                    description = binaryReader.ReadString();
                    available = binaryReader.ReadBoolean();

                    rooms.Add(new Room(idNumber, numPerson, pricePerNight, description, available));
                }
            }
            catch (EndOfStreamException) { }
            catch (IOException e)
            {
                Console.WriteLine("Read error." + e.Message);
            }

            binaryReader.Close();

            return rooms;
        }

        public ArrayList getCustomersList(ArrayList rooms)
        {
            ArrayList customers = new ArrayList();

            BinaryReader binaryReader;

            int id;
            string name;
            string address;
            string telephone;
            int numNights;
            float discount = 0;
            bool checkInStatus = false;

            //Room room;
            string idRoom = "";


            try
            {
                binaryReader = new BinaryReader(new FileStream(filePathCustomer, FileMode.Open));
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message + "\nCannot open " + filePathCustomer);
                return customers;
            }

            try
            {
                for (; ; )
                {
                    id = binaryReader.ReadInt32();
                    //Console.WriteLine("-->" + id);
                    name = binaryReader.ReadString();
                    //Console.WriteLine("-->" + name);
                    address = binaryReader.ReadString();
                    telephone = binaryReader.ReadString();
                    numNights = binaryReader.ReadInt32();
                    discount = binaryReader.ReadSingle();
                    checkInStatus = binaryReader.ReadBoolean();

                    idRoom = binaryReader.ReadString();

                    customers.Add(new Customer(id, name, address, telephone, numNights, rooms, idRoom, discount, checkInStatus));
                }
            }
            catch (EndOfStreamException) { }
            catch (IOException e)
            {
                Console.WriteLine("Read error." + e.Message);
            }

            binaryReader.Close();

            return customers;
        }

        public void serializeHotel(Hotel hotel)
        {
            FileStream fileStream = new FileStream(filePathHotel, FileMode.OpenOrCreate);

            SoapFormatter soapFormatter = new SoapFormatter();

            soapFormatter.Serialize(fileStream, hotel);

            fileStream.Flush();
            fileStream.Close();
        }

        public Hotel deserializeHotel()
        {
            FileStream fileStream = new FileStream(filePathHotel, FileMode.Open);

            SoapFormatter soapFormatter = new SoapFormatter();

            object obj = null;
            Hotel hotel = null;

            try
            {
                obj = soapFormatter.Deserialize(fileStream);

                if (obj is Hotel)
                {
                    hotel = (Hotel)obj;
                }
            }
            catch (EndOfStreamException e)
            {
                Console.WriteLine("No object left!" + e.Message);
            }
            catch (SerializationException)
            {
                //Console.WriteLine(e.Message);
            }
            catch (System.Xml.XmlException)
            {
                //Console.WriteLine(e.Message);
            }

            fileStream.Flush();
            fileStream.Close();

            return hotel;
        }

        public void serializeHotel(Hotel[] hotel)
        {
            FileStream fileStream = new FileStream(filePathHotel, FileMode.OpenOrCreate);

            SoapFormatter soapFormatter = new SoapFormatter();

            for (int i = 0; i < hotel.Length; i++)
                soapFormatter.Serialize(fileStream, hotel[i]);

            fileStream.Flush();
            fileStream.Close();
        }

        public Hotel[] deserializeHotelAsArray()
        {
            FileStream fileStream = new FileStream(filePathHotel, FileMode.Open);

            SoapFormatter soapFormatter = new SoapFormatter();

            object obj = null;

            ArrayList list = new ArrayList();

            for (; ; )
            {
                try
                {
                    obj = soapFormatter.Deserialize(fileStream);

                    if (obj is Hotel)
                    {
                        list.Add((Hotel)obj);
                    }
                }
                catch (EndOfStreamException) { }
                catch (SerializationException)
                {
                    //Console.WriteLine(e.Message);
                    break;
                }
                catch (System.Xml.XmlException)
                {
                    //Console.WriteLine(e.Message);
                    break;
                }
            }

            Hotel[] hotel = new Hotel[list.Count];

            for (int i = 0; i < list.Count; i++)
                hotel[i] = (Hotel)list[i];

            fileStream.Flush();
            fileStream.Close();

            return hotel;
        }

        public void clearFiles()
        {
            BinaryWriter binaryWriter;

            try
            {
                binaryWriter = new BinaryWriter(new FileStream(filePathRoom, FileMode.Create));
                binaryWriter.Flush();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\nCannot open " + filePathRoom);
                return;
            }

            binaryWriter.Close();


            try
            {
                binaryWriter = new BinaryWriter(new FileStream(filePathCustomer, FileMode.Create));
                binaryWriter.Flush();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\nCannot open " + filePathCustomer);
                return;
            }

            binaryWriter.Close();

            /*
            FileStream fileStream; 
            
            try
            {
                fileStream = new FileStream(filePathHotel, FileMode.Create);
                binaryWriter.Flush();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\nCannot open " + filePathHotel);
                return;
            }

            fileStream.Close(); */
        }
    }
}
