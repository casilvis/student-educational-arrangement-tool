// <copyright file="SeatManager.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEATLibrary
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Manager for the program that coordinates the roster of students and all of the rooms.
    /// </summary>
    public class SeatManager
    {
        // Attributes 

        /// <summary>
        /// Indicates that some piece of data has changed and the file now needs to be saved.
        /// </summary>
        private static bool dirty;

        /// <summary>
        /// All of the students.
        /// </summary>
        private ObservableCollection<Student> students;

        /// <summary>
        /// A virtual collection of the sections.
        /// </summary>
        private ObservableCollection<string> sections;

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
            this.students.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(this.Students_CollectionChanged);
            this.sections = new ObservableCollection<string>();
            this.sections.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(this.Sections_CollectionChanged);
            this.rooms = new ObservableCollection<Room>();
            this.rooms.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(this.Rooms_CollectionChanged);
            this.file = null;
            SeatManager.dirty = false;
        }

        /// <summary>
        /// Initializes a new instance of the SeatManager class by reading a saved file from the disk.
        /// </summary>
        /// <param name="file">Location of file to load.</param>
        public SeatManager(string file)
        {
            // Initialize the variables
            this.students = new ObservableCollection<Student>();
            this.students.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(this.Students_CollectionChanged);
            this.sections = new ObservableCollection<string>();
            this.sections.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(this.Sections_CollectionChanged);
            this.rooms = new ObservableCollection<Room>();
            this.rooms.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(this.Rooms_CollectionChanged);
            this.file = file;

            // Read in the XML document and load all of the data into memory
            FileStream filestream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            GZipStream gzipstream = new GZipStream(filestream, CompressionMode.Decompress, true);
            XmlReader r = new XmlTextReader(gzipstream);
            while (r.Read())
            {
                if (r.NodeType == XmlNodeType.Element)
                {
                    // Read in all of the students
                    if (r.Name.ToString() == "Students" && !r.IsEmptyElement)
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
                                    Int32.Parse(r.GetAttribute("Height")),
                                    Int32.Parse(r.GetAttribute("Width")));
                                Trace.WriteLine("PARSER: Adding Room - " + room.ToString()); // DEBUG

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
                                    else if (r.NodeType == XmlNodeType.Element && r.Name == "RoomStudents" && !r.IsEmptyElement)
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

            r.Close();
            gzipstream.Close();
            filestream.Close();
            SeatManager.dirty = false;
        }

        // Events

        /// <summary>
        /// The file has become dirty and those who subscribe to this even need to be notified.
        /// </summary>
        public static event EventHandler FileBecameDirty;

        // Properties 

        /// <summary>
        /// Gets a value indicating whether the data is in need of saving.
        /// </summary>
        /// <value>Is the data dirty.</value>
        public static bool Dirty
        {
            get { return dirty; }
        }

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

        /// <summary>
        /// Gets the list of sections.
        /// </summary>
        /// <value>Read only collection of sections.</value>
        public ReadOnlyObservableCollection<string> SectionList
        {
            get { return new ReadOnlyObservableCollection<string>(this.sections); }
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
            FileStream filestream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write, 1000, true);
            GZipStream gzipstream = new GZipStream(filestream, CompressionMode.Compress);
            XmlWriter w = new XmlTextWriter(gzipstream, null);
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
                for (int j = 0; j < this.rooms[i].Height; j++)
                {
                    for (int k = 0; k < this.rooms[i].Width; k++)
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
            gzipstream.Close();
            filestream.Close();

            // Mark the file as no longer dirty
            SeatManager.dirty = false;
            SeatManager.FileBecameDirty.Invoke(null, null);
        }

        /// <summary>
        /// Returns a string representation of the SEATManager used in the title bar of the application.
        /// </summary>
        /// <returns>String representation of the current file.</returns>
        public override string ToString()
        {
            string title = "SEAT | " + this.file;
            if (SeatManager.Dirty)
            {
                title += " *";
            }

            return title;
        }

        /// <summary>
        /// Can be used anywhere to indicate the a change has been made to the file.
        /// </summary>
        internal static void MarkDirty()
        {
            if (SeatManager.dirty == false)
            {
                SeatManager.dirty = true;
                SeatManager.FileBecameDirty.Invoke(null, null);
            }
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

        /// <summary>
        /// Something in the list of rooms changed and the file needs to be saved.
        /// </summary>
        /// <param name="sender">The sender who triggered this event.</param>
        /// <param name="e">The information about this event.</param>
        private void Rooms_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SeatManager.MarkDirty();
        }

        /// <summary>
        /// Something in the list of students changed and the file needs to be saved.
        /// </summary>
        /// <param name="sender">The sender who triggered this event.</param>
        /// <param name="e">The information about this event.</param>
        private void Students_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SeatManager.MarkDirty();
            
            // Keep the section array up to date
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (Student item in e.NewItems)
                {
                    // Add event handlers to all of the new students
                    item.PropertyChanged += new PropertyChangedEventHandler(this._Student_PropertyChanged);
                }

                this.RefreshSectionList();
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                this.RefreshSectionList();
            }
        }

        /// <summary>
        /// Run when one of the studen't properties changes.  Used to keep the section list up to date.
        /// </summary>
        /// <param name="sender">Who triggered this event.</param>
        /// <param name="e">The parameters for this event.</param>
        private void _Student_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Section")
            {
                this.RefreshSectionList();
            }
        }

        /// <summary>
        /// Bring the list of sections up to date.
        /// Not a efficient function, but it is efficient enough for this purpose.
        /// </summary>
        private void RefreshSectionList()
        {
            // Get the new list of sections
            // The complexity grows linearly as the number of students increases
            Collection<string> list = new Collection<string>();
            for (int i = 0; i < this.students.Count; i++)
            {
                if (!list.Contains(this.students[i].Section))
                {
                    list.Add(this.students[i].Section);
                }
            }

            // Add the new section
            for (int i = 0; i < list.Count; i++)
            {
                if (!this.sections.Contains(list[i]))
                {
                    this.sections.Add(list[i]);
                }
            }

            // Remove the deleted sections
            for (int i = 0; i < this.sections.Count; i++)
            {
                if (!list.Contains(this.sections[i]))
                {
                    this.sections.RemoveAt(i--);
                }
            }
        }

        /// <summary>
        /// Executed when the collection of sections change.
        /// </summary>
        /// <param name="sender">Who triggered this action.</param>
        /// <param name="e">The parameters for this method.</param>
        private void Sections_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // throw new NotImplementedException();
        }
    }
}
