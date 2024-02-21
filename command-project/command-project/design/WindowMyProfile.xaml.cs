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
using command_project.design.admin;
using System.Runtime.Remoting.Contexts;
using Work.EDM;

namespace command_project.design
{
    /// Логика взаимодействия для WindowMyProfile.xaml
    /// </summary>
    public partial class WindowMyProfile : Window
    {
        string login = "";
        int serverPort = 0;
        int clientPort = 0;
        public WindowMyProfile(string login, int serverPort, int clientPort)
        {
            InitializeComponent();
            this.login = login;
            this.serverPort = serverPort;
            this.clientPort = clientPort;
            ThreadPool.QueueUserWorkItem(ReceiveData, 0);
        }

        private void cbMyProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lMyPig_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void lExit_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void bAddAd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void imgEditInfoProf_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void ReceiveData(object state)
        {
            Thread.Sleep(200);
            string sendIt = "getWorker>" + login;
            SendData(sendIt);
            while (true)
            {
                UdpClient client = new UdpClient(clientPort);
                IPEndPoint ipEnd = null;
                byte[] responce = client.Receive(ref ipEnd);
                string text = Encoding.Unicode.GetString(responce);
                List<string> texts = text.Split('>').ToList();
                if (texts[0] == "infoForWorker")
                {
                    DateTime userBD = DateTime.Parse(texts[7]);
                    Dispatcher.Invoke(new Action(() => lName.Content = texts[3] + " " + texts[4]));
                    Dispatcher.Invoke(new Action(() => lAge.Content = texts[11]));
                    Dispatcher.Invoke(new Action(() => lPhone.Content = texts[10]));
                    Dispatcher.Invoke(new Action(() => lEmail.Content = texts[5]));
                    Dispatcher.Invoke(new Action(() => lPlaceOfResidence.Content = texts[8] + " " + texts[9]));
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
    }
}
