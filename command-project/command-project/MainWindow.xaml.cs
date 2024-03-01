
using command_project.coding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        public int staticServerPort = 22885;
        public int serverPort = 0;
        public int clientPort = 0;
        public string login = "";

        bool working = true;

        public MainWindow()
        {
            InitializeComponent();
            clientPort = GetFreePort();
            ThreadPool.QueueUserWorkItem(ReceiveData, 0);
            bCantSign.Foreground = Brushes.Red;
            bCantSign.Content = "";
        }
        void ReceiveData(object state)
        {
            while (working)
            {
                UdpClient client = new UdpClient(clientPort);
                IPEndPoint ipEnd = null;
                byte[] responce = client.Receive(ref ipEnd);
                string text = Encoding.Unicode.GetString(responce);
                List<string> texts = text.Split('>').ToList();
                if (texts[0] == "rightPassword")
                {
                    login = texts[1];
                    serverPort = int.Parse(texts[2]);
                    string status = texts[3];
                    if (status == "Admin")
                    {
                        working = false;
                        design.admin.Menu menu = null;
                        Dispatcher.Invoke(new Action(() => menu = new design.admin.Menu(login, serverPort, clientPort)));
                        Dispatcher.Invoke(new Action(() => menu.Show()));
                        Dispatcher.Invoke(new Action(() => Close()));
                    }
                    else if (status == "Worker") 
                    {
                        working = false;
                        design.WindowMyProfile window = null;
                        Dispatcher.Invoke(new Action(() => window = new design.WindowMyProfile(login, serverPort, clientPort)));
                        Dispatcher.Invoke(new Action(() => window.Show()));
                        Dispatcher.Invoke(new Action(() => Close()));
                    }
                    //else if (status == "Employer")
                    //{
                    //    working = false;
                    //    design.WindowMyProfile window = null;
                    //    Dispatcher.Invoke(new Action(() => window = new design.WindowMyProfile(login, serverPort, clientPort)));
                    //    Dispatcher.Invoke(new Action(() => window.Show()));
                    //    Dispatcher.Invoke(new Action(() => Close()));
                    //}
                    Dispatcher.Invoke(new Action(() => Close()));
                }
                else if (texts[0] == "wrongLogin")
                {
                    Dispatcher.Invoke(new Action(() => bCantSign.Content = "Неіснуючий логін"));
                }
                else if (texts[0] == "wrongPassword")
                {
                    Dispatcher.Invoke(new Action(() => bCantSign.Content = "Неправильний пароль"));
                }
                else if (texts[0] == "rightLogin")
                {
                    Dispatcher.Invoke(new Action(() => _IsRightReg.Content = "Ви зареєстровані"));
                    Dispatcher.Invoke(new Action(() => _IsRightReg.Foreground = Brushes.Green));
                }
                else if (texts[0] == "existLogin")
                {
                    Dispatcher.Invoke(new Action(() => _IsRightReg.Content = "Цей логін вже зайнятий"));
                    Dispatcher.Invoke(new Action(() => _IsRightReg.Foreground = Brushes.Red));
                }
                client?.Close();
            }
        }
        void SendData(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);

            UdpClient client = new UdpClient();
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), staticServerPort);

            client.Send(bytes, bytes.Length, ipEnd);

            client.Close();

            Console.WriteLine(text);
        }

        public static int GetFreePort()
        {
            int port = 31000;
            while (true)
            {
                try
                {
                    UdpClient client = new UdpClient(port);
                    client.Close();
                    return port;
                }
                catch
                {
                    port++;
                }
            }
        }
        private void bSign_Click(object sender, RoutedEventArgs e)
        {
            string data = "checkPassword>" + tbLoginSign.Text + ">" + pbPassSign.Password + ">" + clientPort; 
            SendData(data);
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
            //login > login > password > name > secondName > email > number > country > city > birthdate > status > port
            if (tbEmailReg.Text == "" || tbLoginReg.Text == "" || tbPasswordReg.Text == "" || tbPhoneNumberReg.Text == ""
                || tbNameReg.Text == "" || tbSNameReg.Text == "" || tbCountryReg.Text == "" || tbCityReg.Text == "" || dpBirth.SelectedDate == null)
            {
                _IsRightReg.Content = "Не всі поля заповнені";
                _IsRightReg.Foreground = Brushes.Red;
            }
            else
            {
                string sendIt = "login>" + tbLoginReg.Text + ">" + tbPasswordReg.Text + ">" + tbNameReg.Text + ">" + tbSNameReg.Text + ">" +
                    tbEmailReg.Text + ">" + tbPhoneNumberReg.Text + ">" + tbCountryReg.Text + ">" + tbCityReg.Text + ">" + dpBirth.SelectedDate.ToString() + ">" + _cbStatus.Text + ">" + clientPort;
                SendData(sendIt);
            }
        }

        private void bSingAdm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            working = false;
        }

        private void tbLoginSign_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (bCantSign.Content.ToString() != "")
            {
                bCantSign.Content = "";
            }
        }

        private void pbPassSign_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (bCantSign.Content.ToString() != "")
            {
                bCantSign.Content = "";
            }
        }

        private void dpBirth_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_IsRightReg.Content.ToString() != "")
            {
                _IsRightReg.Content = "";
            }
        }

        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_IsRightReg.Content.ToString() != "")
            {
                _IsRightReg.Content = "";
            }
        }
    }
}
