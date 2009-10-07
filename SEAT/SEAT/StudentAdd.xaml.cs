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
using System.Windows.Shapes;
using SEATLibrary;

namespace SEAT
{
    ///
    /// <summary>
    /// Interaction logic for StudentAdd.xaml
    /// </summary>
    public partial class StudentAdd : Window
    {
        public StudentAdd()
        {
            InitializeComponent();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtSID.Text.Length > 0 && txtLName.Text.Length > 0 && txtFName.Text.Length > 0)
            {
                Student student = new Student(txtFName.Text, txtLName.Text, txtSID.Text, 
                    txtSection.Text, (bool)rbtnLeft.IsChecked, (bool)chkbxvision.IsChecked);
                Window1.manager.addStudentToRoster(student);
                this.Close();
            }
            else
                MessageBox.Show("make sure to set all values");
        }
    }
}
