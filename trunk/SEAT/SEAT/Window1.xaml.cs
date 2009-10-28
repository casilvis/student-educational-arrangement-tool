using System;
using System.Collections.Generic;
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


namespace SEAT
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public static SeatManager manager = new SeatManager();
        public Window1()
        {
            InitializeComponent();
            this.Title = "SEAT Manager";
            lbxRooms.ItemsSource = Window1.manager.RoomList;
            lbxRoster.ItemsSource = Window1.manager.StudentList;
        }

        private void btnaddroom_Click(object sender, RoutedEventArgs e)
        {
            ClassOpen classroom = new ClassOpen();
            classroom.ShowDialog();
        }

        private void FileMenuNew_Click(object sender, RoutedEventArgs e)
        {
            Window1.manager = new SeatManager();
        }

        private void FileMenuOpen_Click(object sender, RoutedEventArgs e)
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
                lbxRooms.ItemsSource = Window1.manager.RoomList;
                lbxRoster.ItemsSource = Window1.manager.StudentList;
            }
        }

        private void FileMenuSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save not implemented");
        }

        private void FileMenuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
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

        private void btnaddstudent_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd student = new StudentAdd();
            student.ShowDialog();
        }

        private void btneditstudent_Click(object sender, RoutedEventArgs e)
        {
            if (lbxRoster.SelectedValue != null)
            {
                StudentAdd student = new StudentAdd(lbxRoster.SelectedValue as Student);
                student.ShowDialog();
            }
        }
        private void btneditroom_Click(object sender, RoutedEventArgs e)
        {
            if (lbxRooms.SelectedValue != null)
            {
                frmGrid Grid = new frmGrid(lbxRooms.SelectedValue as Room, true);
                Grid.ShowDialog();
            }
        }
        private void deletestudent(object sender, RoutedEventArgs e)
        {
          //  if (e.Key == System.Windows.Input.Key.Delete)
            {
        //        manager. create a remove student and remove room function
            }
        }
    }
}
