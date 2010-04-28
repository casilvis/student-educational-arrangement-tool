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
        /// A random number generator used for randomizing the list of students.
        /// </summary>
        private static Random r = new Random();

        /// <summary>
        /// Gets the Random number generator.
        /// </summary>
        /// <value>The Random number generator.</value>
        internal static Random R
        {
            get { return r; }
        }

        /// <summary>
        /// Algorithm that will modify the room by placing students into seats.
        /// </summary>
        /// <param name="room">Room to be modified.</param>
        public abstract void PlaceStudents(Room room);
    }
}
