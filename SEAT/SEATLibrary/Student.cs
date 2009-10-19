using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SEATLibrary
{
    public class Student : INotifyPropertyChanged
    {
        // Attributes
        private Guid id;
        private String firstName;
        private String lastName;
        private String sid;
        private String section;
        private Boolean leftHanded;
        private Boolean visionImpairment;
        private Boolean isEnrolled;


        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }


        // Properties
        public Guid Uid
        {
            get { return id; }
        }
        public String FirstName
        {
            get { return firstName; }
            set
            {
                if (value != this.firstName)
                {
                    firstName = value;
                    NotifyPropertyChanged("FirstName");
                }
            }
        }
        public String LastName
        {
            get { return lastName; }
            set
            {
                if (value != this.lastName)
                {
                    lastName = value;
                    NotifyPropertyChanged("LastName");
                }
            }
        }
        public String Section
        {
            get { return section; }
            set
            {
                if (value != this.section)
                {
                    section = value;
                    NotifyPropertyChanged("Section");
                }
            }
        }
        public String Sid
        {
            get { return sid; }
            set
            {
                if (value != this.sid)
                {
                    sid = value;
                    NotifyPropertyChanged("Sid");
                }
            }
        }
        public Boolean LeftHanded
        {
            get { return leftHanded; }
            set
            {
                if (value != this.leftHanded)
                {
                    leftHanded = value;
                    NotifyPropertyChanged("LeftHanded");
                }
            }
        }
        public Boolean VisionImpairment
        {
            get { return visionImpairment; }
            set
            {
                if (value != this.visionImpairment)
                {
                    visionImpairment = value;
                    NotifyPropertyChanged("VisionImpairment");
                }
            }
        }
        public Boolean IsEnrolled
        {
            get { return isEnrolled; }
            set
            {
                if (value != this.isEnrolled)
                {
                    isEnrolled = value;
                    NotifyPropertyChanged("IsEnrolled");
                }

            }
        }

        // Constructor
        internal Student()
        {
            id = Guid.NewGuid();
            firstName = "Default";
            lastName = "Default";
            sid = "Default";
            section = "Default";
            leftHanded = false;
            visionImpairment = false;
            isEnrolled = false;
        }

        internal Student(Guid id,String firstName, String lastName, String sid, String section, Boolean leftHanded, Boolean visionEnpairment)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.sid = sid;
            this.section = section;
            this.leftHanded = leftHanded;
            this.visionImpairment = visionEnpairment;
            isEnrolled = true;
        }

        public Student(String firstName, String lastName, String sid, String section, Boolean leftHanded, Boolean visionEnpairment)
        {
            this.id = Guid.NewGuid();
            this.firstName = firstName;
            this.lastName = lastName;
            this.sid = sid;
            this.section = section;
            this.leftHanded = leftHanded;
            this.visionImpairment = visionEnpairment;
            isEnrolled = true;
        }

        // Methods
        public override string ToString()
        {
            return lastName + ", " + firstName;
        }
    }
}
