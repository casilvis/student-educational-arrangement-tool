// <copyright file="StudentAdd.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEAT
{
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

    ///
    /// <summary>
    /// Interaction logic for StudentAdd.xaml
    /// </summary>
    public partial class StudentAdd : Window
    {
        private Student student;

        public StudentAdd()
        {
            InitializeComponent();
            this.student = null;
            txtLName.Focus();
        }

        public StudentAdd(Student student)
        {
            InitializeComponent();
            this.student = student;
            txtFName.Text = student.FirstName;
            txtLName.Text = student.LastName;
            txtSID.Text = student.Sid;
            txtSection.Text = student.Section;
            if (student.LeftHanded)
            {
                rbtnLeft.IsChecked = true;
            }
            else
            {
                rbtnRight.IsChecked = true;
            }

            if (student.VisionImpairment)
            {
                chkbxvision.IsChecked = true;
            }

            txtLName.Focus();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtSID.Text.Length > 0 && txtLName.Text.Length > 0 && txtFName.Text.Length > 0)
            {
                if (this.student == null)
                {
                    this.student = new Student(
                        txtFName.Text, 
                        txtLName.Text, 
                        txtSID.Text,
                        txtSection.Text, 
                        (bool)rbtnLeft.IsChecked, 
                        (bool)chkbxvision.IsChecked);
                    Window1.SManager.AddStudentToRoster(this.student);
                }
                else
                {
                    this.student.FirstName = txtFName.Text;
                    this.student.LastName = txtLName.Text;
                    this.student.Sid = txtSID.Text;
                    this.student.Section = txtSection.Text;
                    this.student.LeftHanded = (bool)rbtnLeft.IsChecked;
                    this.student.VisionImpairment = (bool)chkbxvision.IsChecked;
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("make sure to set all values");
            }
        }
    }
}
