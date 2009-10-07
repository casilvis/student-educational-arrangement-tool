using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEATLibrary
{
    public class Student
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

        // Properties
        public Guid Uid
        {
            get { return id; }
        }
        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public String Section
        {
            get { return section; }
            set { section = value; }
        }
        public String Sid
        {
            get { return sid; }
            set { sid = value; }
        }
        public Boolean LeftHanded
        {
            get { return leftHanded; }
            set { leftHanded = value; }
        }
        public Boolean VisionImpairment
        {
            get { return visionImpairment; }
            set { visionImpairment = value; }
        }
        public Boolean IsEnrolled
        {
            get { return isEnrolled; }
            set { isEnrolled = value; }
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
