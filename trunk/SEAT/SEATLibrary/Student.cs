namespace SEATLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    public class Student : INotifyPropertyChanged
    {
        // Attributes
        private Guid id;
        private string firstName;
        private string lastName;
        private string sid;
        private string section;
        private bool leftHanded;
        private bool visionImpairment;
        private bool isEnrolled;

        // Constructor
        public Student(string firstName, string lastName, string sid, string section, bool leftHanded, bool visionEnpairment)
        {
            this.id = Guid.NewGuid();
            this.firstName = firstName;
            this.lastName = lastName;
            this.sid = sid;
            this.section = section;
            this.leftHanded = leftHanded;
            this.visionImpairment = visionEnpairment;
            this.isEnrolled = true;
        }

        internal Student()
        {
            this.id = Guid.NewGuid();
            this.firstName = "Default";
            this.lastName = "Default";
            this.sid = "Default";
            this.section = "Default";
            this.leftHanded = false;
            this.visionImpairment = false;
            this.isEnrolled = false;
        }

        internal Student(Guid id, string firstName, string lastName, string sid, string section, bool leftHanded, bool visionEnpairment)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.sid = sid;
            this.section = section;
            this.leftHanded = leftHanded;
            this.visionImpairment = visionEnpairment;
            this.isEnrolled = true;
        }

        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        // Properties
        public Guid Uid
        {
            get { return this.id; }
        }

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

        public bool IsEnrolled
        {
            get
            {
                return this.isEnrolled;
            }

            set
            {
                if (value != this.isEnrolled)
                {
                    this.isEnrolled = value;
                    this.NotifyPropertyChanged("IsEnrolled");
                }
            }
        }

        // Methods
        public override string ToString()
        {
            return this.lastName + ", " + this.firstName;
        }

        private void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
