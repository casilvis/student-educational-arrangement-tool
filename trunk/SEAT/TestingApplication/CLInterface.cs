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
    /// 
    /// </summary>
    internal class CLInterface
    {
        /// <summary>
        /// 
        /// </summary>
        public CLInterface()
        {
            // Nothing needs to be set up for this class to work
            // The methods could all be static, but that isn't necessary for this test class now.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Student GetNewStudent()
        {
            return new Student(
                this.PromptForstring("First Name"),
                this.PromptForstring("Last Name"),
                this.PromptForstring("Student ID"),
                this.PromptForstring("Section"),
                this.PromptForbool("Left Handed"),
                this.PromptForbool("Vision Enpairment"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Room GetNewRoom()
        {
            return new Room(
                this.PromptForstring("Room Name"),
                this.PromptForstring("Room Location"),
                this.PromptForstring("Room Description"),
                this.PromptForInt("Room Height"),
                this.PromptForInt("Room Width"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
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
        /// 
        /// </summary>
        /// <param name="roomList"></param>
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
        /// 
        /// </summary>
        /// <param name="roomList"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="room"></param>
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
        /// 
        /// </summary>
        /// <param name="room"></param>
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
                    c.SeatName = this.PromptForstring("New Chair Name: ");
                }
            }

            this.WaitForUserEnter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
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
        /// 
        /// </summary>
        public void WaitForUserEnter()
        {
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        private string PromptForstring(string prompt)
        {
            Console.Write(prompt + ": ");
            string response = Console.ReadLine();
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        private int PromptForInt(string prompt)
        {
            string response = this.PromptForstring(prompt);
            return Int32.Parse(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        private bool PromptForbool(string prompt)
        {
            string response = this.PromptForstring(prompt);
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
