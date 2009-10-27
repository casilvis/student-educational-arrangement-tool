namespace SEATLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Chair
    {
        // ATTRIBUTES
        private bool leftHanded; // True for left handed people
        private int fbPosition; // 0 = front; 1 = middle; 2 = back
        private int lrPosition; // 0 = left;  1 = right;  2 = back
        private bool nonChair; // True if this actually isn't a chair
        private bool mustBeEmpty; // True if this chair can't have anyone in it
        private string seatNumber; // The humber assigned to the seat
        private Student theStudent;

        // CONSTRUCTORS
        public Chair()
        {
            this.leftHanded = false;
            this.fbPosition = 1;
            this.lrPosition = 1;
            this.nonChair = false;
            this.mustBeEmpty = false;
            this.seatNumber = "Unknown";
            this.theStudent = null;
        }

        public Chair(bool leftHanded, int fbPosition, int lrPosition, bool nonChair, bool mustBeEmpty, string seatNumber, Student theStudent)
        {
            this.leftHanded = leftHanded;
            this.fbPosition = fbPosition;
            this.lrPosition = lrPosition;
            this.nonChair = nonChair;
            this.mustBeEmpty = mustBeEmpty;
            this.seatNumber = seatNumber;
            this.theStudent = theStudent;
        }

        // PROPERTIES
        public bool LeftHanded
        {
            get { return this.leftHanded; }
            set { this.leftHanded = value; }
        }

        public int FbPosition
        {
            get { return this.fbPosition; }
            set { this.fbPosition = value; }
        }

        public int LrPosition
        {
            get { return this.lrPosition; }
            set { this.lrPosition = value; }
        }

        public bool NonChair
        {
            get { return this.nonChair; }
            set { this.nonChair = value; }
        }

        public bool MustBeEmpty
        {
            get { return this.mustBeEmpty; }
            set { this.mustBeEmpty = value; }
        }

        public string SeatName
        {
            get { return this.seatNumber; }
            set { this.seatNumber = value; }
        }

        public Student TheStudent
        {
            get { return this.theStudent; }
            set { this.theStudent = value; }
        }

        // METHODS
        public bool IsEmpty()
        {
            if (this.theStudent == null)
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
            if (this.nonChair)
            {
                return ".";
            }
            else if (this.mustBeEmpty)
            {
                return "x";
            }
            else if (this.leftHanded)
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
