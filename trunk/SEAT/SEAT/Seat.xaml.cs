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
    /// Interaction logic for Seat.xaml
    /// </summary>
    public partial class Seat : UserControl
    {
        private Chair mychair;
        public Seat()
        {
            mychair = new Chair();
            InitializeComponent();
        }

        public Seat(Chair chare)
        {
            mychair = chare;
            InitializeComponent();
            if (chair.LeftHanded)
                lblName.Foreground=Brushes.Green;
            
        }
        public Chair chair { get { return mychair; } }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
