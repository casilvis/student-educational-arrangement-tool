﻿namespace SEAT
{
    using System;
    using System.Collections;
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
            lbxRooms.ItemsSource = Window1.manager.RoomList;
            lbxRoster.ItemsSource = Window1.manager.StudentList;

            // Creating a KeyBinding between the Open command and Ctrl-O
            KeyBinding openCmdKeyBinding = new KeyBinding(ApplicationCommands.Open, Key.O, ModifierKeys.Control);
            this.InputBindings.Add(openCmdKeyBinding);
            CommandBinding openCmdBinding = new CommandBinding(ApplicationCommands.Open, OpenCmdExecuted);
            this.CommandBindings.Add(openCmdBinding);

            // Creating a KeyBinding between the Save command and Ctrl-S
            KeyBinding saveCmdKeyBinding = new KeyBinding(ApplicationCommands.Save, Key.S, ModifierKeys.Control);
            this.InputBindings.Add(saveCmdKeyBinding);
            CommandBinding saveCmdBinding = new CommandBinding(ApplicationCommands.Save, SaveCmdExecuted);
            this.CommandBindings.Add(saveCmdBinding);

            // Creating a KeyBinding between the New command and Ctrl-N
            KeyBinding newCmdKeyBinding = new KeyBinding(ApplicationCommands.New, Key.N, ModifierKeys.Control);
            this.InputBindings.Add(saveCmdKeyBinding);
            CommandBinding newCmdBinding = new CommandBinding(ApplicationCommands.New, NewCmdExecuted);
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
            // ADD SOME CODE HERE TO MAKE SURE YOU DON'T DELETE UNSAVED DATA!
            Window1.manager = new SeatManager();

            // Bind all of the GUI elements
            lbxRooms.ItemsSource = Window1.manager.RoomList;
            lbxRoster.ItemsSource = Window1.manager.StudentList;
            
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
                lbxRooms.ItemsSource = Window1.manager.RoomList;
                lbxRoster.ItemsSource = Window1.manager.StudentList;
            }
        }

        private void SaveCmdExecuted(object sender, RoutedEventArgs e)
        {
            // If we can save the file where it already was we will, otherwise prompt to save the file
            if (!Window1.manager.SaveXml())
            {
                FileMenuSaveAs_Click(sender, e);
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
            MessageBox.Show("Exit not implemented");
        }

        private void ButtonAddStudent_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd student = new StudentAdd();
            student.ShowDialog();
        }

        private void ButtonEditStudent_Click(object sender, RoutedEventArgs e)
        {
            if (lbxRoster.SelectedValue != null)
            {
                StudentAdd student = new StudentAdd(lbxRoster.SelectedValue as Student);
                student.ShowDialog();
            }
        }

        private void ButtonEditRoom_Click(object sender, RoutedEventArgs e)
        {
            if (lbxRooms.SelectedValue != null)
            {
                frmGrid grid = new frmGrid(lbxRooms.SelectedValue as Room, true);
                grid.ShowDialog();
            }
        }
        private void btnPlace_Click(object sender, RoutedEventArgs e)
        {
            frmGrid grid = new frmGrid(lbxRooms.SelectedValue as Room, false);
            grid.ShowDialog();
        }
        private void DeleteStudent(object sender, RoutedEventArgs e)
        {
            // NOT IMPLEMENTED YET
            /*
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                manager. create a remove student and remove room function
            }
             */
        }

        private void ImportRoster_Click(object sender, RoutedEventArgs e)
        {
            RosterImporter ri = new RosterImporter();
            if (ri.IsOpened)
            {
                ri.ShowDialog();
            }
        }

        private void buttonAddStudentsToRoom_Click(object sender, RoutedEventArgs e)
        {
            if ((Room)lbxRooms.SelectedItem == null)
            {
                MessageBox.Show("No room selected");
            }
            else
            {
                Room room = (Room)lbxRooms.SelectedItem;
                IList list = lbxRoster.SelectedItems;
                for (int i = 0; i < list.Count; i++)
                {
                    Student s = (Student)list[i];
                    room.AddStudent(s);
                }
            }

        }

        private void buttonAddSectionToRoom_Click(object sender, RoutedEventArgs e)
        {
            // NOT IMPLEMENTED YET
        }

        private void lbxRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room room = (Room)lbxRooms.SelectedItem;
            if (room != null)
            {
                lbxStudents.ItemsSource = room.RoomStudents;
            }
            else
            {
                lbxStudents.ItemsSource = null;
            }

        }
    }
}
