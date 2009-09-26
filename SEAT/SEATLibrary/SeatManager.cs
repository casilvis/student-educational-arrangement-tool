using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEATLibrary
{
    public class SeatManager
    {
        // Attributes
        private List<Student> students;
        private List<Room> rooms;

        // Properties
        public List<Room> RoomList
        {
            get { return rooms; }
        }
        public List<Student> StudentList
        {
            get { return students; }
        }

        // Constructors
        public SeatManager()
        {
            students = new List<Student>();
            rooms = new List<Room>();
        }


        // Methods
        public void addStudentToRoster(Student student)
        {
            students.Add(student);
        }

        public Room addNewRoom(int width, int height)
        {
            Room r = new Room(width, height);
            rooms.Add(r);
            return r;
        }

        public void addNewRoom(Room room)
        {
            rooms.Add(room);
        }

        public Boolean addStudentToRoom(int student, int room)
        {
            return false;
        }



    }
}
