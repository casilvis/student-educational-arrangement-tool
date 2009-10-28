using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace SEATLibrary
{
    public class SeatManager
    {
        // Attributes
        private ObservableCollection<Student> students;
        private ObservableCollection<Room> rooms;

        // Properties
        public ObservableCollection<Room> RoomList
        {
            get { return rooms; }
        }
        public ObservableCollection<Student> StudentList
        {
            get { return students; }
        }

        // Constructors
        public SeatManager()
        {
            students = new ObservableCollection<Student>();
            rooms = new ObservableCollection<Room>();
        }

        public SeatManager(String file)
        {
            // Initialize the variables
            students = new ObservableCollection<Student>();
            rooms = new ObservableCollection<Room>();
            // Read in the XML document and load all of the data into memory
            XmlReader r = new XmlTextReader(file);
            while (r.Read())
            {
                if (r.NodeType == XmlNodeType.Element)
                {
                    // Read in all of the students
                    if (r.Name.ToString() == "Students")
                    {
                        while (!(r.NodeType == XmlNodeType.EndElement && r.Name == "Students"))
                        {
                            r.Read();
                            // Add all of the students to the array
                            if (r.Name == "Student")
                            {
                                Student s = new Student(new Guid(r.GetAttribute("Uid")), r.GetAttribute("First"),
                                    r.GetAttribute("Last"), r.GetAttribute("Sid"),
                                    r.GetAttribute("Section"), Boolean.Parse(r.GetAttribute("LeftHanded")),
                                    Boolean.Parse(r.GetAttribute("VisionImpairment")));
                                students.Add(s);
                            }
                            
                        }
                    }
                    // Read in all of the rooms
                    else if (r.Name.ToString() == "Rooms" && r.NodeType == XmlNodeType.Element && !r.IsEmptyElement)
                    {
                        while (!(r.NodeType == XmlNodeType.EndElement && r.Name.ToString() == "Rooms"))
                        {
                            r.Read();
                            // Read in a single room
                            if (r.NodeType == XmlNodeType.Element && r.Name.ToString() == "Room")
                            {
                                //Read the room's attributes and make a new instance of a room
                                Room room = new Room(r.GetAttribute("Name"), r.GetAttribute("Location"),
                                    r.GetAttribute("Description"), Int32.Parse(r.GetAttribute("Width")),
                                    Int32.Parse(r.GetAttribute("Height")));
                                //Add the room to the list of the rooms
                                rooms.Add(room);
                                //Get all of the information contained in a room
                                while (!(r.NodeType == XmlNodeType.EndElement && r.Name == "Room"))
                                {
                                    r.Read();
                                    //Read in all of the chairs
                                    if (r.NodeType == XmlNodeType.Element && r.Name == "Chairs")
                                    {
                                        while (!(r.NodeType == XmlNodeType.EndElement && r.Name == "Chairs"))
                                        {
                                            r.Read();
                                            //Read in the information about a chair
                                            if (r.NodeType == XmlNodeType.Element && r.Name == "Chair")
                                            {
                                                //Get the position of the chair in the room
                                                int x = Int32.Parse(r.GetAttribute("PosX"));
                                                int y = Int32.Parse(r.GetAttribute("PosY"));
                                                //For the student, we are passing by reference so everything stays in sync
                                                Student s = lookupStudent(r.GetAttribute("SUID"));
                                                //Replace the chair from the default constructor with the information contained in the stored version of the chair
                                                room.Chairs[x, y] = new Chair(Boolean.Parse(r.GetAttribute("LeftHanded")),
                                                    Int32.Parse(r.GetAttribute("FbPosition")), Int32.Parse(r.GetAttribute("LrPosition")),
                                                    Boolean.Parse(r.GetAttribute("NonChair")), Boolean.Parse(r.GetAttribute("MustBeEmpty")),
                                                    r.GetAttribute("Name"), s);
                                            }
                                        }
                                    }
                                    //Read in all of the RoomStudents
                                    else if (r.NodeType == XmlNodeType.Element && r.Name == "RoomStudents")
                                    {
                                        while (!(r.NodeType == XmlNodeType.Element && r.Name == "RoomStudents"))
                                        {
                                            r.Read();
                                            //Read in the information about a student
                                            if (r.NodeType == XmlNodeType.Element && r.Name == "RoomStudent")
                                            {
                                                //The student's identifier
                                                Student s = lookupStudent(r.GetAttribute("SUID"));
                                                //If the ID is non-null then we can add this student to the list of room students
                                                if (students != null)
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


        // Methods
        public void AddStudentToRoster(Student student)
        {
            students.Add(student);
        }

        public Room addNewRoom(int width, int height)
        {
            Room r = new Room(width, height);
            rooms.Add(r);
            return r;
        }

        public void addNewRoom(Room room)
        {
            rooms.Add(room);
        }

        public Boolean addStudentToRoom(int student, int room)
        {
            return false;
        }

        private Student lookupStudent(String id)
        {
            return lookupStudent(new Guid(id));
        }

        private Student lookupStudent(Guid id)
        {
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].Uid == id)
                {
                    return students[i];
                }
            }
            return null;
        }

        

        public void saveXml(String file)
        {
            XmlWriter w = new XmlTextWriter(file, null);
            w.WriteStartDocument();
            w.WriteStartElement("SEAT"); // START SEAT
            w.WriteStartElement("Students"); // START STUDENTS
            for (int i = 0; i < students.Count; i++)
            {
                w.WriteStartElement("Student"); // START STUDENT
                w.WriteAttributeString("Uid", students[i].Uid.ToString());
                w.WriteAttributeString("First", students[i].FirstName);
                w.WriteAttributeString("Last", students[i].LastName);
                w.WriteAttributeString("Section", students[i].Section);
                w.WriteAttributeString("Sid", students[i].Sid);
                w.WriteAttributeString("LeftHanded", students[i].LeftHanded.ToString());
                w.WriteAttributeString("VisionImpairment", students[i].VisionImpairment.ToString());
                w.WriteEndElement(); // END STUDENT
            }
            w.WriteEndElement(); // END STUDENTS
            w.WriteStartElement("Rooms"); // START ROOMS
            for (int i = 0; i < rooms.Count; i++)
            {
                w.WriteStartElement("Room"); // START ROOM
                w.WriteAttributeString("Name", rooms[i].RoomName);
                w.WriteAttributeString("Location", rooms[i].Location);
                w.WriteAttributeString("Description", rooms[i].Description);
                w.WriteAttributeString("Width", rooms[i].Width.ToString());
                w.WriteAttributeString("Height", rooms[i].Height.ToString());
                w.WriteStartElement("Chairs"); // START CHAIRS
                for (int j = 0; j < rooms[i].Width; j++)
                {
                    for (int k = 0; k < rooms[i].Height; k++)
                    {
                        Chair c = rooms[i].Chairs[k,j];
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
                        w.WriteEndElement();//END CHAIR
                    }
                }
                w.WriteEndElement(); // END CHAIRS
                w.WriteStartElement("RoomStudents"); // START ROOMSTUDENTS
                for (int j = 0; j < rooms[i].RoomStudents.Count; j++)
                {
                    w.WriteStartElement("RoomStudent"); // START ROOMSTUDENT
                    w.WriteAttributeString("SUID", rooms[i].RoomStudents[j].Uid.ToString());
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


    }
}
