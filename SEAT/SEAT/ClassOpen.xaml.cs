// <copyright file="ClassOpen.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEAT
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
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
    /// Interaction logic for ClassOpen.xaml
    /// </summary>
    public partial class ClassOpen : Window
    {
        /// <summary>
        /// Initializes a new instance of the ClassOpen class.
        /// </summary>
        public ClassOpen()
        {
            InitializeComponent();
            this.Title = "Room creator";
        }

        /// <summary>
        /// Create the new room and launch the room editing window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)rbtncreate.IsChecked)
            {
                string row = Regex.Replace(txtRows.Text, @"[\D]", string.Empty);
                string column = Regex.Replace(txtColumns.Text, @"[\D]", string.Empty);
                txtRows.Text = row;
                txtColumns.Text = column;
                if (column.Length > 0 && row.Length > 0)
                {
                    Room classroom = new Room(Convert.ToInt32(row), Convert.ToInt32(column));
                    Window1.SManager.AddNewRoom(classroom);
                    frmGrid grid = new frmGrid(classroom, true);
                    grid.Show();
                    this.Close();
                }
            }
            else
            {
                try
                {
                    if (txtPath.Text != string.Empty)
                    {
                        Room classroom = new Room(txtPath.Text);
                        Window1.SManager.AddNewRoom(classroom);
                        frmGrid grid = new frmGrid(classroom, true);
                        grid.Show();
                        this.Close();
                    }
                }
                catch (Exception a)
                {
                    MessageBox.Show("Invalid path " + a.ToString());
                }
            }
        }

        /// <summary>
        /// Provide the user with a prompt for selecting a template file.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonBrowse_Click(object sender, RoutedEventArgs e)
        {
            rbtnLoad.IsChecked = true;

            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".tplt"; // Default file extension
            dlg.Filter = "Room File (.tplt)|*.tplt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                txtPath.Text = dlg.FileName;
            }
        }
    }
}
