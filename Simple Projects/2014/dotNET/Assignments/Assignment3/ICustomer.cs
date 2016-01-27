using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Assignment3
{
    public interface ICustomer
    {
        void bookRoom(int id, Hotel hotel, int idHotel, string idRoom);

        void bookRoom(int id, ArrayList rooms, string idRoom);

        void bookRoom(int id, Room room, string idRoom);

        void setUpBookingInformation(int id, ArrayList rooms, string idRoom);

        void checkOut(int id, Hotel hotel, int idHotel, string idRoom);

        void checkOut(int id, string idRoom);

        float getPrice(int id);

        string getInfo(int id);
    }
}
