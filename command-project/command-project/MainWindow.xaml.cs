
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

        static string localLogin = null;
        static string localEmail = null;
        static bool localModeGuest = false;
        public string LoginThisUser { get; set; } = localLogin;
        public string EmailUser { get; set; } = localEmail;
        public bool ModeGuest { get; set; } = localModeGuest;
        public MainWindow()
        {
            InitializeComponent();
            clientPort = GetFreePort();
            ThreadPool.QueueUserWorkItem(ReceiveData, 0);
        }
        void ReceiveData(object state)
        {
            while (true)
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
                        design.admin.Menu menu = null;
                        Dispatcher.Invoke(new Action(() => menu = new design.admin.Menu()));
                        Dispatcher.Invoke(new Action(() => menu.Show()));
                        Dispatcher.Invoke(new Action(() => Close()));
                    }
                }
                else
                {
                    MessageBox.Show("Wrong login or password");
                }
                client.Close();
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
