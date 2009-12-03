// <copyright file="Room.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEATLibrary 
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    
    /// <summary>
    /// A model representing a rectangular room consiting of chair and a list of students.
    /// </summary>
    public class Room : INotifyPropertyChanged
    {
        // Attributes

        /// <summary>
        ///  The name of this room.
        /// </summary>
        private string roomName;

        /// <summary>
        /// The location of this room.
        /// </summary>
        private string location;

        /// <summary>
        /// The description of this room.
        /// </summary>
        private string description;

        /// <summary>
        /// The list of student who should be sitting in this room.
        /// </summary>
        private ObservableCollection<Student> roomStudents;

        /// <summary>
        /// The array of chairs that are in this room.
        /// </summary>
        private Chair[,] chairs;

        /// <summary>
        /// The width of this room.
        /// </summary>
        private int width;

        /// <summary>
        /// The height of this room.
        /// </summary>
        private int height;

        // Constructors

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="height">The width of the room.</param>
        /// <param name="width">The height of the room.</param>
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

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="roomName">The name of the room.</param>
        /// <param name="location">The location of the room.</param>
        /// <param name="description">The description of the room.</param>
        /// <param name="height">The height of the room.</param>
        /// <param name="width">The width of the room.</param>
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

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="file">The location to a room template to initialize the room.</param>
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

                                    // Replace the chair from the default constructor with the information contained in the stored version of the chair
                                    this.Chairs[y, x] = new Chair(
                                        Boolean.Parse(r.GetAttribute("LeftHanded")),
                                        Int32.Parse(r.GetAttribute("FbPosition")), 
                                        Int32.Parse(r.GetAttribute("LrPosition")),
                                        Boolean.Parse(r.GetAttribute("NonChair")), 
                                        Boolean.Parse(r.GetAttribute("MustBeEmpty")),
                                        r.GetAttribute("Name"), 
                                        s);
                                }
                            }
                        }
                    }
                }
            }
        }

        // Events

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        // Properties

        /// <summary>
        /// Gets or sets the name of the room.
        /// </summary>
        /// <value>The name of the room.</value>
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

        /// <summary>
        /// Gets or sets the location of the room.
        /// </summary>
        /// <value>The location of the room.</value>
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

        /// <summary>
        /// Gets or sets the description of the room.
        /// </summary>
        /// <value>The description of the room.</value>
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

        /// <summary>
        /// Gets the array of chairs or a gven chair in the room.
        /// </summary>
        /// <value>The array of chairs.</value>
        public Chair[,] Chairs
        {
            get { return this.chairs; }
        }

        /// <summary>
        /// Gets the width of the room.
        /// </summary>
        /// <value>The width of the room.</value>
        public int Width
        {
            get { return this.width; }
        }

        /// <summary>
        /// Gets the height of the room.
        /// </summary>
        /// <value>The height of the room.</value>
        public int Height
        {
            get { return this.height; }
        }

        /// <summary>
        /// Gets the list of room students.
        /// </summary>
        /// <value>Collection of room students.</value>
        public ObservableCollection<Student> RoomStudents
        {
            get { return this.roomStudents; }
        }

        // Methods

        /// <summary>
        /// Adds a student to the room.
        /// </summary>
        /// <param name="student">The student to be added.</param>
        public void AddStudent(Student student)
        {
            if (!this.roomStudents.Contains(student))
            {
                this.roomStudents.Add(student);
            }
        }

        /// <summary>
        /// Run the specified placement algorithm.
        /// Uses visitor design pattern.
        /// </summary>
        /// <param name="algorithm">The specific algorithm to use for placement.</param>
        public void RunPlacementAlgorithmx(AssignmentVisitor algorithm)
        {
            algorithm.PlaceStudents(this);
        }

        /// <summary>
        /// Determines if the room is empty (all of the chairs have no students in them).
        /// </summary>
        /// <returns>True if the room has no students in the chairs.</returns>
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

        /// <summary>
        /// Writes the current state of the room to a file so that it can be used as a template.
        /// </summary>
        /// <param name="file">The location to save the file.</param>
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

        /// <summary>
        /// Pvodies a string representation of the room.
        /// </summary>
        /// <returns>A string representation of the room.</returns>
        public override string ToString()
        {
            return this.RoomName + " - " + this.Location;
        }

        /// <summary>
        /// Signals that a property of this object has changed.
        /// </summary>
        /// <param name="info">The property that is being affected.</param>
        private void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}

