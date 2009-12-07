// <copyright file="Window1.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEAT
{
    using System;
    using System.Drawing;
    using System.Collections;
    using System.Diagnostics;
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
    using SEATLibrary;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private static SeatManager manager = new SeatManager();

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

        public static SeatManager SManager
        {
            get { return Window1.manager; }
        }

        private void ButtonAddRoom_Click(object sender, RoutedEventArgs e)
        {
            ClassOpen classroom = new ClassOpen();
            classroom.ShowDialog();
        }

        private void DisplayAboutWindow(object sender, RoutedEventArgs e)
        {
            AboutSEAT popupWindow = new AboutSEAT();
            popupWindow.Owner = this;
            popupWindow.ShowDialog();
        }

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

        private void OpenCmdExecuted(object target, ExecutedRoutedEventArgs e)
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

        private void SaveCmdExecuted(object sender, RoutedEventArgs e)
        {
            // If we can save the file where it already was we will, otherwise prompt to save the file
            if (!Window1.manager.SaveXml())
            {
                this.FileMenuSaveAs_Click(sender, e);
            }
        }

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

        private void FileMenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAddStudent_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd student = new StudentAdd();
            student.ShowDialog();
        }

        private void ButtonEditStudent_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxRoster.SelectedValue != null)
            {
                StudentAdd student = new StudentAdd(listBoxRoster.SelectedValue as Student);
                student.ShowDialog();
            }
        }

        private void ButtonEditRoom_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxRooms.SelectedValue != null)
            {
                frmGrid grid = new frmGrid(listBoxRooms.SelectedValue as Room, true);
                grid.ShowDialog();
            }
        }

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

        private void DeleteStudent(object sender, RoutedEventArgs e)
        {
            // NOT IMPLEMENTED YET
        }

        private void ImportRoster_Click(object sender, RoutedEventArgs e)
        {
            RosterImporter ri = new RosterImporter();
            if (ri.IsOpened)
            {
                ri.ShowDialog();
            }
        }

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

        private void buttonSelectStudents_Click(object sender, RoutedEventArgs e)
        {
            listBoxRoster.SelectAll();
        }

        private void buttonDeselectStudents_Click(object sender, RoutedEventArgs e)
        {
            listBoxRoster.UnselectAll();
        }

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

        private void MenuItemHelp_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/student-educational-arrangement-tool/wiki/Help");
        }

        private void MenuItemDocumentation_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/student-educational-arrangement-tool/wiki/Documentation");
        }

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
                //Set the font we want to use
                printer.PrinterFont = new Font("Verdana", 10);
                //Set the TextToPrint property
                Chair[,] roomchairs = room.Chairs;
                ObservableCollection<Student> studentsInRoom = new ObservableCollection<Student>();
                ObservableCollection<String> chairLocations = new ObservableCollection<String>();
                foreach(Chair chair in roomchairs)
                {

                        if (chair.TheStudent != null)
                        {
                            studentsInRoom.Add(chair.TheStudent);
                            chairLocations.Add(chair.SeatName);
                        }
                }

                String studentList = "";
                Console.Write(studentsInRoom.Count);
                for (int i = 0; i < studentsInRoom.Count; i++)
                {
                    Student currentStudent = studentsInRoom.ElementAt(i);
                    studentList += currentStudent.LastName + ", " + currentStudent.FirstName + ": " + chairLocations.ElementAt(i)+"\n";
                }
                printer.TextToPrint = studentList;
                //Issue print command
                printer.Print();
            }
        }

        private void SeatManager_FileBecameDirty(object sender, EventArgs e)
        {
            this.Title = Window1.SManager.ToString();
        }

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
