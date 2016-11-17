using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace Assignment4
{
    class ObjectSerialization
    {
        string filePathRoom = @"rooms.xml";
        
        string filePathCustomer = @"customers.xml";

        string filePathHotel = @"hotel.xml";

        string filePathAll = @"objects.xml";


        public void serializeObject(Hotel hotel, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);

            SoapFormatter soapFormatter = new SoapFormatter();

            soapFormatter.Serialize(fileStream, hotel);

            fileStream.Flush();
            fileStream.Close();
        }

        public bool deserializeObject(ref Hotel hotel, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);

            SoapFormatter soapFormatter = new SoapFormatter();

            object obj = null;
            //Hotel hotel = null;
            hotel = null;

            try
            {
                obj = soapFormatter.Deserialize(fileStream);

                if (obj is Hotel)
                {
                    hotel = (Hotel)obj;

                    return true;
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

            return false;
        }

        public void serializeObject(Room room, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);

            SoapFormatter soapFormatter = new SoapFormatter();

            soapFormatter.Serialize(fileStream, room);

            fileStream.Flush();
            fileStream.Close();
        }

        public bool deserializeObject(ref Room room, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);

            SoapFormatter soapFormatter = new SoapFormatter();

            object obj = null;
            //Room room = null;
            room = null;

            try
            {
                obj = soapFormatter.Deserialize(fileStream);

                if (obj is Room)
                {
                    room = (Room)obj;
                    
                    return true;
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

            return false;
        }

        public void serializeObject(Customer customer, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);

            SoapFormatter soapFormatter = new SoapFormatter();

            soapFormatter.Serialize(fileStream, customer);

            fileStream.Flush();
            fileStream.Close();
        }

        public bool deserializeObject(ref Customer customer, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);

            SoapFormatter soapFormatter = new SoapFormatter();

            object obj = null;
            //Customer customer = null;
            customer = null;

            try
            {
                obj = soapFormatter.Deserialize(fileStream);

                if (obj is Customer)
                {
                    customer = (Customer)obj;

                    return true;
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

            return false;
        }


        //
        // ***********************
        // Serialize Object Arrays

        public void serializeObjectArray(Hotel[] hotel, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);

            SoapFormatter soapFormatter = new SoapFormatter();

            for (int i = 0; i < hotel.Length; i++)
                soapFormatter.Serialize(fileStream, hotel[i]);

            fileStream.Flush();
            fileStream.Close();
        }

        public bool deserializeObjectArray(ref Hotel [] hotel, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);

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
                    else return false;
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

            hotel = new Hotel[list.Count];
            bool execute = false;

            for (int i = 0; i < list.Count; i++)
            {
                hotel[i] = (Hotel)list[i];

                execute = true;
            }

            fileStream.Flush();
            fileStream.Close();

            if (execute) 
                return true;

            return false;
        }

        public void serializeObjectArray(Room[] room, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);

            SoapFormatter soapFormatter = new SoapFormatter();

            for (int i = 0; i < room.Length; i++)
                soapFormatter.Serialize(fileStream, room[i]);

            fileStream.Flush();
            fileStream.Close();
        }

        public bool deserializeObjectArray(ref Room [] room, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);

            SoapFormatter soapFormatter = new SoapFormatter();

            object obj = null;

            ArrayList list = new ArrayList();

            for (; ; )
            {
                try
                {
                    obj = soapFormatter.Deserialize(fileStream);

                    if (obj is Room)
                    {
                        list.Add((Room)obj);
                    }
                    else return false;
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

            room = new Room[list.Count];
            bool execute = false;

            for (int i = 0; i < list.Count; i++)
            {
                room[i] = (Room)list[i];

                execute = true;
            }

            fileStream.Flush();
            fileStream.Close();

            if (execute)
                return true;

            return false;
        }

        public void serializeObjectArray(Customer[] customer, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);

            SoapFormatter soapFormatter = new SoapFormatter();

            for (int i = 0; i < customer.Length; i++)
                soapFormatter.Serialize(fileStream, customer[i]);

            fileStream.Flush();
            fileStream.Close();
        }

        public bool deserializeObjectArray(ref Customer [] customer, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);

            SoapFormatter soapFormatter = new SoapFormatter();

            object obj = null;

            ArrayList list = new ArrayList();

            for (; ; )
            {
                try
                {
                    obj = soapFormatter.Deserialize(fileStream);

                    if (obj is Customer)
                    {
                        list.Add((Customer)obj);
                    }
                    else return false;
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

            customer = new Customer[list.Count];
            bool execute = false;

            for (int i = 0; i < list.Count; i++)
            {
                customer[i] = (Customer)list[i];

                execute = true;
            }

            fileStream.Flush();
            fileStream.Close();

            if (execute)
                return true;

            return false;
        }

        public void serializeAllObjectArray(Customer[] customer, Hotel[] hotel, Room[] room, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);

            SoapFormatter soapFormatter = new SoapFormatter();

            for (int i = 0; i < customer.Length; i++)
                soapFormatter.Serialize(fileStream, customer[i]);

            for (int i = 0; i < hotel.Length; i++)
                soapFormatter.Serialize(fileStream, hotel[i]);

            for (int i = 0; i < room.Length; i++)
                soapFormatter.Serialize(fileStream, room[i]);

            fileStream.Flush();
            fileStream.Close();
        }

        public bool deserializeAllObjectArray(ref Customer[] customer, ref Hotel[] hotel, ref Room[] room, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);

            SoapFormatter soapFormatter = new SoapFormatter();

            object obj = null;

            ArrayList list1 = new ArrayList();
            ArrayList list2 = new ArrayList();
            ArrayList list3 = new ArrayList();

            for (; ; )
            {
                try
                {
                    obj = soapFormatter.Deserialize(fileStream);

                    if (obj is Customer)
                    {
                        list1.Add((Customer)obj);
                    }
                    else if (obj is Hotel)
                    {
                        list2.Add((Hotel)obj);
                    }
                    else if (obj is Room)
                    {
                        list3.Add((Room)obj);
                    }
                    else return false;
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

            customer = new Customer[list1.Count];

            for (int i = 0; i < list1.Count; i++)
            {
                customer[i] = (Customer)list1[i];
            }

            hotel = new Hotel[list2.Count];

            for (int i = 0; i < list2.Count; i++)
            {
                hotel[i] = (Hotel)list2[i];
            }

            room = new Room[list3.Count];

            for (int i = 0; i < list3.Count; i++)
            {
                room[i] = (Room)list3[i];
            }

            fileStream.Flush();
            fileStream.Close();

            return true;
        }


        public string getSamplePath(int sampleNumber)
        {
            string txt = "";

            if (sampleNumber == 1) txt += filePathCustomer;
            else if (sampleNumber == 2) txt += filePathHotel;
            else if (sampleNumber == 3) txt += filePathRoom;
            else if (sampleNumber == 4) txt += filePathAll;

            return txt;
        }
    }
}
