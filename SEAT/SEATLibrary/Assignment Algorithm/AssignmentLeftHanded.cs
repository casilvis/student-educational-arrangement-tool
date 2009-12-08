﻿// <copyright file="AssignmentLeftHanded.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEATLibrary.Assignment_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Place only the left handed students into left handed seats.
    /// </summary>
    public class AssignmentLeftHanded : AssignmentVisitor
    {
        /// <summary>
        /// Algorithm for PlaceStudents which only places left handed students into left handed seats.
        /// </summary>
        /// <param name="room">The room to be modified.</param>
        public override void PlaceStudents(Room room)
        {
            // Place the left handed students into left handed seats.
            for (int n = 0; n < room.RoomStudents.Count; n++)
            {
                Student student = room.RoomStudents[n];
                bool seated = room.IsStudentSeated(student);

                // Ignore right handed and non-vision impaired students
                if (student.LeftHanded == false)
                {
                    seated = true;
                }

                // Attempt to find a seat
                for (int i = 0; i < room.Height && !seated; i++)
                {
                    for (int j = 0; j < room.Width && !seated; j++)
                    {
                        Chair chair = room.Chairs[i, j];
                        if (chair.IsEmpty() && !chair.MustBeEmpty && !chair.NonChair && chair.LeftHanded)
                        {
                            chair.TheStudent = student;
                            seated = true;
                        }
                    }
                }
            }
        }
    }
}
