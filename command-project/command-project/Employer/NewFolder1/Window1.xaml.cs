using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace command_project.Employer.NewFolder1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string login = "";
        int serverPort = 0;
        int clientPort = 0;

        List<string> skills = new List<string>();
        public Window1(string login, int serverPort, int clientPort)
        {
            InitializeComponent();

            InitializeComponent();
            this.login = login;
            this.serverPort = serverPort;
            this.clientPort = clientPort;
            ThreadPool.QueueUserWorkItem(ReceiveData, 0);
        }
        void ReceiveData(object state)
        {
            Thread.Sleep(200);
            string sendIt = "getEmployer>" + login;
            SendData(sendIt);
            while (true)
            {
                UdpClient client = new UdpClient(clientPort);
                IPEndPoint ipEnd = null;
                byte[] responce = client.Receive(ref ipEnd);
                string text = Encoding.Unicode.GetString(responce);
                List<string> texts = text.Split('>').ToList();
                if (texts[0] == "infoForEmployer")
                {
                    Dispatcher.Invoke(new Action(() => _lblLogin.Text = login));
                    Dispatcher.Invoke(new Action(() => _lblName.Text = texts[1]));
                    Dispatcher.Invoke(new Action(() => _lblEmail.Text = texts[2]));
                    Dispatcher.Invoke(new Action(() => _lblRDate.Text = texts[3]));
                    Dispatcher.Invoke(new Action(() => _lblBD.Text = texts[4]));
                    Dispatcher.Invoke(new Action(() => _lblPlace.Text = texts[5]));
                    Dispatcher.Invoke(new Action(() => _lblNumber.Text = texts[6]));
                    skills = texts[7].Split('|').ToList();
                }
                client.Close();
            }
        }
        void SendData(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);

            UdpClient client = new UdpClient();
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), serverPort);

            client.Send(bytes, bytes.Length, ipEnd);

            client.Close();

            Console.WriteLine(text);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void _listFindName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void _listFindName1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void _Skills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void _Experience_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            SendData("close");
        }
    }
}
