namespace SEATLibrary 
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    
    public class Room : INotifyPropertyChanged
    {
        // Attributes
        private string roomName;
        private string location;
        private string description;
        private ObservableCollection<Student> roomStudents;
        private Chair[,] chairs;
        private int width;
        private int height;

        // Constructors
        public Room()
        {
            this.width = 4;
            this.height = 4;

            this.roomName = "Unknown";
            this.location = "Unknown";
            this.description = "Unknown";
            this.roomStudents = new ObservableCollection<Student>();
            this.chairs = new Chair[this.height, this.width];

            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    this.chairs[i, j] = new Chair();
                }
            }
        }

        public Room(int height, int width)
        {
            this.width = width;
            this.height = height;

            this.roomName = "Unknown";
            this.location = "Unknown";
            this.description = "Unknown";
            this.roomStudents = new ObservableCollection<Student>();
            this.chairs = new Chair[this.height, this.width];

            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    this.chairs[i, j] = new Chair();
                }
            }
        }

        public Room(string roomName, string location, string description, int height, int width)
        {
            this.width = width;
            this.height = height;

            this.roomName = roomName;
            this.location = location;
            this.description = description;
            this.roomStudents = new ObservableCollection<Student>();
            this.chairs = new Chair[this.height, this.width];

            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    this.chairs[i, j] = new Chair();
                }
            }
        }

        public Room(string file)
        {
            this.roomStudents = new ObservableCollection<Student>();

            // Read in the XML document and load all of the data into memory
            XmlReader r = new XmlTextReader(file);
            while (r.Read())
            {
                if (r.NodeType == XmlNodeType.Element && r.Name == "Room")
                {
                    // Read the room's attributes and make a new instance of a room
                    this.width = Int32.Parse(r.GetAttribute("Width"));
                    this.height = Int32.Parse(r.GetAttribute("Height"));
                    this.roomName = r.GetAttribute("Name");
                    this.location = r.GetAttribute("Location");
                    this.description = r.GetAttribute("Description");
                    this.chairs = new Chair[this.height, this.width];

                    // Get all of the information contained in a room
                    while (!(r.NodeType == XmlNodeType.EndElement && r.Name == "Room"))
                    {
                        r.Read();

                        // Read in all of the chairs
                        if (r.NodeType == XmlNodeType.Element && r.Name == "Chairs")
                        {
                            while (!(r.NodeType == XmlNodeType.EndElement && r.Name == "Chairs"))
                            {
                                r.Read();

                                // Read in the information about a chair
                                if (r.NodeType == XmlNodeType.Element && r.Name == "Chair")
                                {
                                    // Get the position of the chair in the room
                                    int x = Int32.Parse(r.GetAttribute("PosX"));
                                    int y = Int32.Parse(r.GetAttribute("PosY"));

                                    // For the student, we assume the chair is blank.
                                    Student s = null;
                                    //Replace the chair from the default constructor with the information contained in the stored version of the chair
                                    Chairs[y,x] = new Chair(Boolean.Parse(r.GetAttribute("LeftHanded")),
                                        Int32.Parse(r.GetAttribute("FbPosition")), Int32.Parse(r.GetAttribute("LrPosition")),
                                        Boolean.Parse(r.GetAttribute("NonChair")), Boolean.Parse(r.GetAttribute("MustBeEmpty")),
                                        r.GetAttribute("Name"), s);
                                }
                            }
                        }
                    }
                }
            }
        }

        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        // Properties
        public string RoomName
        {
            get
            {
                return this.roomName;
            }

            set
            {
                if (value != this.roomName)
                {
                    this.roomName = value;
                    this.NotifyPropertyChanged("RoomName");
                }
            }
        }

        public string Location
        {
            get
            {
                return this.location;
            }

            set
            {
                if (value != this.location)
                {
                    this.location = value;
                    this.NotifyPropertyChanged("Location");
                }
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                if (value != this.description)
                {
                    this.description = value;
                    this.NotifyPropertyChanged("Description");
                }
            }
        }

        public Chair[,] Chairs
        {
            get { return this.chairs; }
        }

        public int Width
        {
            get { return this.width; }
        }

        public int Height
        {
            get { return this.height; }
        }

        public ObservableCollection<Student> RoomStudents
        {
            get { return this.roomStudents; }
        }

        // Methods
        public void AddStudent(Student student)
        {
            if (!roomStudents.Contains(student))
            {
                roomStudents.Add(student);
            }
        }

        public void RunPlacementAlgorithmx(AssignmentVisitor algorithm)
        {
            algorithm.PlaceStudents(this);
        }

        public bool IsRoomEmpty()
        {
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    if (!this.chairs[i, j].IsEmpty())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void WriteRoomTemplate(string file)
        {
            XmlWriter w = new XmlTextWriter(file, null);
            w.WriteStartDocument();
            w.WriteStartElement("SEATTEMPLATE"); // START SEATTEMPLATE

            w.WriteStartElement("Room"); // START ROOM
            w.WriteAttributeString("Name", this.RoomName);
            w.WriteAttributeString("Location", this.Location);
            w.WriteAttributeString("Description", this.Description);
            w.WriteAttributeString("Width", this.Width.ToString());
            w.WriteAttributeString("Height", this.Height.ToString());
            w.WriteStartElement("Chairs"); // START CHAIRS
            for (int j = 0; j < this.Height; j++)
            {
                for (int k = 0; k < this.Width; k++)
                {
                    Chair c = this.Chairs[j, k];
                    w.WriteStartElement("Chair"); // START CHAIR
                    w.WriteAttributeString("PosX", j.ToString());
                    w.WriteAttributeString("PosY", k.ToString());
                    w.WriteAttributeString("LeftHanded", c.LeftHanded.ToString());
                    w.WriteAttributeString("FbPosition", c.FbPosition.ToString());
                    w.WriteAttributeString("LrPosition", c.LrPosition.ToString());
                    w.WriteAttributeString("NonChair", c.NonChair.ToString());
                    w.WriteAttributeString("MustBeEmpty", c.MustBeEmpty.ToString());
                    w.WriteAttributeString("Name", c.SeatName);
                    w.WriteAttributeString("SUID", new Guid().ToString());
                    w.WriteEndElement(); // END CHAIR
                }
            }

            w.WriteEndElement(); // END CHAIRS
            w.WriteEndElement(); // END ROOM
            w.WriteEndElement(); // END SEATTEMPLATE
            w.WriteEndDocument();
            w.Close();
        }

        public override string ToString()
        {
            return this.RoomName + " - " + this.Location;
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

