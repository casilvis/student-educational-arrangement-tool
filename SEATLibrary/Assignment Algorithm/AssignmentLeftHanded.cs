// <copyright file="AssignmentLeftHanded.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Place only the left handed students into left handed seats.</summary>
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
            // Create a shuffeled array of the students for placement
            Student[] students = room.RoomStudents.ToArray<Student>();
            for (int i = 0; i < students.Length; i++)
            {
                int index = AssignmentVisitor.R.Next(0, students.Length);
                Student tmp = students[i];
                students[i] = students[index];
                students[index] = tmp;
            }

            // Place the left handed students into left handed seats.
            for (int n = 0; n < students.Length; n++)
            {
                Student student = students[n];
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
