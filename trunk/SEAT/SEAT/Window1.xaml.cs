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
using System.Text.RegularExpressions;


namespace SEAT
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string row = Regex.Replace(txtRows.Text, @"[\D]", "");
            string column = Regex.Replace(txtColumns.Text, @"[\D]", "");
            txtRows.Text = row;
            txtColumns.Text = column;
            if (column.Length > 0 && row.Length > 0)
            {
                frmGrid Grid = new frmGrid(Convert.ToInt32(row), Convert.ToInt32(column));
                Grid.Show();
            }
        }
    }
}
