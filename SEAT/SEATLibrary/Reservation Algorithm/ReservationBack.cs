// <copyright file="ReservationBack.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
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
            throw new NotImplementedException();
        }
    }
}
