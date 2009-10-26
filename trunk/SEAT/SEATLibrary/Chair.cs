using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEATLibrary
{
    public class Chair
    {
        // ATTRIBUTES
        private bool leftHanded; //True for left handed people
        private int fbPosition; // 0 = front; 1 = middle; 2 = back
        private int lrPosition; // 0 = left;  1 = right;  2 = back
        private bool nonChair; //True if this actually isn't a chair
        private bool mustBeEmpty; //True if this chair can't have anyone in it
        private string seatNumber; //The humber assigned to the seat

        private Student theStudent;


        // PROPERTIES
        public bool LeftHanded
        {
            get { return leftHanded; }
            set { leftHanded = value; }
        }
        public int FbPosition
        {
            get { return fbPosition; }
            set { fbPosition = value; }
        }
        public int LrPosition
        {
            get { return lrPosition; }
            set { lrPosition = value;}
        }
        public bool NonChair
        {
            get { return nonChair; }
            set { nonChair = value; }
        }
        public bool MustBeEmpty
        {
            get { return mustBeEmpty; }
            set { mustBeEmpty = value; }
        }
        public string SeatName
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
            fbPosition = 1;
            lrPosition = 1;
            nonChair = false;
            mustBeEmpty = false;
            seatNumber = "Unknown";
            theStudent = null;
        }
        public Chair(bool leftHanded, int fbPosition, int lrPosition, bool nonChair, bool mustBeEmpty,
            string seatNumber, Student theStudent)
        {
            this.leftHanded = leftHanded;
            this.fbPosition = fbPosition;
            this.lrPosition = lrPosition;
            this.nonChair = nonChair;
            this.mustBeEmpty = mustBeEmpty;
            this.seatNumber = seatNumber;
            this.theStudent = theStudent;
        }


        // METHODS
        public bool isEmpty()
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
