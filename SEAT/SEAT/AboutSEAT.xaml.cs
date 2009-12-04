// <copyright file="AboutSEAT.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEAT
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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

    /// <summary>
    /// Interaction logic for AboutSEAT.xaml
    /// </summary>
    public partial class AboutSEAT : Window
    {
        public AboutSEAT()
        {
            InitializeComponent();
            buttonOk.Focus();
            this.AddHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(this.HyperlinkNavigate));
        }

        private void HyperlinkNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void ButtonOk(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
