// <copyright file="frmGrid.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEAT
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Printing;
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
    using SEATLibrary.Assignment_Algorithm;
    using SEATLibrary.Reservation_Algorithm;

    /// <summary>
    /// Interaction logic for frmGrid.xaml
    /// </summary>
    public partial class frmGrid : Window
    {
        /// <summary>
        /// The object that represents the underlying room.
        /// </summary>
        private Room myroom;

        /// <summary>
        /// The array of Seats that represents the room.
        /// </summary>
        private Seat[,] seatArray;

        /// <summary>
        /// The text boxes that represent column names.
        /// </summary>
        private TextBox[] txtcol;

        /// <summary>
        /// The text boxes that represent row names.
        /// </summary>
        private TextBox[] txtrow;

        /// <summary>
        /// The number of rows in the room.
        /// </summary>
        private int row;

        /// <summary>
        /// The number of columns in the room.
        /// </summary>
        private int column;

        /// <summary>
        /// A list of all of the selected seats.
        /// </summary>
        private List<Seat> seatsSelected;

        /// <summary>
        /// Initializes a new instance of the frmGrid class.
        /// </summary>
        public frmGrid()
        {
            // Shouldn't be used but initialized in case
            InitializeComponent();
            int row = 0;
            int column = 0;
            this.seatArray = new Seat[row, column];
        }

        /// <summary>
        /// Initializes a new instance of the frmGrid class.
        /// </summary>
        /// <param name="inroom">The room to be rendered.</param>
        /// <param name="editable">Is the room editible.</param>
        public frmGrid(Room inroom, bool editable)
        {
            // Setting up the room to correct size and properties
            InitializeComponent();
            this.myroom = inroom;
            this.row = this.myroom.Height;
            this.column = this.myroom.Width;

            this.seatsSelected = new List<Seat>();
            int width = 45;
            int height = 60;
            txtloc.Text = this.myroom.Location;
            txtnm.Text = this.myroom.RoomName;
            txtdes.Text = this.myroom.Description;
            this.Title = this.myroom.RoomName + " - " + this.myroom.Location + ": " + this.myroom.Description;
            if (!editable)
            {
                Options.Width = 0;
            }
            else
            {
                // Disable all of the placement algorithms when editing the room.
                this.algorithmBestEffort.IsEnabled = false;
                this.algorithmClearSeats.IsEnabled = false;
                this.algorithmLeftHanded.IsEnabled = false;
                this.algorithmVisuallyImpaired.IsEnabled = false;

                // Hide the Student ListBox UI elements
                this.dockPanelStudents.Visibility = Visibility.Hidden;
                this.dockPanelStudents.Width = 0;
            }

            ContainerVisual newPage = new ContainerVisual();
            this.seatArray = new Seat[this.row, this.column];
            this.txtcol = new TextBox[this.column];
            this.txtrow = new TextBox[this.row];

            griddy.VerticalAlignment = VerticalAlignment.Top;
            for (int i = 0; i < this.row; i++)
            {
                if (editable)
                {
                    // This sets up the row select buttons and row text boxes
                    Button button1 = new Button();
                    button1.FontSize = 10;
                    button1.Content = "Select";
                    button1.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.ButtonRow_Click));
                    button1.VerticalAlignment = VerticalAlignment.Top;
                    button1.HorizontalAlignment = HorizontalAlignment.Left;
                    button1.Width = width;
                    button1.Height = height;
                    button1.Tag = i;
                    button1.Margin = new Thickness(width, height * i, 0, 0);
                    grdleft.Children.Add(button1);

                    this.txtrow[i] = new TextBox();
                    this.txtrow[i].FontSize = 10;
                    this.txtrow[i].VerticalAlignment = VerticalAlignment.Top;
                    this.txtrow[i].HorizontalAlignment = HorizontalAlignment.Left;
                    this.txtrow[i].Margin = new Thickness(0, height * i, 0, 0);
                    this.txtrow[i].Width = width;
                    this.txtrow[i].Height = height;
                    grdleft.Children.Add(this.txtrow[i]);
                }

                for (int j = 0; j < this.column; j++)
                {
                    if (editable)
                    {   // this sets up the column select buttons and text boxes
                        Button button2 = new Button();
                        button2.FontSize = 10;
                        button2.Content = "Select";
                        button2.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.ButtonColumn_Click));
                        button2.VerticalAlignment = VerticalAlignment.Top;
                        button2.HorizontalAlignment = HorizontalAlignment.Left;
                        button2.Margin = new Thickness(width * j, height, 0, 0);
                        button2.Width = width;
                        button2.Height = height;
                        button2.Tag = j;
                        grdtop2.Children.Add(button2);

                        this.txtcol[j] = new TextBox();
                        this.txtcol[j].FontSize = 10;
                        this.txtcol[j].VerticalAlignment = VerticalAlignment.Top;
                        this.txtcol[j].HorizontalAlignment = HorizontalAlignment.Left;
                        this.txtcol[j].Margin = new Thickness(width * j, 0, 0, 0);
                        this.txtcol[j].Width = width;
                        this.txtcol[j].Height = height;
                        grdtop2.Children.Add(this.txtcol[j]);
                    }

                    // this sets up the actual seats in the room
                    this.seatArray[i, j] = new Seat(this.myroom.Chairs[i, j], editable);
                    this.seatArray[i, j].AddHandler(UserControl.MouseLeftButtonUpEvent, new RoutedEventHandler(this.Student_Drop));
                    this.seatArray[i, j].AddHandler(CheckBox.CheckedEvent, new RoutedEventHandler(this.CheckBoxSelected_Checked));
                    this.seatArray[i, j].AddHandler(CheckBox.UncheckedEvent, new RoutedEventHandler(this.CheckBoxSelected_Unchecked));
                    this.seatArray[i, j].Margin = new Thickness(width * j, height * i, 0, 0);
                    griddy.Children.Add(this.seatArray[i, j]);
                }
            }

            if (editable)
            {   // this is all the stuff in the top and left of the room when editable
                Label lblspace = new Label(); // used as a pad for the scroll bar
                Label lblspace2 = new Label();
                lblspace.Width = lblspace2.Width = 20;
                lblspace.Height = lblspace2.Height = 20;
                lblspace.Content = lblspace2.Content = string.Empty;
                lblspace.Margin = new Thickness(0, height * this.row, 0, 0);
                grdleft.Children.Add(lblspace);
                lblspace2.Margin = new Thickness((width * this.column) + width, 0, 0, 0);
                grdtop2.Children.Add(lblspace2);
                Button button3 = new Button();
                button3.FontSize = 10;
                TextBlock select = new TextBlock();
                select.Text = "Select All";
                select.TextWrapping = TextWrapping.Wrap;
                button3.Content = select;
                button3.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.ButtonAll_Click));
                button3.VerticalAlignment = VerticalAlignment.Top;
                button3.HorizontalAlignment = HorizontalAlignment.Left;
                button3.Margin = new Thickness(width, height, 0, 0);
                button3.Width = width;
                button3.Height = height;
                button3.Tag = this.column + " " + this.row;
                grdtop.Children.Add(button3);

                Button btnRbyC = new Button();
                btnRbyC.Content = "R by C";
                btnRbyC.FontSize = 10;
                btnRbyC.VerticalAlignment = VerticalAlignment.Top;
                btnRbyC.HorizontalAlignment = HorizontalAlignment.Left;
                btnRbyC.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.RowByColumn_click));
                btnRbyC.Margin = new Thickness(0, 0, 0, 0);
                btnRbyC.Width = width;
                btnRbyC.Height = height / 2;
                grdtop.Children.Add(btnRbyC);

                Button btnCbyR = new Button();
                btnCbyR.Content = "C by R";
                btnCbyR.FontSize = 10;
                btnCbyR.VerticalAlignment = VerticalAlignment.Top;
                btnCbyR.HorizontalAlignment = HorizontalAlignment.Left;
                btnCbyR.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.ColumnByRow_click));
                btnCbyR.Margin = new Thickness(0, height / 2, 0, 0);
                btnCbyR.Width = width;
                btnCbyR.Height = height / 2;
                grdtop.Children.Add(btnCbyR);

                TextBlock naming = new TextBlock();
                naming.TextWrapping = TextWrapping.Wrap;
                naming.Text = "Name rows";
                naming.Width = width;
                naming.VerticalAlignment = VerticalAlignment.Top;
                naming.HorizontalAlignment = HorizontalAlignment.Left;
                naming.Margin = new Thickness(0, height, 0, 0);
                grdtop.Children.Add(naming);

                TextBlock naming2 = new TextBlock();
                naming2.TextWrapping = TextWrapping.Wrap;
                naming2.Text = "Name columns";
                naming2.Width = width;
                naming2.VerticalAlignment = VerticalAlignment.Top;
                naming2.HorizontalAlignment = HorizontalAlignment.Left;
                naming2.Margin = new Thickness(width, 0, 0, 0);
                grdtop.Children.Add(naming2);

                svrgrid.AddHandler(ScrollViewer.ScrollChangedEvent, new RoutedEventHandler(this.ScrollGrid_ScrollChanged));
                svrtop.AddHandler(ScrollViewer.ScrollChangedEvent, new RoutedEventHandler(this.ScrollTop_ScrollChanged));
                svrleft.AddHandler(ScrollViewer.ScrollChangedEvent, new RoutedEventHandler(this.ScrollLeft_ScrollChanged));
                grdopt.Height = 0;
            }
            else
            {
                grdopt.Width = this.Width;
                this.students.ItemsSource = this.myroom.RoomStudents;
            }

            griddy.MaxHeight = (height * this.row) + 20;
            griddy.MaxWidth = (width * this.column) + 20;
        }

        /// <summary>
        /// Makes the select buttons stay with their rows and columns respectively.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ScrollGrid_ScrollChanged(object sender, EventArgs e)
        {
            svrtop.ScrollToHorizontalOffset(svrgrid.HorizontalOffset);
            svrleft.ScrollToVerticalOffset(svrgrid.VerticalOffset);
        }

        /// <summary>
        /// If the select columns buttons shift shift the grid.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ScrollTop_ScrollChanged(object sender, EventArgs e)
        {
            svrgrid.ScrollToHorizontalOffset(svrtop.HorizontalOffset);
        }

        /// <summary>
        /// If the select rows buttons shift shift the grid.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ScrollLeft_ScrollChanged(object sender, EventArgs e)
        {
            svrgrid.ScrollToVerticalOffset(svrleft.VerticalOffset);
        }

        /// <summary>
        /// If a column select is chosen select/unselect that column.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonColumn_Click(object sender, EventArgs e)
        {
            Button button1 = (Button)sender;
            for (int i = 0; i < this.row; i++)
            {
                if ((string)button1.Content == "Select")
                {
                    if (!(bool)this.seatArray[i, (int)button1.Tag].chkSelected.IsChecked)
                    {
                        this.seatArray[i, (int)button1.Tag].chkSelected.IsChecked = true;
                    }
                }
                else
                {
                    if ((bool)this.seatArray[i, (int)button1.Tag].chkSelected.IsChecked)
                    {
                        this.seatArray[i, (int)button1.Tag].chkSelected.IsChecked = false;
                    }
                }
            }

            if ((string)button1.Content == "Select")
            {
                button1.Content = "Unselect";
            }
            else
            {
                button1.Content = "Select";
            }
        }

        /// <summary>
        /// If a row button is pressed, select/unselect that row.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonRow_Click(object sender, EventArgs e)
        {
            Button button2 = (Button)sender;
            for (int j = 0; j < this.column; j++)
            {
                if ((string)button2.Content == "Select")
                {
                    if (!(bool)this.seatArray[(int)button2.Tag, j].chkSelected.IsChecked)
                    {
                        this.seatArray[(int)button2.Tag, j].chkSelected.IsChecked = true;
                    }
                }
                else
                {
                    if ((bool)this.seatArray[(int)button2.Tag, j].chkSelected.IsChecked)
                    {
                        this.seatArray[(int)button2.Tag, j].chkSelected.IsChecked = false;
                    }
                }
            }

            if ((string)button2.Content == "Select")
            {
                button2.Content = "Unselect";
            }
            else
            {
                button2.Content = "Select";
            }
        }

        /// <summary>
        /// If the select all/ unselect all button is pressed select/unselct all seats.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonAll_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            TextBlock block = (TextBlock)button.Content;
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
                    {
                        if (!(bool)this.seatArray[i, j].chkSelected.IsChecked)
                        {
                            this.seatArray[i, j].chkSelected.IsChecked = true;
                        }
                    }
                    else
                    {
                        if ((bool)this.seatArray[i, j].chkSelected.IsChecked)
                        {
                            this.seatArray[i, j].chkSelected.IsChecked = false;
                        }
                    }
                }
            }

            if (block.Text == "Select All")
            {
                block.Text = "Unselect All";
            }
            else
            {
                block.Text = "Select All";
            }
        }

        /// <summary>
        /// The name box has been changed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.myroom.RoomName = txtnm.Text;
            this.Title = this.myroom.RoomName + " - " + this.myroom.Location + ": " + this.myroom.Description;
        }

        /// <summary>
        /// The location box has been changed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void TextBoxLocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.myroom.Location = txtloc.Text;
            this.Title = this.myroom.RoomName + " - " + this.myroom.Location + ": " + this.myroom.Description;
        }

        /// <summary>
        /// The description box has been changed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void TextBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.myroom.Description = txtdes.Text;
            this.Title = this.myroom.RoomName + " - " + this.myroom.Location + ": " + this.myroom.Description;
        }

        /// <summary>
        /// Sets the seat background color according to the properties selected.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.seatsSelected.Count; i++)
            {
                this.seatsSelected.ElementAt(i).Chair.MustBeEmpty = (bool)chkboxEmpty.IsChecked;
                this.seatsSelected.ElementAt(i).Chair.NonChair = (bool)chkboxNotSeat.IsChecked;
            }
        }

        /// <summary>
        /// Makes the textbox for name turn off depending on if only 1 is chosen.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CheckBoxSelected_Checked(object sender, RoutedEventArgs e)
        {
            Seat seat = (Seat)sender;
            this.seatsSelected.Add(seat);
            if (this.seatsSelected.Count > 1)
            {
                txtnumber.IsEnabled = false;
                btnnumber.IsEnabled = false;
            }
        }

        /// <summary>
        /// Makes the textbox for name turn on depending on if only 1 is chosen.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CheckBoxSelected_Unchecked(object sender, RoutedEventArgs e)
        {
            Seat seat = (Seat)sender;
            this.seatsSelected.Remove(seat);
            if (this.seatsSelected.Count < 2)
            {
                txtnumber.IsEnabled = true;
                btnnumber.IsEnabled = true;
            }
        }

        /// <summary>
        /// R by C is clicked so name each by row then column.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void RowByColumn_click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            for (int i = 0; i < this.row; i++)
            {
                if (this.txtrow[i].Text == string.Empty)
                {
                    MessageBox.Show("Atleast one of your Rows doesn't have a name.");
                    flag = true;
                    break;
                }
            }

            for (int j = 0; j < this.column; j++)
            {
                if (this.txtcol[j].Text == string.Empty)
                {
                    MessageBox.Show("Atleast one of your Columns doesn't have a name.");
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                for (int i = 0; i < this.row; i++)
                {
                    for (int j = 0; j < this.column; j++)
                    {
                        this.seatArray[i, j].Chair.SeatName = this.txtrow[i].Text + this.txtcol[j].Text;
                        this.seatArray[i, j].lblName.Content = this.txtrow[i].Text + this.txtcol[j].Text;
                    }
                }
            }
        }

        /// <summary>
        /// C by R is clicked so name each by column then row.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ColumnByRow_click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            for (int i = 0; i < this.row; i++)
            {
                if (this.txtrow[i].Text == string.Empty)
                {
                    MessageBox.Show("At least one of your Rows doesn't have a name.");
                    flag = true;
                    break;
                }
            }

            for (int j = 0; j < this.column; j++)
            {
                if (this.txtcol[j].Text == string.Empty)
                {
                    MessageBox.Show("At least one of your Columns doesn't have a name.");
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                for (int i = 0; i < this.row; i++)
                {
                    for (int j = 0; j < this.column; j++)
                    {
                        this.seatArray[i, j].Chair.SeatName = this.txtcol[j].Text + this.txtrow[i].Text;
                        this.seatArray[i, j].lblName.Content = this.txtcol[j].Text + this.txtrow[i].Text;
                    }
                }
            }
        }

        /// <summary>
        /// Save the room file as template.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void FileMenuSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".tplt"; // Default file extension
            dlg.Filter = "Room File (.tplt)|*.tplt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Save the template
                this.myroom.WriteRoomTemplate(dlg.FileName);
            }
        }

        /// <summary>
        /// Sets handed depending on what is selected.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonHanded_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.seatsSelected.Count; i++)
            {
                this.seatsSelected.ElementAt(i).Chair.LeftHanded = (bool)rbtnLeftH.IsChecked;
                if (this.seatsSelected.ElementAt(i).Chair.LeftHanded)
                {
                    this.seatsSelected.ElementAt(i).lblName.Foreground = Brushes.Red;
                }
                else
                {
                    this.seatsSelected.ElementAt(i).lblName.Foreground = Brushes.Black;
                }
            }
        }

        /// <summary>
        /// Sets horizontal alignment depending on whats selected.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonHorizontal_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.seatsSelected.Count; i++)
            {
                if ((bool)rbtnLeft.IsChecked)
                {
                    this.seatsSelected.ElementAt(i).Chair.LrPosition = 0;
                }
                else if ((bool)rbtnRight.IsChecked)
                {
                    this.seatsSelected.ElementAt(i).Chair.LrPosition = 2;
                }
                else
                {
                    this.seatsSelected.ElementAt(i).Chair.LrPosition = 1;
                }

                this.seatsSelected.ElementAt(i).chkSelected.Margin = new Thickness(this.seatsSelected.ElementAt(i).Chair.LrPosition * 15, this.seatsSelected.ElementAt(i).Chair.FbPosition * 15, 0, 0);
            }
        }

        /// <summary>
        /// Sets vertical alignment depending on whats selected.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonVertical_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.seatsSelected.Count; i++)
            {
                if ((bool)rbtnBack.IsChecked)
                {
                    this.seatsSelected.ElementAt(i).Chair.FbPosition = 2;
                }
                else if ((bool)rbtnFront.IsChecked)
                {
                    this.seatsSelected.ElementAt(i).Chair.FbPosition = 0;
                }
                else
                {
                    this.seatsSelected.ElementAt(i).Chair.FbPosition = 1;
                }

                this.seatsSelected.ElementAt(i).chkSelected.Margin = new Thickness(this.seatsSelected.ElementAt(i).Chair.LrPosition * 15, this.seatsSelected.ElementAt(i).Chair.FbPosition * 15, 0, 0);
            }
        }

        /// <summary>
        /// Allows you to change the seats name/number.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonNumber_Click(object sender, RoutedEventArgs e)
        {
            if (txtnumber.Text.Length > 0)
            {
                this.seatsSelected.ElementAt(0).Chair.SeatName = txtnumber.Text;
                this.seatsSelected.ElementAt(0).lblName.Content = txtnumber.Text;
                MessageBox.Show(this.seatsSelected.ElementAt(0).Chair.SeatName);
            }
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonDone_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Place the selected student into a seat.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void Student_Drop(object sender, RoutedEventArgs e)
        {
            Seat seat = (Seat)sender;
            if (this.students.SelectedItem != null)
            {
                Student student = (Student)this.students.SelectedItem;
                if (seat.Chair.NonChair)
                {
                    MessageBox.Show("Can not place student in a non-chair.");
                }
                else if (seat.Chair.MustBeEmpty)
                {
                    MessageBox.Show("Students are not allowed to sit in this chair.");
                }
                else if (!this.myroom.IsStudentSeated(student))
                {
                    seat.Chair.TheStudent = student;
                }
                else
                {
                    MessageBox.Show(student.FirstName + " " + student.LastName + " is already seated");
                }

                this.students.UnselectAll();
            }
        }

        /// <summary>
        /// Run the automated placement algorithm.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void SeatStudents_click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException("Seat student algorithm not implemented");
        }

        /// <summary>
        /// Print the room seating chart.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void PrintClassroom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog printDlg = new System.Windows.Controls.PrintDialog();

                // Id print ok is pressed on print dialog
                if (printDlg.ShowDialog() == true) 
                {
                    // get selected printer capabilities
                    System.Printing.PrintCapabilities capabilities = printDlg.PrintQueue.GetPrintCapabilities(printDlg.PrintTicket);

                    // get scale of the print wrt to screen of WPF visual
                    double scale = Math.Min(
                        (capabilities.PageImageableArea.ExtentWidth / this.ActualWidth),
                        (capabilities.PageImageableArea.ExtentHeight / this.ActualHeight));

                    // Transform the Visual to scale
                    this.LayoutTransform = new ScaleTransform(scale, scale);

                    // get the size of the printer page
                    Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

                    // update the layout of the visual to the printer page size.
                    this.Measure(sz);
                    this.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

                    // now print the visual to printer to fit on the one page.
                    printDlg.PrintVisual(this, "First Fit to Page WPF Print");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Filter the list of students based on input filter text.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void TextBoxFilterStudents_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = this.textBoxFilterStudents.Text;
            this.students.Items.Filter = delegate(object obj)
            {
                Student s = obj as Student;
                if (s == null)
                {
                    return false;
                }
                else if (s.FirstName.ToLower().IndexOf(searchText.ToLower(), 0) > -1)
                {
                    return true;
                }
                else if (s.LastName.ToLower().IndexOf(searchText.ToLower(), 0) > -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }

        /// <summary>
        /// Clear all of the seats.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void AlgorithmClearSeats_Click(object sender, RoutedEventArgs e)
        {
            this.myroom.RunPlacementAlgorithm(new AssignmentClearRoom());
        }

        /// <summary>
        /// Attempt to place all of the students using a best effort.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void AlgorithmBestEffort_Click(object sender, RoutedEventArgs e)
        {
            this.myroom.RunPlacementAlgorithm(new AssignmentBestEffort());
        }

        /// <summary>
        /// Place only the visually impaired students.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void AlgorithmVisuallyImpaired_Click(object sender, RoutedEventArgs e)
        {
            this.myroom.RunPlacementAlgorithm(new AssignmentVisuallyImpaired());
        }

        /// <summary>
        /// Place only the left handed students.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void AlgorithmLeftHanded_Click(object sender, RoutedEventArgs e)
        {
            this.myroom.RunPlacementAlgorithm(new AssignmentLeftHanded());
        }

        /// <summary>
        /// Remove all seat reservations.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ReservationClear_Click(object sender, RoutedEventArgs e)
        {
            this.myroom.RunReservationAlgorithm(new ReservationClear());
        }

        /// <summary>
        /// Reserve a chekerboard pattern of seats.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ReservationCheckerboard_Click(object sender, RoutedEventArgs e)
        {
            this.myroom.RunReservationAlgorithm(new ReservationCheckerboard());
        }

        /// <summary>
        /// Reserve every other column of seats.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ReservationEveryOtherColumn_Click(object sender, RoutedEventArgs e)
        {
            this.myroom.RunReservationAlgorithm(new ReservationEveryOtherColumn());
        }

        /// <summary>
        /// Reserve every other row of seats.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ReservationEveryOtherRow_Click(object sender, RoutedEventArgs e)
        {
            this.myroom.RunReservationAlgorithm(new ReservationEveryOtherRow());
        }

        /// <summary>
        /// Reserve the left side of the room.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ReservationLeft_Click(object sender, RoutedEventArgs e)
        {
            this.myroom.RunReservationAlgorithm(new ReservationLeft());
        }

        /// <summary>
        /// Reserve the right side of the room.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ReservationRight_Click(object sender, RoutedEventArgs e)
        {
            this.myroom.RunReservationAlgorithm(new ReservationRight());
        }

        /// <summary>
        /// Reserve the back of the room.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ReservationBack_Click(object sender, RoutedEventArgs e)
        {
            this.myroom.RunReservationAlgorithm(new ReservationBack());
        }
    }
}
