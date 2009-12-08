// <copyright file="ReservationClear.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEATLibrary.Reservation_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Remove all seat reservations from the room.
    /// </summary>
    public class ReservationClear : ReservationVisitor
    {
        /// <summary>
        /// Run the reservation algorithm.
        /// </summary>
        /// <param name="room">The room to be modified.</param>
        public override void ReserveSeats(Room room)
        {
            for (int i = 0; i < room.Height; i++)
            {
                for (int j = 0; j < room.Width; j++)
                {
                    room.Chairs[i, j].MustBeEmpty = false;
                }
            }
        }
    }
}