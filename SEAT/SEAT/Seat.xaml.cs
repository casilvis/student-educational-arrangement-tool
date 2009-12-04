﻿// <copyright file="Seat.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEAT
{
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

    /// <summary>
    /// Interaction logic for Seat.xaml
    /// </summary>
    public partial class Seat : UserControl
    {
        private Chair chair;
        private TextBlock txtblname = new TextBlock();

        public Seat()
        {
            this.chair = new Chair();
            InitializeComponent();
        }

        public Seat(Chair chair, bool editable)
        {
            this.txtblname.TextWrapping = TextWrapping.Wrap;
            this.txtblname.FontSize = 8;
            this.chair = chair;
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

            if (Chair.LeftHanded)
            {
                lblName.Foreground = Brushes.Red;
            }

            if (Chair.NonChair || Chair.MustBeEmpty)
            {
                if (Chair.NonChair)
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

            chkSelected.Margin = new Thickness(Chair.LrPosition * 15, Chair.FbPosition * 15, 0, 0);
            if (!editable)
            {
                chkSelected.Visibility = Visibility.Hidden;
                this.AddHandler(UserControl.MouseDoubleClickEvent, new RoutedEventHandler(this.Seat_click));
                grdSelect.Children.Add(this.txtblname);
            }

            lblName.Content = Chair.SeatName;
        }

        public Chair Chair
        {
            get { return this.chair; }
        }

        public TextBlock Txtblname
        {
            get { return this.txtblname; }
            set { this.txtblname = value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // throw new NotImplementedException("UserControl_Loaded");
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            // throw new NotImplementedException("UserControl_Loaded");
        }

        private void Seat_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hey");
        }
    }
}
