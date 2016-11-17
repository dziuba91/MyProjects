using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Assignment3
{
    [Serializable]
    public class Hotel
    {
        private int id;
        private string name;
        private string address;

        private ArrayList rooms = new ArrayList();

        public Hotel(int id, string name, string address)
        {
            this.id = id;
            this.name = name;
            this.address = address;
        }

        public Hotel(int id, string name, string address, ArrayList rooms)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.rooms = rooms;
        }

        public void addRoom(int id, Room room)
        {
            if (id == this.id)
                rooms.Add(room);
        }

        public void addRoom(int id, string idNumber, int numPerson, float pricePerNight)
        {
            if (id == this.id)
                rooms.Add(new Room(idNumber, numPerson, pricePerNight));
        }

        public void addRoom(int id, string idNumber, int numPerson, float pricePerNight, string description)
        {
            if (id == this.id)
                rooms.Add(new Room(idNumber, numPerson, pricePerNight, description));
        }

        public Room changeRoomStatus(int id, string idRoomNumber, bool status)
        {
            Room room = null;

            if (id == this.id)
            {
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (((Room)rooms[i]).checkRoom(idRoomNumber))
                    {
                        if (status == true)
                            ((Room)rooms[i]).setRoomNotAvailable(idRoomNumber);
                        else
                        {
                            ((Room)rooms[i]).setRoomAvailable(idRoomNumber);
                            break;
                        }

                        room = (Room)rooms[i];

                        return room;
                    }
                }
            }

            return room;
        }

        public void actualizeRooms(int id, ArrayList rooms)
        {
            this.rooms = rooms;
        }

        public string getInfo()
        {
            string txt = "";

            txt += "Hotel ID: " + id + "\n";
            txt += "HOTEL: " + name + "\n";
            txt += "Address: " + address + "\n";
            txt += "  ROOMS : \n";

            for (int i = 0; i < rooms.Count; i++)
            {
                txt += "   " + ((Room)rooms[i]).getInfo();
            }

            return txt;
        }

        public string getInfo(int id)
        {
            string txt = "";

            if (id == this.id)
            {
                txt += "Hotel ID: " + id + "\n";
                txt += "HOTEL: " + name + "\n";
                txt += "Address: " + address + "\n";
                txt += "  ROOMS : \n";

                for (int i = 0; i < rooms.Count; i++)
                {
                    txt += "   " + ((Room)rooms[i]).getInfo();
                }
            }

            return txt;
        }
    }
}
