// <copyright file="RosterImporter.xaml.cs" company="University of Louisville Speed School of Engineering">
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

    /// <summary>
    /// Interaction logic for RosterImporter.xaml
    /// </summary>
    public partial class RosterImporter : Window
    {
        /// <summary>
        /// The parsing engine that is part of the model.
        /// </summary>
        private RosterFile rosterFile;

        /// <summary>
        /// Flag that indicates if the file was successfully opened.
        /// </summary>
        private bool isOpened;

        /// <summary>
        /// Initializes a new instance of the RosterImporter class.
        /// </summary>
        public RosterImporter()
        {
            InitializeComponent();

            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "Roster File (.csv)|*.csv"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                this.rosterFile = new RosterFile(dlg.FileName);
                if (this.rosterFile.Count == 0)
                {
                    MessageBox.Show("Unable to import roster.  Check if file is still in use.");
                }

                this.isOpened = true;
                this.FillGrid();
            }
            else
            {
                this.isOpened = false;
                this.Close();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the file was opened.
        /// </summary>
        public bool IsOpened
        {
            get { return this.isOpened; }
        }

        /// <summary>
        /// Fills the grid on the screen with the information contained in the CSV file.
        /// </summary>
        private void FillGrid()
        {
            // Add all of the necessary columns
            for (int i = 0; i < this.rosterFile.NumColumns; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                myGrid.ColumnDefinitions.Add(cd);
            }

            // Add all of the necessary rows
            for (int i = 0; i < this.rosterFile.Count; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(20);
                myGrid.RowDefinitions.Add(rd);
            }

            // Add all of the data into the grid
            for (int i = 1; i < this.rosterFile.Count; i++)
            {
                string[] cols = this.rosterFile.ParsedData[i];
                Student student = new Student(cols[2], cols[1], cols[0], cols[4], false, false);
                Window1.SManager.AddStudentToRoster(student);
                for (int j = 0; j < cols.Length; j++)
                {
                    TextBlock cell = new TextBlock();
                    cell.Text = cols[j];
                    cell.FontSize = 11;
                    cell.VerticalAlignment = VerticalAlignment.Top;
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);

                    myGrid.Children.Add(cell);
                }
            }
        }
    }
}
