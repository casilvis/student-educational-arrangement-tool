using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEATLibrary
{
    class Chair
    {
        Boolean leftHanded; //True for left handed people
        int fbPosition; // 1 = front; 2 = middle; 3 = back
        int lrOosition; // 1 = left;  2 = right;  3 = back
        Boolean nonChair; //True if this actually isn't a chair
        Boolean mustBeEmpty; //True if this chair can't have anyone in it
        String seatNumber; //The humber assigned to the seat

    }
}
