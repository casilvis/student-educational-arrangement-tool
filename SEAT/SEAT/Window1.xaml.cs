// <copyright file="Window1.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEAT
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using CommonLibrary;
    using SEATLibrary;
    
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        /// <summary>
        /// Main instance of the SeatManager which represents the currently opened file.
        /// </summary>
        private static SeatManager manager = new SeatManager();

        /// <summary>
        /// Initializes a new instance of the Window1 class.
        /// </summary>
        public Window1()
        {
            InitializeComponent();
            this.Title = "SEAT Manager";
            listBoxRooms.ItemsSource = Window1.manager.RoomList;
            listBoxRoster.ItemsSource = Window1.manager.StudentList;
            listBoxSection.ItemsSource = Window1.manager.SectionList;
            SeatManager.FileBecameDirty += new EventHandler(this.SeatManager_FileBecameDirty);
            this.Title = Window1.SManager.ToString();

            // Creating a KeyBinding between the Open command and Ctrl-O
            KeyBinding openCmdKeyBinding = new KeyBinding(ApplicationCommands.Open, Key.O, ModifierKeys.Control);
            this.InputBindings.Add(openCmdKeyBinding);
            CommandBinding openCmdBinding = new CommandBinding(ApplicationCommands.Open, this.OpenCmdExecuted);
            this.CommandBindings.Add(openCmdBinding);

            // Creating a KeyBinding between the Save command and Ctrl-S
            KeyBinding saveCmdKeyBinding = new KeyBinding(ApplicationCommands.Save, Key.S, ModifierKeys.Control);
            this.InputBindings.Add(saveCmdKeyBinding);
            CommandBinding saveCmdBinding = new CommandBinding(ApplicationCommands.Save, this.SaveCmdExecuted);
            this.CommandBindings.Add(saveCmdBinding);
            
            // Creating a KeyBinding between the New command and Ctrl-N
            KeyBinding newCmdKeyBinding = new KeyBinding(ApplicationCommands.New, Key.N, ModifierKeys.Control);
            this.InputBindings.Add(saveCmdKeyBinding);
            CommandBinding newCmdBinding = new CommandBinding(ApplicationCommands.New, this.NewCmdExecuted);
            this.CommandBindings.Add(newCmdBinding);
        }

        /// <summary>
        /// Gets an instance of the current SeatManager that can be used anywhere.
        /// </summary>
        public static SeatManager SManager
        {
            get { return Window1.manager; }
        }

        /// <summary>
        /// Adds a new room to the list of rooms.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonAddRoom_Click(object sender, RoutedEventArgs e)
        {
            ClassOpen classroom = new ClassOpen();
            classroom.ShowDialog();
        }

        /// <summary>
        /// Displays the about window for the application.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void DisplayAboutWindow(object sender, RoutedEventArgs e)
        {
            AboutSEAT popupWindow = new AboutSEAT();
            popupWindow.Owner = this;
            popupWindow.ShowDialog();
        }

        /// <summary>
        /// Creates a new, empty SeatManager, prompts to save if the current manager has unsaved changes.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void NewCmdExecuted(object sender, RoutedEventArgs e)
        {
            bool makenew = true;
            if (SeatManager.Dirty)
            {
                string message = "Do you want to save changes?";
                MessageBoxResult result = MessageBox.Show(message, "SEAT", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Cancel)
                {
                    // Don't do anything
                    makenew = false;
                }
                else if (result == MessageBoxResult.Yes)
                {
                    // Save changes before the file is replaced with a new one
                    this.SaveCmdExecuted(this, null);
                }
            }

            if (makenew)
            {
                Window1.manager = new SeatManager();

                // Bind all of the GUI elements
                listBoxRooms.ItemsSource = Window1.manager.RoomList;
                listBoxRoster.ItemsSource = Window1.manager.StudentList;
                listBoxSection.ItemsSource = Window1.manager.SectionList;
                this.Title = Window1.SManager.ToString();
            }
        }

        /// <summary>
        /// Provides a prompt to open a file that loads a saved copy of SeatManager.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OpenCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".seat"; // Default file extension
            dlg.Filter = "SEAT File (.seat)|*.seat"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                Window1.manager = new SeatManager(dlg.FileName);
                
                // Bind all of the GUI elements
                listBoxRooms.ItemsSource = Window1.manager.RoomList;
                listBoxRoster.ItemsSource = Window1.manager.StudentList;
                listBoxSection.ItemsSource = Window1.manager.SectionList;
                this.Title = Window1.SManager.ToString();
            }
        }

        /// <summary>
        /// Saves the current copy of SeatManager, reverts to save as if file has not been saved yet.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void SaveCmdExecuted(object sender, RoutedEventArgs e)
        {
            // If we can save the file where it already was we will, otherwise prompt to save the file
            if (!Window1.manager.SaveXml())
            {
                this.FileMenuSaveAs_Click(sender, e);
            }
        }

        /// <summary>
        /// Save the current SeatManager to a new file.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void FileMenuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".seat"; // Default file extension
            dlg.Filter = "SEAT File (.seat)|*.seat"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                Window1.manager.SaveXml(dlg.FileName);
            }
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void FileMenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Provides the prompt to add a new student to the roster of students.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonAddStudent_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd student = new StudentAdd();
            student.ShowDialog();
        }

        /// <summary>
        /// Provide the prompt to edit the selected student.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonEditStudent_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxRoster.SelectedValue != null)
            {
                StudentAdd student = new StudentAdd(listBoxRoster.SelectedValue as Student);
                student.ShowDialog();
            }
        }

        /// <summary>
        /// Loads the window for editing the selected room.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonEditRoom_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxRooms.SelectedValue != null)
            {
                frmGrid grid = new frmGrid(listBoxRooms.SelectedValue as Room, true);
                grid.ShowDialog();
            }
        }

        /// <summary>
        /// Loads the window for placing students into seats in a room.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonPlace_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxRooms.SelectedValue != null)
            {
                frmGrid grid = new frmGrid(listBoxRooms.SelectedValue as Room, false);
                grid.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a Room");
            }
        }

        /// <summary>
        /// Delets the selected student (not implemented yet).
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void DeleteStudent(object sender, RoutedEventArgs e)
        {
            // NOT IMPLEMENTED YET
        }

        /// <summary>
        /// Prompts for a roster to import into the SeatManager.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ImportRoster_Click(object sender, RoutedEventArgs e)
        {
            RosterImporter ri = new RosterImporter();
            if (ri.IsOpened)
            {
                ri.ShowDialog();
            }
        }

        /// <summary>
        /// Adds all of the selected students to the selected room.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonAddStudentsToRoom_Click(object sender, RoutedEventArgs e)
        {
            if ((Room)listBoxRooms.SelectedItem == null)
            {
                MessageBox.Show("No room selected");
            }
            else
            {
                Room room = (Room)listBoxRooms.SelectedItem;
                IList list = listBoxRoster.SelectedItems;
                for (int i = 0; i < list.Count; i++)
                {
                    Student s = (Student)list[i];
                    room.AddStudent(s);
                }
            }
        }

        /// <summary>
        /// Selects all of the students in the roster.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonSelectStudents_Click(object sender, RoutedEventArgs e)
        {
            listBoxRoster.SelectAll();
        }

        /// <summary>
        /// Deselects all of the students in the roster.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonDeselectStudents_Click(object sender, RoutedEventArgs e)
        {
            listBoxRoster.UnselectAll();
        }

        /// <summary>
        /// Add all students in the selected section to the selected room.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonAddSectionToRoom_Click(object sender, RoutedEventArgs e)
        {
            if ((Room)listBoxRooms.SelectedItem == null)
            {
                MessageBox.Show("No room selected");
            }
            else if ((string)this.listBoxSection.SelectedItem == null)
            {
                MessageBox.Show("No section selected");
            }
            else
            {
                Room room = (Room)listBoxRooms.SelectedItem;
                string s = this.listBoxSection.SelectedItem as string;

                for (int i = 0; i < Window1.SManager.StudentList.Count; i++)
                {
                    Student student = Window1.SManager.StudentList[i];
                    if (student.Section == s)
                    {
                        room.AddStudent(student);
                    }
                }
            }
        }

        /// <summary>
        /// Keep the list of room students updated depending on which room is selected.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ListBoxRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room room = (Room)listBoxRooms.SelectedItem;
            if (room != null)
            {
                listBoxStudents.ItemsSource = room.RoomStudents;
            }
            else
            {
                listBoxStudents.ItemsSource = null;
            }
        }

        /// <summary>
        /// Load the help web page.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void MenuItemHelp_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/student-educational-arrangement-tool/wiki/Help");
        }

        /// <summary>
        /// Load the documentation web page.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void MenuItemDocumentation_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/student-educational-arrangement-tool/wiki/Documentation");
        }

        /// <summary>
        /// Print the seating assignments for the selected room.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void PrintSeatingChart_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)listBoxRooms.SelectedItem;
            if (room == null)
            {
                MessageBox.Show("No room selected!");
            }
            else
            {
                PCPrint printer = new PCPrint();

                // Set the font we want to use
                printer.PrinterFont = new Font("Verdana", 10);

                // Set the TextToPrint property
                Chair[,] roomchairs = room.Chairs;
                ObservableCollection<Student> studentsInRoom = new ObservableCollection<Student>();
                ObservableCollection<string> chairLocations = new ObservableCollection<string>();
                foreach (Chair chair in roomchairs)
                {
                    if (chair.TheStudent != null)
                    {
                        studentsInRoom.Add(chair.TheStudent);
                        chairLocations.Add(chair.SeatName);
                    }
                }

                if (studentsInRoom.Count == 0)
                {
                    MessageBox.Show("There are no students placed in this room, cannot print.");
                    return;
                }

                string studentList = " ";
                Console.Write(studentsInRoom.Count);
                for (int i = 0; i < studentsInRoom.Count; i++)
                {
                    Student currentStudent = studentsInRoom.ElementAt(i);
                    studentList += currentStudent.LastName + ", " + currentStudent.FirstName + ": " + chairLocations.ElementAt(i) + "\n";
                }

                printer.TextToPrint = studentList;

                // Issue print command
                printer.Print();
            }
        }

        /// <summary>
        /// Update the title bar when the file becomes dirty.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void SeatManager_FileBecameDirty(object sender, EventArgs e)
        {
            this.Title = Window1.SManager.ToString();
        }

        /// <summary>
        /// Intercept the file closing and provide a user with a prompt to save their unsaved work.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (SeatManager.Dirty)
            {
                string message = "Do you want to save changes before exiting?";
                MessageBoxResult result = MessageBox.Show(message, "SEAT", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Cancel)
                {
                    // User does not want to close the window.
                    e.Cancel = true;
                }
                else if (result == MessageBoxResult.Yes)
                {
                    this.SaveCmdExecuted(this, null);
                }
            }
        }
    }
}
