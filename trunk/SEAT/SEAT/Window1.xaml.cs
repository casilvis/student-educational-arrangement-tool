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
            
        }

        private void btnaddroom_Click(object sender, RoutedEventArgs e)
        {
            ClassOpen classroom = new ClassOpen();
            classroom.Show();
        }

        private void btnaddstudent_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd student = new StudentAdd();
            student.Show();
        }

        private void FileMenuNew_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("New not implemented");
        }

        private void FileMenuOpen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Open not implemented");
        }

        private void FileMenuSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save not implemented");
        }

        private void FileMenuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save as not implemented");
        }

        private void FileMenuExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exit not implemented");
        }

        
    }
}
