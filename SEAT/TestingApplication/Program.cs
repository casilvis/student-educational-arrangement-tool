namespace TestingApplication
{
    using System;
    using System.Linq;
    using System.Text;
    using SEATLibrary;

    public class Program
    {
        // Make an instance of the Command Line interface so we can do some testing on our class
        private static CLInterface cli = new CLInterface();

        public static void Main(string[] args)
        {
            // Make an instance of the seat manager, this is what we are going to use
            /*
            Console.Write("File to Open: ");
            String file = Console.ReadLine();
            SeatManager sm = new SeatManager(file);
             */
            SeatManager sm = new SeatManager();

            string prompt = string.Empty;
            while (!prompt.Equals("exit"))
            {
                Console.Clear();

                Console.WriteLine("To close the application simply type exit");
                Console.WriteLine("Make A Selection:");
                Console.WriteLine("1) List rooms");
                Console.WriteLine("2) List students");
                Console.WriteLine("3) Add student");
                Console.WriteLine("4) Add room");
                Console.WriteLine("5) Add room from template");
                Console.WriteLine("6) Save File");
                Console.Write("\n > ");
                prompt = Console.ReadLine();

                Console.Clear();

                if (prompt.Equals("1"))
                {
                    Console.WriteLine("Displaying list of all of the rooms\n");
                    if (sm.RoomList.Count > 0)
                    {
                        int selection = cli.SelectRoomList(sm.RoomList);
                        if (selection >= 0)
                        {
                            cli.UpdateRoom(sm.RoomList[selection]);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Selection...");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rooms available...");
                    }

                    cli.WaitForUserEnter();
                }
                else if (prompt.Equals("2"))
                {
                    Console.WriteLine("Displaying list of all of the students\n");
                    cli.DisplayStudentRoster(sm.StudentList);
                    cli.WaitForUserEnter();
                }
                else if (prompt.Equals("3"))
                {
                    Console.WriteLine("Adding a student to the roster\n");
                    Console.WriteLine("Enter the following information for the student:");
                    Student s = cli.GetNewStudent();
                    sm.AddStudentToRoster(s);
                    cli.WaitForUserEnter();
                }
                else if (prompt.Equals("4"))
                {
                    Console.WriteLine("Adding a room");
                    Room r = cli.GetNewRoom();
                    sm.AddNewRoom(r);
                    cli.WaitForUserEnter();
                }
                else if (prompt.Equals("5"))
                {
                    Console.Write("Location of Template: ");
                    string file = Console.ReadLine();
                    Room r = new Room(file);
                    sm.AddNewRoom(r);
                }
                else if (prompt.Equals("6"))
                {
                    Console.Write("File Name: ");
                    string f = Console.ReadLine();
                    sm.SaveXml(f);
                }
                else if (prompt.Equals("exit"))
                {
                    // Let the program fall through to the end.
                }
                else
                {
                    Console.WriteLine("Invalid option...");
                    cli.WaitForUserEnter();
                }
            }
        }
    }
}
