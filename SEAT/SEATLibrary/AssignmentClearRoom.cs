// <copyright file="AssignmentClearRoom.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEATLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Removes all students from the seats in a room.
    /// </summary>
    public class AssignmentClearRoom : AssignmentVisitor
    {
        /// <summary>
        /// Constructor for PlaceStudents that removes all students currently in chairs from their chair.
        /// </summary>
        /// <param name="room">The room to be modified.</param>
        public override void PlaceStudents(Room room, int[] spaces, bool[] checks)
        {
            for (int i = 0; i < room.Height; i++)
            {
                for (int j = 0; j < room.Width; j++)
                {
                    room.Chairs[i, j].TheStudent = null;
                }
            }
        }
    }
}
