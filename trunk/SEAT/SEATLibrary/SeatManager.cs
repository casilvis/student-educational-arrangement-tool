using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEATLibrary
{
    public class SeatManager
    {
        private List<Student> students;
        private List<Room> rooms;

        public List<Room> RoomList
        {
            get { return rooms; }
        }


        public SeatManager()
        {
            students = new List<Student>();
            rooms = new List<Room>();

            rooms.Add(new Room());

            
        }


    }
}
