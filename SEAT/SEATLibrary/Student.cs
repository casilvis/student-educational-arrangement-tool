using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEATLibrary
{
    public class Student
    {
        //private int primaryId;
        private String firstName;
        private String lastName;
        private String section;
        private Boolean leftHanded;
        private Boolean visionEnpairment;
        private Boolean isEnrolled;

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

        public Student()
        {
            firstName = "";
            lastName = "";
            section = "";
            leftHanded = false;
            visionEnpairment = false;
            isEnrolled = false;
        }
    }
}
