// <copyright file="AssignmentBestEffort.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Places the room students into seats as best as possible.</summary>
namespace SEATLibrary.Assignment_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Places the room students in seats as best as possible.
    /// </summary>
    public class AssignmentBestEffort : AssignmentVisitor
    {
        /// <summary>
        /// Algorithm for PlaceStudents which attempts to place every student in a room in a seat.
        /// </summary>
        /// <param name="room">Room to be modified.</param>
        public override void PlaceStudents(Room room)
        {
            // Place all of the left handed vision impaired students
            for (int n = 0; n < room.RoomStudents.Count; n++)
            {
                Student student = room.RoomStudents[n];
                bool seated = room.IsStudentSeated(student);

                // Ignore right handed and non-vision impaired students
                if (student.LeftHanded == false || student.VisionImpairment == false)
                {
                    seated = true;
                }

                // Attempt to find a seat
                for (int i = 0; i < room.Height && !seated; i++)
                {
                    for (int j = 0; j < room.Width && !seated; j++)
                    {
                        Chair chair = room.Chairs[i, j];
                        if (chair.IsEmpty() && !chair.MustBeEmpty && !chair.NonChair && chair.LeftHanded && chair.FbPosition == 0)
                        {
                            chair.TheStudent = student;
                            seated = true;
                        }
                    }
                }
            }

            // Place all of the vision impaired students
            for (int n = 0; n < room.RoomStudents.Count; n++)
            {
                Student student = room.RoomStudents[n];
                bool seated = room.IsStudentSeated(student);

                // Ignore right handed and non-vision impaired students
                if (student.VisionImpairment == false)
                {
                    seated = true;
                }

                // Attempt to find a seat
                for (int i = 0; i < room.Height && !seated; i++)
                {
                    for (int j = 0; j < room.Width && !seated; j++)
                    {
                        Chair chair = room.Chairs[i, j];
                        if (chair.IsEmpty() && !chair.MustBeEmpty && !chair.NonChair && chair.FbPosition == 0)
                        {
                            chair.TheStudent = student;
                            seated = true;
                        }
                    }
                }
            }

            // Place all of the left handed students
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

            // Place everyone else
            for (int n = 0; n < room.RoomStudents.Count; n++)
            {
                Student student = room.RoomStudents[n];
                bool seated = room.IsStudentSeated(student);

                // Attempt to find a seat
                for (int i = 0; i < room.Height && !seated; i++)
                {
                    for (int j = 0; j < room.Width && !seated; j++)
                    {
                        Chair chair = room.Chairs[i, j];
                        if (chair.IsEmpty() && !chair.MustBeEmpty && !chair.NonChair && chair.LeftHanded == student.LeftHanded)
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
