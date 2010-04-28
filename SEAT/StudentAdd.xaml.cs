// <copyright file="StudentAdd.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Add or updated a students information.</summary>
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

    /// <summary>
    /// Add or update a students information.
    /// </summary>
    public partial class StudentAdd : Window
    {
        /// <summary>
        /// An instance of the student who is being edited.
        /// </summary>
        private Student student;

        /// <summary>
        /// Initializes a new instance of the StudentAdd class.
        /// Used when adding a student.
        /// </summary>
        public StudentAdd()
        {
            InitializeComponent();
            this.student = null;
            txtLName.Focus();
        }

        /// <summary>
        /// Initializes a new instance of the StudentAdd class.
        /// Used when editing a student.
        /// </summary>
        /// <param name="student">The student to be edited.</param>
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

        /// <summary>
        /// Add the student or update the instance of the student.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
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
