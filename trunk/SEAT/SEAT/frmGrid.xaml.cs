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
            InitializeComponent();
            int row = rows;
            int column = columns;
            string answer = row + " by " + column;
            lblTest.Content = answer;
            ContainerVisual newPage = new ContainerVisual();
            Seat[,] stArray = new Seat[row,column];
            int x=0;
            int y=0;
            Width = column * 180;
            Height = row * 70;
            griddy.VerticalAlignment = VerticalAlignment.Top;
            for (int i=0; i < row; i++)
            {
                for (int j=0; j < column; j++)
                {
                    stArray[i, j] = new Seat();
                    
                    x = (x + 180) % (180*column);
                    y = (y + 70) % (70*row);
                    //labels.Content = answer;
                 //   griddy.
                    //left, top,0,0
                    stArray[i,j].Margin = new Thickness(x,y,0,0);
                    griddy.Children.Add(stArray[i, j]);
                }
            }
            
            this.Content = griddy;
            
            this.Show();
        }
    }
}
