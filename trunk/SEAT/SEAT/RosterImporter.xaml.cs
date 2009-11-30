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
    /// <summary>
    /// Interaction logic for RosterImporter.xaml
    /// </summary>
    public partial class RosterImporter : Window
    {
        private RosterFile rosterFile;
        private Boolean isOpened;

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
                this.isOpened = true;
                fillGrid();
            }
            else
            {
                this.isOpened = false;
                this.Close();
            }
        }

        public Boolean IsOpened{
            get{return this.isOpened;}
        }

        private void fillGrid()
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
            for (int i = 0; i < this.rosterFile.Count; i++)
            {
                string[] cols = this.rosterFile.ParsedData[i];

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
