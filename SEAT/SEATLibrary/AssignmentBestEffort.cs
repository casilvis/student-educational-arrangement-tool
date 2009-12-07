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
            room.RunPlacementAlgorithmx(new AssignmentClearRoom(), spaces, checks);

            //Creates an easily manipulated copy of the student list
            List<Student> toBePlacedStudents = new List<Student>();
            foreach (Student s in room.RoomStudents)
                toBePlacedStudents.Add(s);

            int startI, endI, incrementI = 0;
            if (priority == 1)
            {
                startI = 0;
                endI = room.Height;
                incrementI = 1 + spacesX;
            }
            else if (priority == 0)
            {
                startI = room.Height / 2;
                endI = room.Height;
                incrementI = 1;

            }
            else
            {
                startI = room.Height-1;
                endI = room.Height;
                incrementI = -1 - spacesX;
            }

            for (int i = startI; i < endI; i+=incrementI)
            {
                if (i < 0 || i > room.RoomStudents.Count)
                    break;
                /*if (priority == 0)
                {
                    if (i % 2 == 1)
                        incrementI = (incrementI - 1) * -1;
                    else
                        incrementI = (incrementI + 1) * -1;
                }*/
                int startJ = 0, incrementJ = 1;
                if (checkered)
                {
                    startJ = i % 2;
                    incrementJ = 2;
                }
                for (int j = startJ; j < room.Width; j+=(incrementJ+spacesY))
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
                for (int i = startI; i < room.Height; i += incrementI)
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
