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

namespace SEAT
{
    /// <summary>
    /// Interaction logic for Seat.xaml
    /// </summary>
    public partial class Seat : UserControl
    {
        public Seat()
        {
            InitializeComponent();
            Height = 70;
            Width = 180;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
