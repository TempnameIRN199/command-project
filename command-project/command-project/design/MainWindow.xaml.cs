
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace command_project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

        }
        static string localLogin = null;
        static string localEmail = null;
        static bool localModeGuest = false;
        public string LoginThisUser { get; set; } = localLogin;
        public string EmailUser { get; set; } = localEmail;
        public bool ModeGuest { get; set; } = localModeGuest;
        private void bSign_Click(object sender, RoutedEventArgs e)
        {
 
        }

        private void bSingGuest_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bSing_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bRegistration_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bCantSign_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bSingAdm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
