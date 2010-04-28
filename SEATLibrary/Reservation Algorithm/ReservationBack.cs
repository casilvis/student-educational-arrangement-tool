// <copyright file="ReservationBack.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Reserve all of the seats in the back of the room.</summary>
namespace SEATLibrary.Reservation_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Reserve all of the seats in the back of the room.
    /// </summary>
    public class ReservationBack : ReservationVisitor
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
                    if (room.Chairs[i, j].FbPosition == 2)
                    {
                        room.Chairs[i, j].MustBeEmpty = true;
                    }
                }
            }
        }
    }
}
