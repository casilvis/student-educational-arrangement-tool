using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEATLibrary
{
    class Chair
    {
        private int primaryID;
        private Room room;
        private int posX;
        private int posY;
        private Boolean leftHanded; //True for left handed people
        private int fbPosition; // 1 = front; 2 = middle; 3 = back
        private int lrOosition; // 1 = left;  2 = right;  3 = back
        private Boolean nonChair; //True if this actually isn't a chair
        private Boolean mustBeEmpty; //True if this chair can't have anyone in it
        private String seatNumber; //The humber assigned to the seat

    }
}
