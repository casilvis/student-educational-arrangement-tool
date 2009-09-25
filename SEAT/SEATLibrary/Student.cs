using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEATLibrary
{
    public class Student
    {
        // Attributes
        private String firstName;
        private String lastName;
        private String section;
        private Boolean leftHanded;
        private Boolean visionEnpairment;
        private Boolean isEnrolled;

        // Properties
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
        public Boolean LeftHanded
        {
            get { return leftHanded; }
            set { leftHanded = value; }
        }
        public Boolean VisionEnpairment
        {
            get { return visionEnpairment; }
            set { visionEnpairment = value; }
        }
        public Boolean IsEnrolled
        {
            get { return isEnrolled; }
            set { isEnrolled = value; }
        }

        // Constructor
        public Student()
        {
            firstName = "Default";
            lastName = "Default";
            section = "Default";
            leftHanded = false;
            visionEnpairment = false;
            isEnrolled = false;
        }

        public Student(String firstName, String lastName, String section, Boolean leftHanded, Boolean visionEnpairment)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.section = section;
            this.leftHanded = leftHanded;
            this.visionEnpairment = visionEnpairment;
            isEnrolled = true;
        }

        // Methods
        public override string ToString()
        {
            return lastName + ", " + firstName;
        }
    }
}
