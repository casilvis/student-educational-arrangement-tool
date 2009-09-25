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

            sm.addNewRoom(10, 10);


            /*
            Student s = cli.getNewStudent();
            sm.addStudentToRoster(s);

            cli.displayStudentRoster(sm.StudentList);

            */

            Console.ReadLine();
        }
    }
}
