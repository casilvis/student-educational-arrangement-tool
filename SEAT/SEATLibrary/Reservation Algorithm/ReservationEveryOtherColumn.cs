// <copyright file="ReservationEveryOtherColumn.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEATLibrary.Reservation_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Reserve every other column to spread out the students in the classroom.
    /// </summary>
    public class ReservationEveryOtherColumn : ReservationVisitor
    {
        /// <summary>
        /// Run the reservation algorithm.
        /// </summary>
        /// <param name="room">The room to be modified.</param>
        public override void ReserveSeats(Room room)
        {
            for (int i = 0; i < room.Height; i++)
            {
                for (int j = 1; j < room.Width; j += 2)
                {
                    room.Chairs[i, j].MustBeEmpty = true;
                }
            }
        }
    }
}
