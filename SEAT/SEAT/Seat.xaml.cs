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

        public Seat(Chair chare,bool editable)
        {
            mychair = chare;
            InitializeComponent();
            if (editable)
            {
                for (int i = 0; i < 9; i++)
                {
                    int x = (i % 3) * 14;
                    int y = (i / 3) * 14;
                    Rectangle rect01 = new Rectangle();
                    rect01.Width = 15;
                    rect01.Height = 15;
                    rect01.Stroke = Brushes.Gray;
                    rect01.HorizontalAlignment = HorizontalAlignment.Left;
                    rect01.VerticalAlignment = VerticalAlignment.Top;
                    rect01.Margin = new Thickness(x, y, 0, 0);
                    grdSelect.Children.Add(rect01);
                }
            }
            if (chair.LeftHanded)
                lblName.Foreground = Brushes.Red;
            if (chair.NonChair || chair.MustBeEmpty)
            {
                if (chair.NonChair)
                {
                    this.Background = Brushes.Gray;
                }
                else
                {
                    this.Background = Brushes.LightGray;
                }
            }
            else
            {
                this.Background = Brushes.White;
            }
            chkSelected.Margin = new Thickness(chair.LrPosition * 15, chair.FbPosition * 15, 0, 0);
            if(!editable)
            {
                chkSelected.Visibility=Visibility.Hidden;
                this.AddHandler(UserControl.MouseDoubleClickEvent, new RoutedEventHandler(Seat_click));
            }
            lblName.Content = chair.SeatName;
            

            
        }
        public Chair chair { get { return mychair; } }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
        private void Seat_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hey");
        }
    }
}
