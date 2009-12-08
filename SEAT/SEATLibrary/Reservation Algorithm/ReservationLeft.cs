// <copyright file="ReservationLeft.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEATLibrary.Reservation_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Reserve the left part of the classroom so students can not sit in those seats.
    /// </summary>
    public class ReservationLeft : ReservationVisitor
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
