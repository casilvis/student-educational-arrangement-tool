// <copyright file="ReservationEveryOtherRow.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEATLibrary.Reservation_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Reserve every other row to spread out students evenly in the classroom.
    /// </summary>
    public class ReservationEveryOtherRow : ReservationVisitor
    {
        /// <summary>
        /// Run the reservation algorithm.
        /// </summary>
        /// <param name="room">The room to be modified.</param>
        public override void ReserveSeats(Room room)
        {
            for (int i = 1; i < room.Height; i += 2)
            {
                for (int j = 0; j < room.Width; j++)
                {
                    room.Chairs[i, j].MustBeEmpty = true;
                }
            }
        }
    }
}
