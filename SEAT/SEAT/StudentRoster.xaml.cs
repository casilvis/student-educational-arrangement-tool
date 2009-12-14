// <copyright file="StudentRoster.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Student Roster used for printing.</summary>
namespace SEAT
{
    using System;
    using System.Collections;
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
        public StudentRoster(ObservableCollection<Student> students)
        {
            // Implement me!
        }

        /// <summary>
        /// Initializes a new instance of the StudentRoster class.
        /// Loads the text box with the seating chart assignments for the given room.
        /// </summary>
        /// <param name="room">The room to display the seat assignments for.</param>
        public StudentRoster(Room room)
        {
            // Implement me!
        }

        /// <summary>
        /// Save the current rich text box which was loaded roster to a text file for use later.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void FileMenuSave_Click(object sender, RoutedEventArgs e)
        {
            // Implement me!
        }

        /// <summary>
        /// Print the current rich text box which was loaded with the roster.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void FileMenuPrint_Click(object sender, RoutedEventArgs e)
        {
            // Implement me!
        }
    }
}
