// <copyright file="ReservationVisitor.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEATLibrary.Reservation_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Reservation visitor algorithm for reserving seats that students can not sit in.
    /// </summary>
    public abstract class ReservationVisitor
    {
        /// <summary>
        /// Algorithm for reserving various patterns of seats that students can not sit in.
        /// </summary>
        /// <param name="room">The room to be modified.</param>
        public abstract void ReserveSeats(Room room);
    }
}
