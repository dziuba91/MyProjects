using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    public interface IRoom
    {
        void setRoomNotAvailable(string id);

        void setRoomAvailable(string id);

        bool getAvailableStatus(string id);

        void setNumPerson(string id, int numPerson);

        void setPricePerNight(string id, float pricePerNight);

        float getPricePerNight();

        void setDescription(string id, string description);

        bool checkRoom(string id);
        
        string getInfo();
    }
}
