using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEATLibrary
{
    public class Room
    {
        //private int primaryId;
        //private RoomType roomType;
        private String roomName;
        private String location;
        private String description;

        private List<Student> roomStudents;
        private Chair[,] chairs;
        private int width;
        private int height;

        public String RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }
        public String Location
        {
            get { return location; }
            set { location = value; }
        }
        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        public Chair[,] Chairs
        {
            get { return chairs; }
        }

        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }
        public List<Student> RoomStudents
        {
            get { return roomStudents; }
        }


        public Room()
        {
            this.width = 4;
            this.height = 4;

            this.roomName = "Unknown";
            this.location = "Unknown";
            this.description = "Unknown";
            roomStudents = new List<Student>();
            chairs = new Chair[this.width, this.height];

            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    chairs[i, j] = new Chair();
                }
            }
        }

        public Room(String roomName, String location, String description, int width, int height)
        {
            this.width = width;
            this.height = height;

            this.roomName = roomName;
            this.location = location;
            this.description = description;
            roomStudents = new List<Student>();
            chairs = new Chair[this.width, this.height];

            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    chairs[i, j] = new Chair();
                }
            }
        }

        public override string ToString()
        {
            String result = "";
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    result += chairs[i, j].ToString() + "\t";
                }
                result += "\n";
            }
            return result;
        }

    }
}
