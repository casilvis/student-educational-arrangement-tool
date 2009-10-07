using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace SEATLibrary
{
    public class Room
    {
        // Attributes
        private String roomName;
        private String location;
        private String description;
        private ObservableCollection<Student> roomStudents;
        private Chair[,] chairs;
        private int width;
        private int height;

        // Properties
        public String RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }
        public String Location
        {
            get { return location; }
            set { location = value; }
        }
        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        public Chair[,] Chairs
        {
            get { return chairs; }
        }

        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }
        public ObservableCollection<Student> RoomStudents
        {
            get { return roomStudents; }
        }

        // Constructors
        public Room()
        {
            this.width = 4;
            this.height = 4;

            this.roomName = "Unknown";
            this.location = "Unknown";
            this.description = "Unknown";
            roomStudents = new ObservableCollection<Student>();
            chairs = new Chair[this.width, this.height];

            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    chairs[i, j] = new Chair();
                }
            }
        }

        public Room(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.roomName = "Unknown";
            this.location = "Unkonwn";
            this.description = "Unknown";
            roomStudents = new ObservableCollection<Student>();
            chairs = new Chair[this.width, this.height];

            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    chairs[i, j] = new Chair();
                }
            }
        }

        public Room(String roomName, String location, String description, int width, int height)
        {
            this.width = width;
            this.height = height;

            this.roomName = roomName;
            this.location = location;
            this.description = description;
            roomStudents = new ObservableCollection<Student>();
            chairs = new Chair[this.width, this.height];

            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    chairs[i, j] = new Chair();
                }
            }
        }

        public Room(string file)
        {
            roomStudents = new ObservableCollection<Student>();

            // Read in the XML document and load all of the data into memory
            XmlReader r = new XmlTextReader(file);
            while (r.Read())
            {
                if (r.NodeType == XmlNodeType.Element && r.Name == "Room")
                {
                    //Read the room's attributes and make a new instance of a room
                    width = Int32.Parse(r.GetAttribute("Width"));
                    height = Int32.Parse(r.GetAttribute("Height"));
                    roomName = r.GetAttribute("Name");
                    location = r.GetAttribute("Location");
                    description = r.GetAttribute("Description");
                    chairs = new Chair[this.width, this.height];

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
                                    //For the student, we assume the chair is blank.
                                    Student s = null;
                                    //Replace the chair from the default constructor with the information contained in the stored version of the chair
                                    Chairs[x, y] = new Chair(Boolean.Parse(r.GetAttribute("LeftHanded")),
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



        //Methods
        public void runPlacementAlgorithmx(AssignmentVisitor algorithm)
        {
            algorithm.placeStudents(this);
        }

        public bool isRoomEmpty()
        {
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    if (!chairs[i, j].isEmpty())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void writeRoomTemplate(String file)
        {
            XmlWriter w = new XmlTextWriter(file, null);
            w.WriteStartDocument();
            w.WriteStartElement("SEATTEMPLATE"); // START SEATTEMPLATE

            w.WriteStartElement("Room"); // START ROOM
            w.WriteAttributeString("Name", RoomName);
            w.WriteAttributeString("Location", Location);
            w.WriteAttributeString("Description", Description);
            w.WriteAttributeString("Width", Width.ToString());
            w.WriteAttributeString("Height", Height.ToString());
            w.WriteStartElement("Chairs"); // START CHAIRS
            for (int j = 0; j < Width; j++)
            {
                for (int k = 0; k < Height; k++)
                {
                    Chair c = Chairs[j, k];
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
                    w.WriteEndElement();//END CHAIR
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

    }
}
