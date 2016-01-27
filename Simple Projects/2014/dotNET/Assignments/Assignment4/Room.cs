using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Assignment4
{
    [Serializable]
    public class Room : IRoom
    {
        private string idNumber;
        private int numPerson;
        private float pricePerNight;
        private string description = "";
        private bool available = true;

        string filePath = @"rooms.dat";

        public Room(string idNumber, int numPerson, float pricePerNight)
        {
            this.idNumber = convertStringFormat(idNumber, 5); //max 5 characters
            this.numPerson = numPerson;
            this.pricePerNight = pricePerNight;
            this.description = convertStringFormat(this.description, 40); //max 40 characters

            writeToFile();
        }

        public Room(string idNumber, int numPerson, float pricePerNight, string description)
        {
            this.idNumber = convertStringFormat(idNumber, 5);
            this.numPerson = numPerson;
            this.pricePerNight = pricePerNight;
            this.description = convertStringFormat(description, 40);

            writeToFile();
        }

        public Room(string idNumber, int numPerson, float pricePerNight, string description, bool available) // only for read object from file
        {
            this.idNumber = convertStringFormat(idNumber, 5);
            this.numPerson = numPerson;
            this.pricePerNight = pricePerNight;
            this.description = convertStringFormat(description, 40);
            this.available = available;

            //writeToFile();
        }

        public string getInfo()
        {
            string txt = "";

            txt += "Room Number: ---" + idNumber + "---\n";
            txt += "Room for: " + numPerson;

            if (numPerson == 1) txt += " Person \n";
            else txt += " Persons \n";

            txt += "Description: ";

            if (description.Equals("")) txt += "(none)\n";
            else txt += description + "\n";

            txt += "Price: " + pricePerNight + "\n";
            txt += "Room Status: ";

            if (available) txt += "available\n";
            else txt += "not available\n";

            return txt;
        }

        public void setRoomNotAvailable(string id)
        {
            if (convertStringFormat(id, 5).Equals(idNumber))
            {
                available = false;

                changeInFile(convertStringFormat(id, 5), 4);
            }
        }

        public void setRoomAvailable(string id)
        {
            if (convertStringFormat(id, 5).Equals(idNumber))
            {
                available = true;

                changeInFile(convertStringFormat(id, 5), 4);
            }
        }

        public bool getAvailableStatus(string id)
        {
            return this.available;
        }

        public void setNumPerson(string id, int numPerson)
        {
            if (convertStringFormat(id, 5).Equals(idNumber))
            {
                this.numPerson = numPerson;

                changeInFile(convertStringFormat(id, 5), 1);
            }
        }

        public void setPricePerNight(string id, float pricePerNight)
        {
            if (convertStringFormat(id, 5).Equals(idNumber))
            {
                this.pricePerNight = pricePerNight;

                changeInFile(convertStringFormat(id, 5), 2);
            }
        }

        public float getPricePerNight()
        {
            return pricePerNight;
        }

        public void setDescription(string id, string description)
        {
            if (convertStringFormat(id, 5).Equals(idNumber))
            {
                this.description = convertStringFormat(description, 40);

                changeInFile(convertStringFormat(id, 5), 3);
            }
        }

        public bool checkRoom(string id)
        {
            return this.idNumber.Equals(id);
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

                binaryWriter.Write(idNumber);
                binaryWriter.Write(numPerson);
                binaryWriter.Write(pricePerNight);
                binaryWriter.Write(description);
                binaryWriter.Write(available);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\nWrite error.");
            }

            binaryWriter.Close();
        }

        private void changeInFile(string id, int offset)
        {
            long index = 0;

            BinaryReader binaryReader;

            string idNumber;
            int numPerson; // offset = 1
            float pricePerNight; // offset = 2
            string description; // offset = 3
            bool available; // offset = 4

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
                    idNumber = binaryReader.ReadString();

                    if (idNumber.Equals(id) && offset == 1) break;
                    numPerson = binaryReader.ReadInt32();

                    if (idNumber.Equals(id) && offset == 2) break;
                    pricePerNight = binaryReader.ReadSingle();

                    if (idNumber.Equals(id) && offset == 3) break;
                    description = binaryReader.ReadString();

                    if (idNumber.Equals(id) && offset == 4) break;
                    available = binaryReader.ReadBoolean();
                }

                index = binaryReader.BaseStream.Position;
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
                binaryWriter = new BinaryWriter(new FileStream(filePath, FileMode.OpenOrCreate));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\nCannot open " + filePath);
                return;
            }

            try
            {
                binaryWriter.Seek((int)index, SeekOrigin.Begin);

                if (offset == 1) binaryWriter.Write(this.numPerson);
                else if (offset == 2) binaryWriter.Write(this.pricePerNight);
                else if (offset == 3) binaryWriter.Write(this.description);
                else if (offset == 4) binaryWriter.Write(this.available);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\nWrite error.");
            }

            binaryWriter.Close();
        }
    }
}
