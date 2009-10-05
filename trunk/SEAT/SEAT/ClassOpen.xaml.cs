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
using System.Text.RegularExpressions;
using SEATLibrary;


namespace SEAT
{
    /// <summary>
    /// Interaction logic for ClassOpen.xaml
    /// </summary>
    public partial class ClassOpen : Window
    {
        public ClassOpen()
        {
            InitializeComponent();
        }
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            
            if ((bool)rbtncreate.IsChecked)
            {
                string row = Regex.Replace(txtRows.Text, @"[\D]", "");
                string column = Regex.Replace(txtColumns.Text, @"[\D]", "");
                txtRows.Text = row;
                txtColumns.Text = column;
                if (column.Length > 0 && row.Length > 0)
                {
                    Room classroom = new Room(Convert.ToInt32(row), Convert.ToInt32(column));
                    Window1.manager.addNewRoom(classroom);
                    frmGrid Grid = new frmGrid(classroom);
                    Grid.Show();
                    this.Close();
                }
            }
        }
    }
}
