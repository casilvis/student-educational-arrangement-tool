// <copyright file="Chair.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEATLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A representation of a char that is placed inside of a Room.
    /// </summary>
    public class Chair : INotifyPropertyChanged
    {
        // ATTRIBUTES

        /// <summary>
        /// Handedness, true for left handed people.
        /// </summary>
        private bool leftHanded;

        /// <summary>
        /// 0 = front; 1 = middle; 2 = back.
        /// </summary>
        private int frontBackPosition;

        /// <summary>
        /// 0 = left;  1 = right;  2 = back.
        /// </summary>
        private int leftRightPosition;

        /// <summary>
        /// True if this actually isn't a chair.
        /// </summary>
        private bool nonChair;

        /// <summary>
        /// True if this chair can't have anyone in it.
        /// </summary>
        private bool mustBeEmpty;

        /// <summary>
        /// The humber assigned to the seat.
        /// </summary>
        private string seatNumber;

        /// <summary>
        /// The student who is sitting in the chair.
        /// </summary>
        private Student theStudent;

        // CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the Chair class.
        /// </summary>
        public Chair()
        {
            this.leftHanded = false;
            this.frontBackPosition = 1;
            this.leftRightPosition = 1;
            this.nonChair = false;
            this.mustBeEmpty = false;
            this.seatNumber = "Unknown";
            this.theStudent = null;
        }

        /// <summary>
        /// Initializes a new instance of the Chair class using given parameters.
        /// </summary>
        /// <param name="leftHanded">True if the chair is left handed.</param>
        /// <param name="frontBackPosition">0, 1, or 2 depending on the front to back position in room.</param>
        /// <param name="leftRightPosition">0, 1, or 2 depending on the left to right position in room.</param>
        /// <param name="nonChair">True if the chair is not a chair, it can be an aisle or simplly not exist.</param>
        /// <param name="mustBeEmpty">True if a student is not allowed to be placed in this chair.</param>
        /// <param name="seatNumber">The given string representation for the chair.</param>
        /// <param name="theStudent">The student who is sitting in this chair, null for an empty chair.</param>
        public Chair(bool leftHanded, int frontBackPosition, int leftRightPosition, bool nonChair, bool mustBeEmpty, string seatNumber, Student theStudent)
        {
            this.leftHanded = leftHanded;
            this.frontBackPosition = frontBackPosition;
            this.leftRightPosition = leftRightPosition;
            this.nonChair = nonChair;
            this.mustBeEmpty = mustBeEmpty;
            this.seatNumber = seatNumber;
            this.theStudent = theStudent;
        }

        // Events

        /// <summary>
        /// Event for updating properties.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        // PROPERTIES

        /// <summary>
        /// Gets or sets a value indicating whether the chair chair is lefthanded.
        /// </summary>
        /// <value>True if the chair is left handed.</value>
        public bool LeftHanded
        {
            get
            {
                return this.leftHanded;
            }
            set
            {
                if (this.leftHanded != value)
                {
                    this.leftHanded = value;
                    this.NotifyPropertyChanged("LeftHanded");
                }
            }
        }

        /// <summary>
        /// Gets or sets the front to back position of the chair.
        /// </summary>
        /// <value>0, 1, or 2 depending on front to back positioning of the chair.</value>
        public int FbPosition
        {
            get
            {
                return this.frontBackPosition;
            }
            set
            {
                if (this.frontBackPosition != value)
                {
                    this.frontBackPosition = value;
                    this.NotifyPropertyChanged("FbPosition");
                }
            }
        }

        /// <summary>
        /// Gets or sets the left to right position of the chair.
        /// </summary>
        /// <value>0, 1, or 2 depending on the left to right positioning of the chair.</value>
        public int LrPosition
        {
            get
            {
                return this.leftRightPosition;
            }
            set
            {
                if (this.leftRightPosition != value)
                {
                    this.leftRightPosition = value;
                    this.NotifyPropertyChanged("LrPosition");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the chair is actually a chair.
        /// </summary>
        /// <value>True if the chair is not a chair, could be an aisle or simply not exist.</value>
        public bool NonChair
        {
            get
            {
                return this.nonChair;
            }
            set
            {
                if (this.nonChair != value)
                {
                    this.nonChair = value;
                    this.NotifyPropertyChanged("NonChair");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this chair must be empty.
        /// </summary>
        /// <value>True if a student is not allowed to be placed in this chair.</value>
        public bool MustBeEmpty
        {
            get
            {
                return this.mustBeEmpty;
            }
            set
            {
                if (this.mustBeEmpty != value)
                {
                    this.mustBeEmpty = value;
                    this.NotifyPropertyChanged("MustBeEmpty");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of this chair.
        /// </summary>
        /// <value>String representation of the chairs position in the room.</value>
        public string SeatName
        {
            get
            {
                return this.seatNumber;
            }
            set
            {
                if (this.seatNumber != value)
                {
                    this.seatNumber = value;
                    this.NotifyPropertyChanged("SeatName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the student who is sitting in this chair.
        /// </summary>
        /// <value>The student who is currently sitting in the chair, null for an empty chair.</value>
        public Student TheStudent
        {
            get
            {
                return this.theStudent;
            }
            set
            {
                if (this.theStudent != value)
                {
                    this.theStudent = value;
                    this.NotifyPropertyChanged("TheStudent");
                }
            }
        }

        // METHODS

        /// <summary>
        /// True is there is not student sitting in this chair.
        /// </summary>
        /// <returns>True if the seat is empty.</returns>
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

        /// <summary>
        /// Returns a string representation of this chair.
        /// </summary>
        /// <returns>String representation of this chair.</returns>
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

        /// <summary>
        /// Signals that a property of this object has changed.
        /// </summary>
        /// <param name="info">The property that is being affected.</param>
        private void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
                SeatManager.MarkDirty();
            }
        }
    }
}
