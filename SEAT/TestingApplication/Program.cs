using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEATLibrary;

namespace TestingApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Make an instance of the seat manager, this is what we are going to use
            SeatManager sm = new SeatManager();
            // Make an instance of the Command Line interface so we can do some testing on our class
            CLInterface cli = new CLInterface();

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
                Console.Write("\n > ");
                prompt = Console.ReadLine();

                Console.Clear();

                if (prompt.Equals("1"))
                {
                    Console.WriteLine("Displaying list of all of the rooms");
                    cli.waitForUserEnter();
                }
                else if (prompt.Equals("2"))
                {
                    Console.WriteLine("Displaying list of all of the students");
                    cli.waitForUserEnter();
                }
                else if (prompt.Equals("3"))
                {
                    Console.WriteLine("Adding a student to the roster");
                    cli.waitForUserEnter();
                }
                else if (prompt.Equals("4"))
                {
                    Console.WriteLine("Adding a room");
                    cli.waitForUserEnter();
                }
                else if (prompt.Equals("exit"))
                {
                    // Let the program fall through to the end.
                }
                else
                {
                    Console.WriteLine("Invalid option, press enter to return to main menu.");
                    Console.ReadLine();
                }

            }

        }
    }
}
