// <copyright file="AssignmentVisitor.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Assignment visitor algorithm for placing students in a room.</summary>
namespace SEATLibrary.Assignment_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Assignment visitor algorithm for placing students in a room.
    /// </summary>
    public abstract class AssignmentVisitor
    {
        /// <summary>
        /// Algorithm that will modify the room by placing students into seats.
        /// </summary>
        /// <param name="room">Room to be modified.</param>
        public abstract void PlaceStudents(Room room);
    }
}
