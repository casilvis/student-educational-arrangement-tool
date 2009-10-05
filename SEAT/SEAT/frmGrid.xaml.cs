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
        public Seat[,] stArray;
        public TextBox[] txtcol;
        public TextBox[] txtrow;
        public int row;
        public int column;
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
            row = rows;
            column = columns;
            string answer = row + " by " + column;
      //      lblTest.Content = answer;
            ContainerVisual newPage = new ContainerVisual();
            stArray = new Seat[row,column];
            txtcol = new TextBox[column];
            txtrow = new TextBox[row];

            griddy.VerticalAlignment = VerticalAlignment.Top; 
            for (int i=0; i < row; i++)
            {           
                Button button1 = new Button();
                button1.FontSize = 10;
                button1.Content = "Select";
                button1.AddHandler(Button.ClickEvent, new RoutedEventHandler(rowbutton_click));
                button1.VerticalAlignment = VerticalAlignment.Top;
                button1.HorizontalAlignment = HorizontalAlignment.Left;
                button1.Width = seatSize;
                button1.Height = seatSize;
                button1.Tag = i;
                button1.Margin = new Thickness(seatSize, seatSize * i , 0, 0);
                grdleft.Children.Add(button1);

                txtrow[i] = new TextBox();
                txtrow[i].FontSize = 10;
                txtrow[i].VerticalAlignment = VerticalAlignment.Top;
                txtrow[i].HorizontalAlignment = HorizontalAlignment.Left;
                txtrow[i].Margin = new Thickness(0,seatSize * i, 0, 0);
                txtrow[i].Width = seatSize;
                txtrow[i].Height = seatSize;
                grdleft.Children.Add(txtrow[i]);
                for (int j=0; j < column; j++)
                {
                    Button button2 = new Button();
                    button2.FontSize = 10;
                    button2.Content = "Select";
                    button2.AddHandler(Button.ClickEvent, new RoutedEventHandler(columnbutton_click));
                    button2.VerticalAlignment = VerticalAlignment.Top;
                    button2.HorizontalAlignment = HorizontalAlignment.Left;
                    button2.Margin = new Thickness(seatSize * j ,seatSize, 0, 0);
                    button2.Width = seatSize;
                    button2.Height = seatSize;
                    button2.Tag = j;  
                    grdtop2.Children.Add(button2);

                    txtcol[j] = new TextBox();
                    txtcol[j].FontSize = 10;
                    txtcol[j].VerticalAlignment = VerticalAlignment.Top;
                    txtcol[j].HorizontalAlignment = HorizontalAlignment.Left;
                    txtcol[j].Margin = new Thickness(seatSize * j , 0, 0, 0);
                    txtcol[j].Width = seatSize;
                    txtcol[j].Height = seatSize;
                    grdtop2.Children.Add(txtcol[j]);

                    stArray[i, j] = new Seat();   
                    stArray[i,j].Margin = new Thickness(seatSize*j,seatSize*i,0,0);
                    griddy.Children.Add(stArray[i,j]);
                }
            }            
            Label lblspace = new Label();//used as a pad for the scroll bar
            Label lblspace2 = new Label();//
            lblspace.Width = lblspace2.Width = 20;//
            lblspace.Height = lblspace2.Height = 20;//
            lblspace.Content = lblspace2.Content = "";//
            lblspace.Margin = new Thickness(0, seatSize * row, 0, 0);//
            grdleft.Children.Add(lblspace);//
            lblspace2.Margin = new Thickness(seatSize * column+seatSize,0, 0, 0);//
            grdtop2.Children.Add(lblspace2);//
            Button button3 = new Button();
            button3.FontSize = 10;
            TextBlock select = new TextBlock();
            select.Text = "Select All";
            select.TextWrapping = TextWrapping.Wrap;
            button3.Content = select;
            button3.AddHandler(Button.ClickEvent, new RoutedEventHandler(allbutton_click));
            button3.VerticalAlignment = VerticalAlignment.Top;
            button3.HorizontalAlignment = HorizontalAlignment.Left;
            button3.Margin = new Thickness(45, 45, 0, 0);
            button3.Width = seatSize;
            button3.Height = seatSize;
            button3.Tag = column+" "+row;
            grdtop.Children.Add(button3);
            TextBlock naming = new TextBlock();
            naming.TextWrapping = TextWrapping.Wrap;
            naming.Text = "Name entire column or row";
            naming.VerticalAlignment = VerticalAlignment.Top;
            naming.HorizontalAlignment = HorizontalAlignment.Left;
            naming.Margin= new Thickness(0,0,0,0);
            naming.Width = 90;
            grdtop.Children.Add(naming);

            griddy.MaxHeight= seatSize*row+20;
            griddy.MaxWidth=seatSize*column+20;
            svrgrid.AddHandler(ScrollViewer.ScrollChangedEvent, new RoutedEventHandler(svrgrid_ScrollChanged));
            svrtop.AddHandler(ScrollViewer.ScrollChangedEvent, new RoutedEventHandler(svrtop_ScrollChanged));
            svrleft.AddHandler(ScrollViewer.ScrollChangedEvent, new RoutedEventHandler(svrleft_ScrollChanged));
            this.Show();
        }
        private void svrgrid_ScrollChanged(object sender, EventArgs e)
        {
                svrtop.ScrollToHorizontalOffset(svrgrid.HorizontalOffset);
                svrleft.ScrollToVerticalOffset(svrgrid.VerticalOffset);
            
        }
        private void svrtop_ScrollChanged(object sender, EventArgs e)
        {
            svrgrid.ScrollToHorizontalOffset(svrtop.HorizontalOffset);
        }
        private void svrleft_ScrollChanged(object sender, EventArgs e)
        {
            svrgrid.ScrollToVerticalOffset(svrleft.VerticalOffset);
        }
        private void columnbutton_click(object sender, EventArgs e)
        {
            Button button1= (Button)sender;
            for (int i = 0; i < row; i++)
            {
                if ((string)button1.Content == "Select")
                    stArray[i, (int)button1.Tag].chkSelected.IsChecked = true;
                else
                    stArray[i, (int)button1.Tag].chkSelected.IsChecked = false;
            }
            if ((string)button1.Content == "Select")
                button1.Content = "Unselect";
            else
                button1.Content = "Select";
        }
        private void rowbutton_click(object sender, EventArgs e)
        {
            Button button2 = (Button)sender;
            for (int j=0; j < column; j++)
            {
                if ((string)button2.Content == "Select")
                    stArray[(int)button2.Tag,j].chkSelected.IsChecked = true;
                else
                    stArray[(int)button2.Tag,j].chkSelected.IsChecked = false;
            }
            if ((string)button2.Content == "Select")
                button2.Content = "Unselect";
            else
                button2.Content = "Select";
        }
        private void allbutton_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            TextBlock block =(TextBlock)button.Content;
            string numbers = (string)button.Tag;
            int middle;
            middle = numbers.IndexOf(' ');
            int column = Convert.ToInt32(numbers.Substring(0, middle));
            int row = Convert.ToInt32(numbers.Substring(middle));
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (block.Text == "Select All")
                        stArray[i, j].chkSelected.IsChecked = true;
                    else
                        stArray[i, j].chkSelected.IsChecked = false;
                }
            }
            if (block.Text == "Select All")
                block.Text = "Unselect All";
            else
                block.Text = "Select All";
        }
    }
}
