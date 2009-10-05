using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEATLibrary;

namespace TestingApplication
{
    class Program
    {
        // Make an instance of the Command Line interface so we can do some testing on our class
        private static CLInterface cli = new CLInterface();

        static void Main(string[] args)
        {
            // Make an instance of the seat manager, this is what we are going to use
            /*
            Console.Write("File to Open: ");
            String file = Console.ReadLine();
            SeatManager sm = new SeatManager(file);
             */
            SeatManager sm = new SeatManager();
            

            String prompt = "";
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
                        int selection = cli.selectRoomList(sm.RoomList);
                        if (selection >= 0)
                        {
                            cli.updateRoom(sm.RoomList[selection]);
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
                    cli.waitForUserEnter();
                }
                else if (prompt.Equals("2"))
                {
                    Console.WriteLine("Displaying list of all of the students\n");
                    cli.displayStudentRoster(sm.StudentList);
                    cli.waitForUserEnter();
                }
                else if (prompt.Equals("3"))
                {
                    Console.WriteLine("Adding a student to the roster\n");
                    Console.WriteLine("Enter the following information for the student:");
                    Student s = cli.getNewStudent();
                    sm.addStudentToRoster(s);
                    cli.waitForUserEnter();
                }
                else if (prompt.Equals("4"))
                {
                    Console.WriteLine("Adding a room");
                    Room r = cli.getNewRoom();
                    sm.addNewRoom(r);
                    cli.waitForUserEnter();
                }
                else if (prompt.Equals("5"))
                {
                    Console.Write("Location of Template: ");
                    String file = Console.ReadLine();
                    Room r = new Room(file);
                    sm.addNewRoom(r);
                }
                else if (prompt.Equals("6"))
                {
                    Console.Write("File Name: ");
                    String f = Console.ReadLine();
                    sm.saveXml(f);
                }
                else if (prompt.Equals("exit"))
                {
                    // Let the program fall through to the end.
                }
                else
                {
                    Console.WriteLine("Invalid option...");
                    cli.waitForUserEnter();
                }

            }

        }

        
    }
}
