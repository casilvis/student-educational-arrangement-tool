// <copyright file="Student.cs" company="University of Louisville Speed School of Engineering">
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
    /// 
    /// </summary>
    public class Student : INotifyPropertyChanged
    {
        // Attributes

        /// <summary>
        /// 
        /// </summary>
        private Guid id;

        /// <summary>
        /// 
        /// </summary>
        private string firstName;

        /// <summary>
        /// 
        /// </summary>
        private string lastName;

        /// <summary>
        /// 
        /// </summary>
        private string sid;

        /// <summary>
        /// 
        /// </summary>
        private string section;

        /// <summary>
        /// 
        /// </summary>
        private bool leftHanded;

        /// <summary>
        /// 
        /// </summary>
        private bool visionImpairment;

        /// <summary>
        /// 
        /// </summary>
        private bool isEnrolled;

        // Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="sid"></param>
        /// <param name="section"></param>
        /// <param name="leftHanded"></param>
        /// <param name="visionEnpairment"></param>
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

        /// <summary>
        /// 
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
            this.isEnrolled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="sid"></param>
        /// <param name="section"></param>
        /// <param name="leftHanded"></param>
        /// <param name="visionEnpairment"></param>
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

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        // Properties

        /// <summary>
        /// 
        /// </summary>
        public Guid Uid
        {
            get { return this.id; }
        }

        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
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
        /// 
        /// </summary>
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
        /// 
        /// </summary>
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
        /// 
        /// </summary>
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
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.lastName + ", " + this.firstName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        private void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
