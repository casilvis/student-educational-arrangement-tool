using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEATLibrary
{
    public class Chair
    {
        // ATTRIBUTES
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
        public Boolean NonChair
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

            theStudent = null;
        }


        // METHODS
        public Boolean isEmpty()
        {
            if (theStudent == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            if (nonChair)
            {
                return ".";
            }
            else if (mustBeEmpty)
            {
                return "x";
            }
            else if (leftHanded)
            {
                return "L";
            }
            else
            {
                return "R";
            }
        }
    }
}
