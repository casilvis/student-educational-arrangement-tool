using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEATLibrary
{
    class Chair
    {
        // ATTRIBUTES
        //private int primaryID;
        //private Room room;
        //private int posX;
        //private int posY;
        private Boolean leftHanded; //True for left handed people
        private int fbPosition; // 1 = front; 2 = middle; 3 = back
        private int lrPosition; // 1 = left;  2 = right;  3 = back
        private Boolean nonChair; //True if this actually isn't a chair
        private Boolean mustBeEmpty; //True if this chair can't have anyone in it
        private String seatNumber; //The humber assigned to the seat

        private Student theStudent;


        // PROPERTIES
        public Boolean LeftHanded
        {
            get { return leftHanded; }
            set { leftHanded = value; }
        }
        public Boolean NonCHair
        {
            get { return nonChair; }
            set { nonChair = value; }
        }
        public Boolean MustBeEmpty
        {
            get { return mustBeEmpty; }
            set { mustBeEmpty = value; }
        }
        public String SeatNumber
        {
            get { return seatNumber; }
            set { seatNumber = value; }
        }
        public Student TheStudent
        {
            get { return theStudent; }
            set { theStudent = value; }
        }



        // CONSTRUCTORS
        public Chair()
        {
            leftHanded = false;
            fbPosition = 0;
            lrPosition = 0;
            nonChair = false;
            mustBeEmpty = false;
            seatNumber = "Unknown";

            student = null;
        }


        // METHODS
        public override string ToString()
        {
            String str = "";
            if (leftHanded)
            {
                str += "L";
            }
            else
            {
                str += "R";
            }
            return str;
        }
    }
}
