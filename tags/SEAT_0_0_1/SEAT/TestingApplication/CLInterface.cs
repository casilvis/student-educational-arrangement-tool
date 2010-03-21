// <copyright file="CLInterface.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace TestingApplication
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using SEATLibrary;

    /// <summary>
    /// Exposes useful command line tools that are able to perform common tasks.
    /// </summary>
    internal class CLInterface
    {
        /// <summary>
        /// Initializes a new instance of the CLInterface class.
        /// </summary>
        public CLInterface()
        {
            // Nothing needs to be set up for this class to work
            // The methods could all be static, but that isn't necessary for this test class now.
        }

        /// <summary>
        /// Prompts the user for the necessary information to create a student.
        /// </summary>
        /// <returns>A new instance of a student.</returns>
        public Student GetNewStudent()
        {
            return new Student(
                this.PromptForString("First Name"),
                this.PromptForString("Last Name"),
                this.PromptForString("Student ID"),
                this.PromptForString("Section"),
                this.PromptForbool("Left Handed"),
                this.PromptForbool("Vision Enpairment"));
        }

        /// <summary>
        /// Prompts the user for the necessary information to create a room.
        /// </summary>
        /// <returns>A new instance of a room.</returns>
        public Room GetNewRoom()
        {
            return new Room(
                this.PromptForString("Room Name"),
                this.PromptForString("Room Location"),
                this.PromptForString("Room Description"),
                this.PromptForInt("Room Height"),
                this.PromptForInt("Room Width"));
        }

        /// <summary>
        /// Displays all of the students.
        /// </summary>
        /// <param name="list">The list of students to be displayed.</param>
        public void DisplayStudentRoster(ObservableCollection<Student> list)
        {
            Console.WriteLine("Displaying Student Roster");
            if (list.Count == 0)
            {
                Console.WriteLine("There are currently no students.");
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine(list[i].ToString());
                }
            }
        }

        /// <summary>
        /// Displays all of the rooms.
        /// </summary>
        /// <param name="roomList">The list of rooms to be displayed.</param>
        public void DisplayRoomList(ObservableCollection<Room> roomList)
        {
            Console.WriteLine("List of all room names");
            if (roomList.Count == 0)
            {
                Console.WriteLine("There are currently no rooms.");
            }
            else
            {
                for (int i = 0; i < roomList.Count; i++)
                {
                    Console.WriteLine(i + ") " + roomList[i].RoomName);
                }
            }
        }

        /// <summary>
        /// Prompts the user to select one of the rooms.
        /// </summary>
        /// <param name="roomList">The list of rooms to choose from.</param>
        /// <returns>The index of the selected room.</returns>
        public int SelectRoomList(ObservableCollection<Room> roomList)
        {
            Console.WriteLine("List of all room names");
            if (roomList.Count == 0)
            {
                Console.WriteLine("There are currently no rooms.");
                return -1;
            }
            else
            {
                Console.WriteLine("Choose room from list: ");
                for (int i = 0; i < roomList.Count; i++)
                {
                    Console.WriteLine((i + 1) + ") " + roomList[i].RoomName);
                }

                int n = this.PromptForInt("Selection");
                n--;
                if (n > roomList.Count || n < 0)
                {
                    return -1;
                }

                return n;
            }
        }

        /// <summary>
        /// Provide an interface to modify a room.
        /// </summary>
        /// <param name="room">The room to be modified.</param>
        public void UpdateRoom(Room room)
        {
            string prompt = string.Empty;
            while (!prompt.Equals("back"))
            {
                Console.Clear();
                this.DisplayRoom(room);
                Console.WriteLine("Choose: update, savetemplate, back");
                Console.Write(" > ");
                prompt = Console.ReadLine();

                if (prompt.Equals("update"))
                {
                    this.UpdateRoomSeat(room);
                }
                else if (prompt.Equals("savetemplate"))
                {
                    Console.Write("Location to save Template: ");
                    string file = Console.ReadLine();
                    room.WriteRoomTemplate(file);
                }
                else if (prompt.Equals("back"))
                {
                    // Nothing needs to be done here
                }
                else
                {
                    Console.WriteLine("Invalid input...");
                    this.WaitForUserEnter();
                }
            }
        }

        /// <summary>
        /// Provide an interface to modify a seat.
        /// </summary>
        /// <param name="room">The room (which contains seats) to be modified.</param>
        public void UpdateRoomSeat(Room room)
        {
            // Have the user select one of the seats in the room
            Console.Clear();
            this.DisplayRoom(room);
            Console.WriteLine();
            int x = 0, y = 0;
            while (x < 1 || x > room.Width)
            {
                x = this.PromptForInt("Column");
            }

            x--;
            while (y < 1 || x > room.Height)
            {
                y = this.PromptForInt("Row");
            }

            y--;
            Chair c = room.Chairs[x, y];

            // Tell the user about the current seat
            int input = -1;
            while (input != 0)
            {
                Console.Clear();
                Console.WriteLine("Updating Char at position (" + (x + 1) + ", " + (y + 1) + ")\n");
                Console.WriteLine("0) Finish Editing Chair");
                Console.WriteLine("1) Left Handed: " + c.LeftHanded);
                Console.WriteLine("2) Non Chair: " + c.NonChair);
                Console.WriteLine("3) Must Be Empty: " + c.MustBeEmpty);
                Console.WriteLine("4) Seat Number: " + c.SeatName);

                input = this.PromptForInt("Selection");

                if (input == 0)
                {
                    // No action
                }
                else if (input == 1)
                {
                    // Flip the LeftHanded flag
                    if (c.LeftHanded == true)
                    {
                        c.LeftHanded = false;
                    }
                    else
                    {
                        c.LeftHanded = true;
                    }
                }
                else if (input == 2)
                {
                    // Flip the NonChair flag
                    if (c.NonChair == true)
                    {
                        c.NonChair = false;
                    }
                    else
                    {
                        c.NonChair = true;
                    }
                }
                else if (input == 3)
                {
                    // Flip the MustBeEmpty flag
                    if (c.MustBeEmpty == true)
                    {
                        c.MustBeEmpty = false;
                    }
                    else
                    {
                        c.MustBeEmpty = true;
                    }
                }
                else if (input == 4)
                {
                    c.SeatName = this.PromptForString("New Chair Name: ");
                }
            }

            this.WaitForUserEnter();
        }

        /// <summary>
        /// Displays the room.
        /// </summary>
        /// <param name="r">The room to be displayed.</param>
        public void DisplayRoom(Room r)
        {
            Console.Write("\t");
            for (int i = 0; i < r.Width; i++)
            {
                Console.Write((i + 1) + "\t");
            }

            Console.WriteLine();
            for (int i = 0; i < r.Height; i++)
            {
                Console.Write((i + 1) + "\t");
                for (int j = 0; j < r.Width; j++)
                {
                    Console.Write(r.Chairs[i, j].ToString() + "\t");
                }

                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Waits for the user to press enter before continuing.
        /// </summary>
        public void WaitForUserEnter()
        {
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        /// <summary>
        /// Retreives a string from the command line.
        /// </summary>
        /// <param name="prompt">The prompt to give the user.</param>
        /// <returns>The retreived string.</returns>
        private string PromptForString(string prompt)
        {
            Console.Write(prompt + ": ");
            string response = Console.ReadLine();
            return response;
        }

        /// <summary>
        /// Retreives an integer from the command line.
        /// </summary>
        /// <param name="prompt">The prompt to give the user.</param>
        /// <returns>The retreived integer.</returns>
        private int PromptForInt(string prompt)
        {
            string response = this.PromptForString(prompt);
            return Int32.Parse(response);
        }

        /// <summary>
        /// Retreives a boolean value from the command line.
        /// </summary>
        /// <param name="prompt">The prompt to give the user.</param>
        /// <returns>The retreived bool.</returns>
        private bool PromptForbool(string prompt)
        {
            string response = this.PromptForString(prompt);
            if (response.Equals("Yes") || response.Equals("yes") || response.Equals("YES"))
            {
                return true;
            }
            else if (response.Equals("Y") || response.Equals("y"))
            {
                return true;
            }
            else if (response.Equals("True") || response.Equals("true") || response.Equals("TRUE"))
            {
                return true;
            }
            else if (response.Equals("T") || response.Equals("t"))
            {
                return true;
            }
            else if (response.Equals("No") || response.Equals("no") || response.Equals("NO"))
            {
                return true;
            }
            else if (response.Equals("N") || response.Equals("n"))
            {
                return true;
            }
            else if (response.Equals("False") || response.Equals("false") || response.Equals("FALSE"))
            {
                return true;
            }
            else if (response.Equals("F") || response.Equals("f"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
