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
            
            Width = column * 180;
            Height = row * 70;
            Canvas myParentCanvas = new Canvas();
            myParentCanvas.Width = Width;
            myParentCanvas.Height = Height;
            for (int i=0; i < row; i++)
            {
                for (int j=0; j < column; j++)
                {
                    stArray[i, j] = new Seat();
                    //labels.Content = answer;
                    myParentCanvas.Children.Add(stArray[i,j]);
                       
                }
            }
            this.Content = myParentCanvas;
            this.Show();
        }
    }
}
