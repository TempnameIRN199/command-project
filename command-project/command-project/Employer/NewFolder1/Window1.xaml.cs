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
using System.Net.NetworkInformation;
using System.Timers;
using command_project.design;
using command_project.coding;

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

        List<coding.Skill> listSkills = new List<coding.Skill>();
        coding.Skill addSkill = new coding.Skill();
        coding.Skill currentSkill = new coding.Skill();

        List<string> skills = new List<string>();
        List<rClass> myRequests = new List<rClass>();
        rClass selectedRequest = new rClass();

        List<wClass> myWorkers = new List<wClass>();
        wClass selectedWorker = new wClass();

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
                    Dispatcher.Invoke(new Action(() => _Skills.ItemsSource = skills));
                }
                else if (texts[0] == "RequestAdded")
                {
                    Dispatcher.Invoke(new Action(() => MessageBox.Show("Вакансія зроблена")));
                }
                else if (texts[0] == "RequestExist")
                {
                    Dispatcher.Invoke(new Action(() => MessageBox.Show("Вакансія з таким ім'ям вже існує")));
                }
                else if (texts[0] == "yourRequests")
                {
                    myRequests.Clear();
                    for (int i = 1; i < texts.Count; i++)
                    {
                        if (texts[i] == "") break;
                        List<string> parts = texts[i].Split('&').ToList();
                        myRequests.Add(new rClass() { Name = parts[0], Info = parts[1], Skills = parts[2] });
                    }
                    Dispatcher.Invoke(new Action(() => _listMyRequests.ItemsSource = myRequests.Select(i => i.Name)));
                }
                else if (texts[0] == "workersOnRequest")
                {
                    myWorkers.Clear();
                    for (int i = 1; i < texts.Count; i++)
                    {
                        if (texts[i] == "") break;
                        List<string> parts = texts[i].Split('&').ToList();
                        myWorkers.Add(new wClass() { Name = parts[0], Info = parts[1], Skills = parts[2], Login = parts[3] });
                    }
                    Dispatcher.Invoke(new Action(() => _listWorkers.ItemsSource = myWorkers.Select(i => i.Name)));
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
            if (_myTab.SelectedIndex == 1)
            {
                SendData("getMyRequests>" + login);
                _listMyRequests.SelectedIndex = -1;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void _listFindName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_listFindName.SelectedIndex != -1)
            {
                currentSkill =  listSkills[_listFindName.SelectedIndex];
            }
        }
        private void _Experience_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_Skills.SelectedIndex >= 0)
            {
                addSkill = new coding.Skill() { Name = _Skills.SelectedItem.ToString(), Time = (int)_Experience.Value };
            }
        }

        private void _txtFindSkills_TextChanged(object sender, TextChangedEventArgs e)
        {
            _Skills.ItemsSource = skills.Where(i => i.ToLower().Contains(_txtFindSkills.Text.ToLower()));
        }

        private void _Skills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_Skills.SelectedIndex >= 0)
            {
                addSkill = new coding.Skill() { Name = _Skills.SelectedItem.ToString(), Time = (int)_Experience.Value };
            }
        }

        private void RefreshList()
        {
            _listFindName.ItemsSource = listSkills.Select(i => i.ReadSkill()).ToList();
        }

        private void _addSkillButton_Click(object sender, RoutedEventArgs e)
        {
            if (listSkills.Where(i => i.Name == addSkill.Name).ToList().Count == 0 && addSkill.Name != "" && addSkill.Time != 0)
            {
                listSkills.Add(addSkill);
                RefreshList();
            }
        }


        private void _deleteSkillButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentSkill.Name != null)
            {
                listSkills.Remove(listSkills.First(i => i.Name == currentSkill.Name));
                currentSkill = new coding.Skill();
                RefreshList();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SendData("close");
        }

        private void _addRequestButton_Click(object sender, RoutedEventArgs e)
        {
            if (_txtvacancyName.Text == "" || _txtvacancyInfo.Text == "")
            {
                MessageBox.Show("Заповніть всі данні");
            }
            else
            {
                SendData("addRequest>" + login + ">" + _txtvacancyName.Text + ">" + _txtvacancyInfo.Text + ">" + coding.Functions.SkillsIntoDBFormat(listSkills));
            }
        }

        private void _listMyRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_listMyRequests.SelectedIndex != -1)
            {
                selectedRequest = myRequests[_listMyRequests.SelectedIndex];
                _txtRequestAboutItem.Text = selectedRequest.Show();
                SendData("getWorkersOnRequest>" + selectedRequest.Name);
                _listWorkers.SelectedIndex = -1;
                selectedWorker = new wClass();
                _txtAboutWorker.Text = "";
                _txtIsEnough.Text = "";
                _buttonAccept.IsEnabled = false;
                _buttonDecline.IsEnabled = false;
            }
        }

        private void _listWorkers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_listWorkers.SelectedIndex != -1)
            {
                selectedWorker = myWorkers[_listWorkers.SelectedIndex];
                _txtAboutWorker.Text = selectedWorker.Show();
                bool isEnough = Functions.IsEnoughSkills(Functions.GetSkills(selectedWorker.Skills), Functions.GetSkills(selectedRequest.Skills));
                if (isEnough)
                {
                    _txtIsEnough.Foreground = Brushes.Green;
                    _txtIsEnough.Text = "Достатньо навичок";
                }
                else
                {
                    _txtIsEnough.Foreground = Brushes.Red;
                    _txtIsEnough.Text = "Не достатньо навичок";
                }
                _buttonAccept.IsEnabled = true;
                _buttonDecline.IsEnabled = true;
            }
        }

        private void _buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            SendData("acceptWorkerOnRequest>" + selectedWorker.Login + ">" + selectedRequest.Name);
            _txtAboutWorker.Text = "";
            _txtIsEnough.Text = "";
            _buttonAccept.IsEnabled = false;
            _buttonDecline.IsEnabled = false;
        }

        private void _buttonDecline_Click(object sender, RoutedEventArgs e)
        {
            SendData("declinetWorkerOnRequest>" + selectedWorker.Login + ">" + selectedRequest.Name);
            _txtAboutWorker.Text = "";
            _txtIsEnough.Text = "";
            _buttonAccept.IsEnabled = false;
            _buttonDecline.IsEnabled = false;
        }
    }

    class wClass
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public string Skills { get; set; }
        public string Login { get; set; }

        public wClass() { }
        public string Show()
        {
            return Name + "\n" + Info + "\n" + string.Join("\n", Functions.GetSkills(Skills).Select(i => i.ReadSkill()));
        }
    }
}
