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
            //This is just a scratch program where various parts of the program are tested
            //No really functional code is here, just some tests of other classes

            SeatManager sm = new SeatManager();

            Console.WriteLine(sm.RoomList[0].ToString());
            Console.ReadLine();
        }
    }
}
