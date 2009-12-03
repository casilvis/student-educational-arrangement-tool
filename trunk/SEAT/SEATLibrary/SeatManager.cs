// <copyright file="SeatManager.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEATLibrary
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Manager for the program that coordinates the roster of students and all of the rooms.
    /// </summary>
    public class SeatManager
    {
        // Attributes 

        /// <summary>
        /// All of the students.
        /// </summary>
        private ObservableCollection<Student> students;

        /// <summary>
        /// All of the rooms.
        /// </summary>
        private ObservableCollection<Room> rooms;

        /// <summary>
        /// The location of the file for saving.
        /// </summary>
        private string file;

        // Constructors

        /// <summary>
        /// Initializes a new instance of the SeatManager class without first saving it to disk.
        /// </summary>
        public SeatManager()
        {
            this.students = new ObservableCollection<Student>();
            this.rooms = new ObservableCollection<Room>();
            this.file = null;
        }

        /// <summary>
        /// Initializes a new instance of the SeatManager class by reading a saved file from the disk.
        /// </summary>
        /// <param name="file">Location of file to load.</param>
        public SeatManager(string file)
        {
            // Initialize the variables
            this.students = new ObservableCollection<Student>();
            this.rooms = new ObservableCollection<Room>();
            this.file = file;

            // Read in the XML document and load all of the data into memory
            XmlReader r = new XmlTextReader(file);
            while (r.Read())
            {
                if (r.NodeType == XmlNodeType.Element)
                {
                    // Read in all of the students
                    if (r.Name.ToString() == "Students")
                    {
                        bool test1 = true;
                        while (!(r.NodeType == XmlNodeType.EndElement && r.Name == "Students") && test1)
                        {
                            test1 = r.Read();

                            // Add all of the students to the array
                            if (r.Name == "Student")
                            {
                                Student s = new Student(
                                    new Guid(r.GetAttribute("Uid")),
                                    r.GetAttribute("First"),
                                    r.GetAttribute("Last"),
                                    r.GetAttribute("Sid"),
                                    r.GetAttribute("Section"),
                                    bool.Parse(r.GetAttribute("LeftHanded")),
                                    bool.Parse(r.GetAttribute("VisionImpairment")));
                                this.students.Add(s);
                            }
                        }
                    }
                    else if (r.Name.ToString() == "Rooms" && r.NodeType == XmlNodeType.Element && !r.IsEmptyElement)
                    {
                        // Read in all of the rooms
                        bool test2 = true;
                        while (!(r.NodeType == XmlNodeType.EndElement && r.Name.ToString() == "Rooms") && test2)
                        {
                            test2 = r.Read();

                            // Read in a single room
                            if (r.NodeType == XmlNodeType.Element && r.Name.ToString() == "Room")
                            {
                                // Read the room's attributes and make a new instance of a room
                                Room room = new Room(
                                    r.GetAttribute("Name"),
                                    r.GetAttribute("Location"),
                                    r.GetAttribute("Description"),
                                    Int32.Parse(r.GetAttribute("Width")),
                                    Int32.Parse(r.GetAttribute("Height")));

                                // Add the room to the list of the rooms
                                this.rooms.Add(room);
                                
                                // Get all of the information contained in a room
                                bool test3 = true;
                                while (!(r.NodeType == XmlNodeType.EndElement && r.Name == "Room") && test3)
                                {
                                    test3 = r.Read();

                                    // Read in all of the chairs
                                    if (r.NodeType == XmlNodeType.Element && r.Name == "Chairs")
                                    {
                                        bool test4 = true;
                                        while (!(r.NodeType == XmlNodeType.EndElement && r.Name == "Chairs") && test4)
                                        {
                                            test4 = r.Read();

                                            // Read in the information about a chair
                                            if (r.NodeType == XmlNodeType.Element && r.Name == "Chair")
                                            {
                                                // Get the position of the chair in the room
                                                int x = Int32.Parse(r.GetAttribute("PosX"));
                                                int y = Int32.Parse(r.GetAttribute("PosY"));

                                                // For the student, we are passing by reference so everything stays in sync
                                                Student s = this.LookupStudent(r.GetAttribute("SUID"));

                                                // Replace the chair from the default constructor with the information contained in the stored version of the chair
                                                room.Chairs[x, y] = new Chair(
                                                    bool.Parse(r.GetAttribute("LeftHanded")),
                                                    Int32.Parse(r.GetAttribute("FbPosition")),
                                                    Int32.Parse(r.GetAttribute("LrPosition")),
                                                    bool.Parse(r.GetAttribute("NonChair")),
                                                    bool.Parse(r.GetAttribute("MustBeEmpty")),
                                                    r.GetAttribute("Name"),
                                                    s);
                                            }
                                        }
                                    }
                                    else if (r.NodeType == XmlNodeType.Element && r.Name == "RoomStudents")
                                    {
                                        // Read in all of the RoomStudents
                                        bool test5 = true;
                                        while (!(r.NodeType == XmlNodeType.EndElement && r.Name == "RoomStudents") && test5)
                                        {
                                            test5 = r.Read();

                                            // Read in the information about a student
                                            if (r.NodeType == XmlNodeType.Element && r.Name == "RoomStudent")
                                            {
                                                // The student's identifier
                                                Student s = this.LookupStudent(r.GetAttribute("SUID"));

                                                // If the ID is non-null then we can add this student to the list of room students
                                                if (this.students != null)
                                                {
                                                    // For the student, we are passing by reference so everything stays in sync
                                                    room.RoomStudents.Add(s);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Properties 

        /// <summary>
        /// Gets the list of rooms.
        /// </summary>
        /// <value>Collection of rooms.</value>
        public ObservableCollection<Room> RoomList
        {
            get { return this.rooms; }
        }

        /// <summary>
        /// Gets the list of students.
        /// </summary>
        /// <value>Collection of students.</value>
        public ObservableCollection<Student> StudentList
        {
            get { return this.students; }
        }

        // Methods 

        /// <summary>
        /// Adds a new student to the roster of students.
        /// </summary>
        /// <param name="student">An instance of the student to add to the roster.</param>
        public void AddStudentToRoster(Student student)
        {
            this.students.Add(student);
        }

        /// <summary>
        /// Adds a new room to the list of rooms given the rooms dimensions.
        /// </summary>
        /// <param name="width">Width of room.</param>
        /// <param name="height">Height of room.</param>
        /// <returns>The instance of the room that was added.</returns>
        public Room AddNewRoom(int width, int height)
        {
            Room r = new Room(width, height);
            this.rooms.Add(r);
            return r;
        }

        /// <summary>
        /// Adds a new room to the list of rooms.
        /// </summary>
        /// <param name="room">The instance of the room to add.</param>
        public void AddNewRoom(Room room)
        {
            this.rooms.Add(room);
        }

        /// <summary>
        /// Adds the given student to the given room and performs bounds checking.
        /// </summary>
        /// <param name="student">The instnace of the student to add to the room.</param>
        /// <param name="room">The instance of the room to add the student to.</param>
        /// <returns>True if the student was able to be added to the room.</returns>
        public bool AddStudentToRoom(int student, int room)
        {
            // THIS IS NOT IMPLEMENTED
            throw new NotImplementedException("AddStudentToRoom");
        }

        /// <summary>
        /// Saves the state of the application to the file specified inside of this manager.
        /// </summary>
        /// <returns>True if the file was able to be saved.</returns>
        public bool SaveXml()
        {
            if (this.file != null)
            {
                this.SaveXml(this.file);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Saves the sate of this manager to a file.
        /// </summary>
        /// <param name="file">The location to save the file to.</param>
        public void SaveXml(string file)
        {
            this.file = file;
            XmlWriter w = new XmlTextWriter(file, null);
            w.WriteStartDocument();
            w.WriteStartElement("SEAT"); // START SEAT
            w.WriteStartElement("Students"); // START STUDENTS
            for (int i = 0; i < this.students.Count; i++)
            {
                w.WriteStartElement("Student"); // START STUDENT
                w.WriteAttributeString("Uid", this.students[i].Uid.ToString());
                w.WriteAttributeString("First", this.students[i].FirstName);
                w.WriteAttributeString("Last", this.students[i].LastName);
                w.WriteAttributeString("Section", this.students[i].Section);
                w.WriteAttributeString("Sid", this.students[i].Sid);
                w.WriteAttributeString("LeftHanded", this.students[i].LeftHanded.ToString());
                w.WriteAttributeString("VisionImpairment", this.students[i].VisionImpairment.ToString());
                w.WriteEndElement(); // END STUDENT
            }

            w.WriteEndElement(); // END STUDENTS
            w.WriteStartElement("Rooms"); // START ROOMS
            for (int i = 0; i < this.rooms.Count; i++)
            {
                w.WriteStartElement("Room"); // START ROOM
                w.WriteAttributeString("Name", this.rooms[i].RoomName);
                w.WriteAttributeString("Location", this.rooms[i].Location);
                w.WriteAttributeString("Description", this.rooms[i].Description);
                w.WriteAttributeString("Width", this.rooms[i].Width.ToString());
                w.WriteAttributeString("Height", this.rooms[i].Height.ToString());
                w.WriteStartElement("Chairs"); // START CHAIRS
                for (int j = 0; j < this.rooms[i].Width; j++)
                {
                    for (int k = 0; k < this.rooms[i].Height; k++)
                    {
                        Chair c = this.rooms[i].Chairs[j, k];
                        w.WriteStartElement("Chair"); // START CHAIR
                        w.WriteAttributeString("PosX", j.ToString());
                        w.WriteAttributeString("PosY", k.ToString());
                        w.WriteAttributeString("LeftHanded", c.LeftHanded.ToString());
                        w.WriteAttributeString("FbPosition", c.FbPosition.ToString());
                        w.WriteAttributeString("LrPosition", c.LrPosition.ToString());
                        w.WriteAttributeString("NonChair", c.NonChair.ToString());
                        w.WriteAttributeString("MustBeEmpty", c.MustBeEmpty.ToString());
                        w.WriteAttributeString("Name", c.SeatName);
                        if (c.TheStudent == null)
                        {
                            w.WriteAttributeString("SUID", new Guid().ToString());
                        }
                        else
                        {
                            w.WriteAttributeString("SUID", c.TheStudent.Uid.ToString());
                        }

                        w.WriteEndElement(); // END CHAIR
                    }
                }

                w.WriteEndElement(); // END CHAIRS
                w.WriteStartElement("RoomStudents"); // START ROOMSTUDENTS
                for (int j = 0; j < this.rooms[i].RoomStudents.Count; j++)
                {
                    w.WriteStartElement("RoomStudent"); // START ROOMSTUDENT
                    w.WriteAttributeString("SUID", this.rooms[i].RoomStudents[j].Uid.ToString());
                    w.WriteEndElement(); // END ROOMSTUDENT
                }

                w.WriteEndElement(); // END ROOMSTUDENTS
                w.WriteEndElement(); // END ROOM
            }

            w.WriteEndElement(); // END ROOMS
            w.WriteEndElement(); // END SEAT
            w.WriteEndDocument();
            w.Close();
        }

        /// <summary>
        /// Gets an instance of a student given a string representation of a Guid.
        /// </summary>
        /// <param name="id">The Guid of the student to locatate.</param>
        /// <returns>The instance of the student requested.</returns>
        private Student LookupStudent(string id)
        {
            return this.LookupStudent(new Guid(id));
        }

        /// <summary>
        /// Gets an instance of a student given a Guid.
        /// </summary>
        /// <param name="id">The Guid of the student to locate.</param>
        /// <returns>The instance of the student requested.</returns>
        private Student LookupStudent(Guid id)
        {
            for (int i = 0; i < this.students.Count; i++)
            {
                if (this.students[i].Uid == id)
                {
                    return this.students[i];
                }
            }

            return null;
        }
    }
}
