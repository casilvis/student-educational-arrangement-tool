namespace SEATLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AssignmentClearRoom: AssignmentVisitor
    {
        public override void placeStudents(Room room)
        {
            for (int i = 0; i < room.Width; i++)
            {
                for (int j = 0; j < room.Height; j++)
                {
                    room.Chairs[i, j].TheStudent = null;
                }
            }
        }
    }
}
