using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEATLibrary;

namespace TestingApplication
{
    class CLInterface
    {

        public CLInterface()
        {

        }

        public Student getNewStudent()
        {
            return new Student(promptForString("First Name"), promptForString("Last Name"),
                promptForString("Section"), promptForBoolean("Left Handed"), 
                promptForBoolean("Vision Enpairment"));
        }

        public void displayStudentRoster(List<Student> list)
        {
            Console.WriteLine("Displaying Student Roster");
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].ToString());
            }
        }

        public void displayRoom(Room r)
        {
            for (int i = 0; i < r.Height; i++)
            {
                for (int j = 0; j < r.Width; j++)
                {
                    Console.Write(r.Chairs[i, j].ToString() + "\t");
                }
                Console.WriteLine();
            }
        }

        private String promptForString(String prompt)
        {
            Console.Write(prompt + ": ");
            String response = Console.ReadLine();
            return response;
        }

        private int promptForInt(String prompt)
        {
            String response = promptForString(prompt);
            return Int32.Parse(response);
        }

        private Boolean promptForBoolean(String prompt)
        {
            String response = promptForString(prompt);
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
            if (response.Equals("No") || response.Equals("no") || response.Equals("NO"))
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
