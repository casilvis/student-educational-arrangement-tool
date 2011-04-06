// <copyright file="StudentRoster.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Student Roster used for printing.</summary>
namespace SEAT
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using SEATLibrary;

    /// <summary>
    /// Student Roster used for printing.
    /// </summary>
    public partial class StudentRoster : Window
    {
        /// <summary>
        /// Initializes a new instance of the StudentRoster class.
        /// The text box is left blank for the default constructor.
        /// </summary>
        public StudentRoster()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the StudentRoster class.
        /// Loads the text box with the list of students broken down by section.
        /// </summary>
        /// <param name="students">The list of students to display.</param>
        public StudentRoster(ReadOnlyObservableCollection<Student> students)
        {
            InitializeComponent();

            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Create the list of sections
            List<string> sections = this.GetSectionList(students);
            for (int i = 0; i < sections.Count; i++)
            {
                // Add the section
                Paragraph p = new Paragraph(new Run("Section " + sections[i]));
                p.FontSize = 14;
                p.FontWeight = FontWeights.Bold;
                myFlowDoc.Blocks.Add(p);

                // Add the students that are in the section
                List<Student> sectionStudents = this.GetStudentListInSection(students, sections[i]);
                string listOfStudents = string.Empty;
                for (int j = 0; j < sectionStudents.Count; j++)
                {
                    Student s = sectionStudents[j];
                    listOfStudents += this.StudentNameForRoster(s) + "\n";
                }

                myFlowDoc.Blocks.Add(new Paragraph(new Run(listOfStudents)));
            }

            // Add content to the RichTextBox.
            richTextBox.Document = myFlowDoc;
        }

        /// <summary>
        /// Initializes a new instance of the StudentRoster class.
        /// Loads the text box with the seating chart assignments for the given room.
        /// </summary>
        /// <param name="room">The room to display the seat assignments for.</param>
        public StudentRoster(Room room)
        {
            InitializeComponent();

            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Create the list of sections
            List<string> sections = this.GetSectionList(room.RoomStudents);
            for (int i = 0; i < sections.Count; i++)
            {
                // Add the section
                Paragraph p = new Paragraph(new Run("Section " + sections[i]));
                p.FontSize = 14;
                p.FontWeight = FontWeights.Bold;
                myFlowDoc.Blocks.Add(p);

                // Add the students that are in the section
                List<Student> sectionStudents = this.GetStudentListInSection(room.RoomStudents, sections[i]);
                string listOfStudents = string.Empty;
                for (int j = 0; j < sectionStudents.Count; j++)
                {
                    Student s = sectionStudents[j];
                    listOfStudents += s.LastName + ", " + s.FirstName;

                    if (room.IsStudentSeated(s))
                    {
                        listOfStudents += " (" + room.GetStudentSeatNumber(s) + ")";
                    }
                    else
                    {
                        listOfStudents += " (Not Seated)";
                    }

                    listOfStudents += "\n";
                }

                myFlowDoc.Blocks.Add(new Paragraph(new Run(listOfStudents)));
            }

            // Add content to the RichTextBox.
            richTextBox.Document = myFlowDoc;
        }

        /// <summary>
        /// Save the current rich text box which was loaded roster to a text file for use later.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void FileMenuSave_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement a save feature (Issue 18)
            MessageBox.Show("Sorry, this feature is not yet implemented.");
        }

        /// <summary>
        /// Print the current rich text box which was loaded with the roster.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void FileMenuPrint_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement a print feature (Issue 19)
            MessageBox.Show("Sorry, this feature is not yet implemented.");
        }

        /// <summary>
        /// Create the list of sections based off of the list of studends.
        /// </summary>
        /// <param name="students">A list of students.</param>
        /// <returns>A list of sections that is sorted.</returns>
        private List<string> GetSectionList(ReadOnlyObservableCollection<Student> students)
        {
            List<string> sections = new List<string>();
            for (int i = 0; i < students.Count; i++)
            {
                if (!sections.Contains(students[i].Section))
                {
                    sections.Add(students[i].Section);
                }
            }

            sections.Sort();
            return sections;
        }

        /// <summary>
        /// Get the subset of the list of students who are part of the specified section.
        /// </summary>
        /// <param name="students">A list of students.</param>
        /// <param name="section">The section that will be extracted.</param>
        /// <returns>A list of students in the section that is sorted.</returns>
        private List<Student> GetStudentListInSection(ReadOnlyObservableCollection<Student> students, string section)
        {
            List<Student> output = new List<Student>();
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].Section.Equals(section))
                {
                    output.Add(students[i]);
                }
            }

            output.Sort();
            return output;
        }

        /// <summary>
        /// Get a string representation that includes the students name and if they are left handed or vision impaired.
        /// </summary>
        /// <param name="student">The student to convert to a string.</param>
        /// <returns>A string representation of the student.</returns>
        private string StudentNameForRoster(Student student)
        {
            string output = student.LastName + ", " + student.FirstName;
            if (student.LeftHanded)
            {
                output += " (Left Handed)";
            }

            if (student.VisionImpairment)
            {
                output += " (Vision)";
            }

            return output;
        }
    }
}
