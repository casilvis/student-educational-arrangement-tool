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

namespace SEAT
{
    /// <summary>
    /// Interaction logic for frmGrid.xaml
    /// </summary>
    public partial class frmGrid : Window
    {
        public frmGrid()
        {
            InitializeComponent();
            int row = 0;
            int column = 0;
            Seat[,] stArray = new Seat[row, column];
        }
        public frmGrid(int rows, int columns)
        {
            int seatSize = 45;
            InitializeComponent();
            int row = rows;
            int column = columns;
            string answer = row + " by " + column;
            lblTest.Content = answer;
            ContainerVisual newPage = new ContainerVisual();
            Seat[,] stArray = new Seat[row,column];
            griddy.VerticalAlignment = VerticalAlignment.Top;
            for (int i=0; i < row; i++)
            {
                Button button1 = new Button();
                button1.AddHandler(Button.ClickEvent, new RoutedEventHandler(columnbutton_click));
                button1.Margin=new Thickness(i * seatSize+seatSize, 0, 0, 0);
                button1.VerticalAlignment = VerticalAlignment.Top;
                button1.HorizontalAlignment = HorizontalAlignment.Left;
                button1.Width = seatSize;
                button1.Height = seatSize;
                button1.Tag = i;
                griddy.Children.Add(button1);
                for (int j=0; j < column; j++)
                {
                    Button button2 = new Button();
                    button2.AddHandler(Button.ClickEvent, new RoutedEventHandler(rowbutton_click));
                    button2.Margin = new Thickness(0,j * seatSize + seatSize, 0, 0);
                    button2.VerticalAlignment = VerticalAlignment.Top;
                    button2.HorizontalAlignment = HorizontalAlignment.Left;
                    button2.Width = seatSize;
                    button2.Height = seatSize;
                    button2.Tag = j;
                    griddy.Children.Add(button2);
                    stArray[i, j] = new Seat();
                    stArray[i, j].Margin = new Thickness(i * seatSize + seatSize, j * seatSize + seatSize, 0, 0);
                    griddy.Children.Add(stArray[i, j]);
                }
            }
            
            this.Content = griddy;

            this.Height = seatSize *(column+2);
            this.Width = seatSize * (row+2);
            this.Show();
        }
        private void rowbutton_click(object sender, EventArgs e)
        {
            Button button1= (Button)sender;
            MessageBox.Show("Row " +button1.Tag);
        }
        private void columnbutton_click(object sender, EventArgs e)
        {
            Button button2 = (Button)sender;
            MessageBox.Show("Column " + button2.Tag);
        }
    }
}
