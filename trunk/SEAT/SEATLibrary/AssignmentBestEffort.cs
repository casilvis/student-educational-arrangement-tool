// <copyright file="AssignmentBestEffort.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEATLibrary
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
        /// Constructor for PlaceStudents which attempts to place every student in a room in a seat.
        /// </summary>
        /// <param name="room">Room to be modified.</param>
        public override void PlaceStudents(Room room, int[] spaces, bool[] checks)
        {
            //testing variables

            bool lHand = checks[0];
            bool vImpaired = checks[1];
            bool checkered = checks[2];
            int spacesX = spaces[0];
            int spacesY = spaces[1];
            int priority = spaces[2];

            //Clears the room of students
            for (int i = 0; i < room.Width; i++)
            {
                for (int j = 0; j < room.Height; j++)
                {
                    room.Chairs[i, j].TheStudent = null;
                }
            }

            //Creates an easily manipulated copy of the student list
            List<Student> toBePlacedStudents = new List<Student>();
            foreach (Student s in room.RoomStudents)
                toBePlacedStudents.Add(s);

            room.Chairs[0, 0].TheStudent = toBePlacedStudents[0];

            int l;
            if (priority == -1)
                l = 0;
            else
                l = room.RoomStudents.Count;

            for (int i = 0; i < room.Height; i++)
            {
                int k = 0;
                if (checkered)
                    k = i % 2;
                for (int j = k; j < room.Width; j++)
                {
                    if (!room.Chairs[i, j].MustBeEmpty && !room.Chairs[i, j].NonChair && toBePlacedStudents.Count > 0)
                    {
                        if (room.Chairs[i, j].LeftHanded && lHand)
                        {
                            room.Chairs[i, j].TheStudent = toBePlacedStudents[room.FindLeftHandedStudent()];
                            toBePlacedStudents.RemoveAt(room.FindLeftHandedStudent());
                        }
                        else if (vImpaired)
                        {
                            room.Chairs[i, j].TheStudent = toBePlacedStudents[room.FindVisuallyImpairedStudent()];
                            toBePlacedStudents.RemoveAt(room.FindVisuallyImpairedStudent());
                        }
                        else
                        {
                            room.Chairs[i, j].TheStudent = toBePlacedStudents[0];
                            toBePlacedStudents.RemoveAt(0);
                        }
                        
                    }
                }
            }
            if (toBePlacedStudents.Count > 0)
            {
                for (int i = l; i < room.Height; i += priority)
                {
                    for (int j = 0; j < room.Width; j += priority)
                    {
                        if (!room.Chairs[i, j].MustBeEmpty && !room.Chairs[i, j].NonChair && room.Chairs[i, j].TheStudent == null && toBePlacedStudents.Count > 0)
                        {
                            if (room.Chairs[i, j].LeftHanded && lHand)
                            {
                                room.Chairs[i, j].TheStudent = toBePlacedStudents[room.FindLeftHandedStudent()];
                                toBePlacedStudents.RemoveAt(room.FindLeftHandedStudent());
                            }
                            else
                            {
                                room.Chairs[i, j].TheStudent = toBePlacedStudents[0];
                                toBePlacedStudents.RemoveAt(0);
                            }
                        }
                    }
                }
            }
        }
    }
}
