// <copyright file="Student.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>A preresentation of a student.</summary>
namespace SEATLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A representation of a student.
    /// </summary>
    public class Student : INotifyPropertyChanged
    {
        // Attributes

        /// <summary>
        /// Unique id for each student.
        /// </summary>
        private Guid id;

        /// <summary>
        /// First name of the student.
        /// </summary>
        private string firstName;

        /// <summary>
        /// Last name of the student.
        /// </summary>
        private string lastName;

        /// <summary>
        /// Student Identifier.
        /// </summary>
        private string sid;

        /// <summary>
        /// Section number.
        /// </summary>
        private string section;

        /// <summary>
        /// Handedness of the student.
        /// </summary>
        private bool leftHanded;

        /// <summary>
        /// Visionimpairment status.
        /// </summary>
        private bool visionImpairment;

        // Constructor

        /// <summary>
        /// Initializes a new instance of the Student class.
        /// </summary>
        /// <param name="firstName">First Name.</param>
        /// <param name="lastName">Last Name.</param>
        /// <param name="sid">Student Identifier.</param>
        /// <param name="section">Section Number.</param>
        /// <param name="leftHanded">Left Handedness.</param>
        /// <param name="visionImpairment">Vision Impairment.</param>
        public Student(string firstName, string lastName, string sid, string section, bool leftHanded, bool visionImpairment)
        {
            this.id = Guid.NewGuid();
            this.firstName = firstName;
            this.lastName = lastName;
            this.sid = sid;
            this.section = section;
            this.leftHanded = leftHanded;
            this.visionImpairment = visionImpairment;
        }

        /// <summary>
        /// Initializes a new instance of the Student class.
        /// </summary>
        internal Student()
        {
            this.id = Guid.NewGuid();
            this.firstName = "Default";
            this.lastName = "Default";
            this.sid = "Default";
            this.section = "Default";
            this.leftHanded = false;
            this.visionImpairment = false;
        }

        /// <summary>
        /// Initializes a new instance of the Student class.
        /// </summary>
        /// <param name="id">The student's Guid.</param>
        /// <param name="firstName">The student's First Name.</param>
        /// <param name="lastName">The student's Last Name.</param>
        /// <param name="sid">The Student's Identifier.</param>
        /// <param name="section">The student'sSection Number.</param>
        /// <param name="leftHanded">The student's Handedness status.</param>
        /// <param name="visionImpairment">The student's Vision Impairment status.</param>
        internal Student(Guid id, string firstName, string lastName, string sid, string section, bool leftHanded, bool visionImpairment)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.sid = sid;
            this.section = section;
            this.leftHanded = leftHanded;
            this.visionImpairment = visionImpairment;
        }

        // Events

        /// <summary>
        /// Event for updating properties.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        // Properties

        /// <summary>
        /// Gets the Guid for the student.
        /// </summary>
        /// <value>The unique Guid.</value>
        public Guid Uid
        {
            get { return this.id; }
        }

        /// <summary>
        /// Gets or sets the first name of the student.
        /// </summary>
        /// <value>First Name.</value>
        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                if (value != this.firstName)
                {
                    this.firstName = value;
                    this.NotifyPropertyChanged("FirstName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the last name of the student.
        /// </summary>
        /// <value>Last Name.</value>
        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                if (value != this.lastName)
                {
                    this.lastName = value;
                    this.NotifyPropertyChanged("LastName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the section of the student.
        /// </summary>
        /// <value>Student section.</value>
        public string Section
        {
            get
            {
                return this.section;
            }

            set
            {
                if (value != this.section)
                {
                    this.section = value;
                    this.NotifyPropertyChanged("Section");
                }
            }
        }

        /// <summary>
        /// Gets or sets the student's identifier.
        /// </summary>
        /// <value>Student Identifier.</value>
        public string Sid
        {
            get
            {
                return this.sid;
            }

            set
            {
                if (value != this.sid)
                {
                    this.sid = value;
                    this.NotifyPropertyChanged("Sid");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the student is left handed.
        /// </summary>
        /// <value>Left handedness.</value>
        public bool LeftHanded
        {
            get
            {
                return this.leftHanded;
            }

            set
            {
                if (value != this.leftHanded)
                {
                    this.leftHanded = value;
                    this.NotifyPropertyChanged("LeftHanded");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the students vision impaired.
        /// </summary>
        /// <value>Vision Impairment.</value>
        public bool VisionImpairment
        {
            get
            {
                return this.visionImpairment;
            }

            set
            {
                if (value != this.visionImpairment)
                {
                    this.visionImpairment = value;
                    this.NotifyPropertyChanged("VisionImpairment");
                }
            }
        }

        // Methods

        /// <summary>
        /// Provides a string representation of the student.
        /// </summary>
        /// <returns>String representation of a student.</returns>
        public override string ToString()
        {
            return this.lastName + ", " + this.firstName;
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
