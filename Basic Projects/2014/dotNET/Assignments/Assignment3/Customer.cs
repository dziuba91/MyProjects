using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Assignment3
{
    class Customer : ICustomer
    {
        private int id;
        private string name;
        private string address;
        private string telephone;
        private int numNights;
        private float discount = 0; // values from 0 to 1 - eg.: 0.15 - means 15% discount 
        private bool checkInStatus = false;

        Room room;
        private string idRoom = "";

        string filePath = @"customers.dat";

        public Customer(int id, string name, string address, string telephone, int numNights)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.telephone = telephone;
            this.numNights = numNights;

            this.room = null;
            this.idRoom = convertStringFormat(this.idRoom, 5);

            writeToFile();
        }

        public Customer(int id, string name, string address, string telephone, int numNights, Room room, string idRoom)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.telephone = telephone;
            this.numNights = numNights;

            bookRoom(id, room, idRoom);

            writeToFile();
        }

        public Customer(int id, string name, string address, string telephone, int numNights, ArrayList rooms, string idRoom)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.telephone = telephone;
            this.numNights = numNights;

            bookRoom(id, rooms, idRoom);

            writeToFile();
        }

        public Customer(int id, string name, string address, string telephone, int numNights, Hotel hotel, int idHotel, string idRoom)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.telephone = telephone;
            this.numNights = numNights;

            bookRoom(id, hotel, idHotel, idRoom);

            writeToFile();
        }

        public Customer(int id, string name, string address, string telephone, int numNights, float bonus)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.telephone = telephone;
            this.numNights = numNights;

            this.room = null;
            this.idRoom = convertStringFormat(this.idRoom, 5);

            this.discount = bonus;

            writeToFile();
        }

        public Customer(int id, string name, string address, string telephone, int numNights, Room room, string idRoom, float bonus)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.telephone = telephone;
            this.numNights = numNights;

            bookRoom(id, room, idRoom);

            this.discount = bonus;

            writeToFile();
        }

        public Customer(int id, string name, string address, string telephone, int numNights, ArrayList rooms, string idRoom, float bonus)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.telephone = telephone;
            this.numNights = numNights;

            bookRoom(id, rooms, idRoom);

            this.discount = bonus;

            writeToFile();
        }

        public Customer(int id, string name, string address, string telephone, int numNights, Hotel hotel, int idHotel, string idRoom, float bonus)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.telephone = telephone;
            this.numNights = numNights;

            bookRoom(id, hotel, idHotel, idRoom);

            this.discount = bonus;

            writeToFile();
        }

        public Customer(int id, string name, string address, string telephone, int numNights, ArrayList rooms, string idRoom, float bonus, bool checkInStatus) // only for read object from file
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.telephone = telephone;
            this.numNights = numNights;

            this.checkInStatus = checkInStatus;

            this.discount = bonus;

            this.idRoom = convertStringFormat(idRoom, 5);

            if (!idRoom.Equals("") && !idRoom.Equals("     "))
            {
                setUpBookingInformation(id, rooms, idRoom);
            }

            //writeToFile();
        }

        public void bookRoom(int id, Hotel hotel, int idHotel, string idRoom)
        {
            if (id == this.id)
            {
                string idRoom2 = convertStringFormat(idRoom, 5);
                this.checkInStatus = true;

                this.room = hotel.changeRoomStatus(idHotel, idRoom2, checkInStatus);

                if (this.room != null)
                {
                    this.idRoom = idRoom2;
                }
                else this.checkInStatus = false;

                changeInFile(id, 7);
                changeInFile(id, 6);
            }
        }

        public void bookRoom(int id, ArrayList rooms, string idRoom)
        {
            if (id == this.id)
            {
                string idRoom2 = convertStringFormat(idRoom, 5);

                for (int i = 0; i < rooms.Count; i++)
                {
                    if (((Room)rooms[i]).checkRoom(idRoom2))
                    {
                        this.room = (Room)rooms[i];
                        this.idRoom = idRoom2;

                        if (this.checkInStatus != true)
                            this.room.setRoomNotAvailable(idRoom2);

                        this.checkInStatus = true;
                        changeInFile(id, 7);
                        changeInFile(id, 6);

                        break;
                    }
                }
            }
        }

        public void bookRoom(int id, Room room, string idRoom)
        {
            if (id == this.id && room != null)
            {
                string idRoom2 = convertStringFormat(idRoom, 5);

                room.setRoomNotAvailable(idRoom2);

                this.room = room;
                this.idRoom = idRoom2;

                if (!this.checkInStatus)
                {
                    changeInFile(id, 7);
                    changeInFile(id, 6);
                }

                this.checkInStatus = true;
            }
        }

        public void setUpBookingInformation(int id, ArrayList rooms, string idRoom)
        {
            if (id == this.id)
            {

                for (int i = 0; i < rooms.Count; i++)
                {
                    if (((Room)rooms[i]).checkRoom(idRoom))
                    {
                        this.room = (Room)rooms[i];

                        break;
                    }
                }
            }
        }

        public void checkOut(int id, Hotel hotel, int idHotel, string idRoom)
        {
            if (id == this.id)
            {
                this.checkInStatus = false;

                this.room = hotel.changeRoomStatus(idHotel, convertStringFormat(idRoom, 5), checkInStatus);

                this.idRoom = convertStringFormat("", 5);

                changeInFile(id, 7);
                changeInFile(id, 6);
            }
        }

        public void checkOut(int id, string idRoom)
        {
            if (id == this.id)
            {
                this.room.setRoomAvailable(idRoom);

                this.room = null;
                this.idRoom = convertStringFormat("", 5);
                this.checkInStatus = false;

                changeInFile(id, 7);
                changeInFile(id, 6);
            }
        }

        public float getPrice(int id)
        {
            if (id == this.id && room != null)
            {
                float price = numNights * room.getPricePerNight();
                return price - (price * discount);
            }

            return -1;
        }

        public string getInfo(int id)
        {
            string txt = "";

            if (id == this.id)
            {
                txt += "Customer ID: ---" + id + "---\n";
                txt += "Name: " + name + "\n";
                txt += "Address: " + address + "\n";
                txt += "Telephone Number: " + telephone + "\n";

                if (room != null)
                {
                    txt += "ROOM: \n" + room.getInfo();
                    txt += "Number of nights: " + numNights + "\n";
                    txt += "Discount: " + discount * 100 + "%\n";
                    txt += "Price: " + getPrice(id) + "\n";
                }
            }

            return txt;
        }

        public string getInfo()
        {
            string txt = "";

            txt += "Customer ID: ---" + id + "---\n";
            txt += "Name: " + name + "\n";
            txt += "Address: " + address + "\n";
            txt += "Telephone Number: " + telephone + "\n";
            txt += "Room ID: " + idRoom + "\n";
            txt += "Check IN Status: " + checkInStatus + "\n";

            if (room != null)
            {
                txt += "ROOM: \n" + room.getInfo();
                txt += "Number of nights: " + numNights + "\n";
                txt += "Discount: " + discount * 100 + "%\n";
                txt += "TOTAL : " + getPrice(id) + "\n";
            }

            return txt;
        }

        private string convertStringFormat(string txt, int numbOfChar)
        {
            string txt2 = "";
            for (int i = 0; i < numbOfChar; i++)
            {
                if (i < txt.Length) txt2 += txt[i];
                else txt2 += " ";
            }

            return txt2;
        }

        private void writeToFile()
        {
            BinaryWriter binaryWriter;

            try
            {
                binaryWriter = new BinaryWriter(new FileStream(filePath, FileMode.OpenOrCreate));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\nCannot open " + filePath);
                return;
            }

            try
            {
                binaryWriter.Seek(0, SeekOrigin.End);

                binaryWriter.Write(this.id);
                binaryWriter.Write(this.name);
                binaryWriter.Write(this.address);
                binaryWriter.Write(this.telephone);
                binaryWriter.Write(this.numNights);
                binaryWriter.Write(this.discount);
                binaryWriter.Write(this.checkInStatus);

                binaryWriter.Write(this.idRoom);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\nWrite error.");
            }

            binaryWriter.Close();
        }

        private void changeInFile(int id, int offset)
        {
            bool customerFound = false;
            long index = 0;

            BinaryReader binaryReader;

            int idCustomer;
            string name; // offset = 1
            string address; // offset = 2
            string telephone; // offset = 3
            int numNights; // offset = 4
            float discount; // offset = 5
            bool checkInStatus; // offset = 6

            //Room room;
            string idRoom; // offset = 7

            try
            {
                binaryReader = new BinaryReader(new FileStream(filePath, FileMode.Open));
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message + "\nCannot open " + filePath);
                return;
            }

            try
            {
                for (; ; )
                {
                    idCustomer = binaryReader.ReadInt32();

                    if ((id == idCustomer) && offset == 1)
                    {
                        customerFound = true;
                        break;
                    }
                    name = binaryReader.ReadString();

                    if ((id == idCustomer) && offset == 2)
                    {
                        customerFound = true;
                        break;
                    }
                    address = binaryReader.ReadString();

                    if ((id == idCustomer) && offset == 3)
                    {
                        customerFound = true;
                        break;
                    }
                    telephone = binaryReader.ReadString();

                    if ((id == idCustomer) && offset == 4)
                    {
                        customerFound = true;
                        break;
                    }
                    numNights = binaryReader.ReadInt32();

                    if ((id == idCustomer) && offset == 5)
                    {
                        customerFound = true;
                        break;
                    }
                    discount = binaryReader.ReadSingle();

                    if ((id == idCustomer) && offset == 6)
                    {
                        customerFound = true;
                        break;
                    }
                    checkInStatus = binaryReader.ReadBoolean();

                    if ((id == idCustomer) && offset == 7)
                    {
                        customerFound = true;
                        break;
                    }
                    idRoom = binaryReader.ReadString();
                }

                if (customerFound)
                    index = binaryReader.BaseStream.Position;
                else return;
            }
            catch (EndOfStreamException)
            {
                return;
            }
            catch (IOException e)
            {
                Console.WriteLine("Read error." + e.Message);
            }

            binaryReader.Close();


            BinaryWriter binaryWriter;

            try
            {
                binaryWriter = new BinaryWriter(new FileStream(filePath, FileMode.Open));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\nCannot open " + filePath);
                return;
            }

            try
            {
                binaryWriter.Seek((int)index, SeekOrigin.Begin);

                if (offset == 1) binaryWriter.Write(this.name);
                else if (offset == 2) binaryWriter.Write(this.address);
                else if (offset == 3) binaryWriter.Write(this.telephone);
                else if (offset == 4) binaryWriter.Write(this.numNights);
                else if (offset == 5) binaryWriter.Write(this.discount);
                else if (offset == 6) binaryWriter.Write(this.checkInStatus);
                else if (offset == 7) binaryWriter.Write(this.idRoom);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\nWrite error.");
            }

            binaryWriter.Close();
        }
    }
}
