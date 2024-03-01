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
using command_project.coding;

namespace command_project.design
{
    /// Логика взаимодействия для WindowMyProfile.xaml
    /// </summary>
    public partial class WindowMyProfile : Window
    {
        string login = "";
        int serverPort = 0;
        int clientPort = 0;

        List<coding.Skill> listSkills = new List<coding.Skill>();
        coding.Skill addSkill = new coding.Skill();
        coding.Skill currentSkill = new coding.Skill();

        List<string> skills = new List<string>();
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
                    skills = texts[12].Split('|').ToList();
                    Dispatcher.Invoke(new Action(() => _Skills.ItemsSource = skills));
                    Dispatcher.Invoke(new Action(() => _txtABout.Text = texts[13]));
                    listSkills = Functions.GetSkills(texts[14]);
                    Dispatcher.Invoke(new Action(() => _listFindData.ItemsSource = listSkills.Select(i => i.ReadSkill())));
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {

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
                _addSkillLabel.Content = addSkill.ReadSkill();
            }
        }
        private void _Experience_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_Skills.SelectedIndex >= 0)
            {
                addSkill = new coding.Skill() { Name = _Skills.SelectedItem.ToString(), Time = (int)_Experience.Value };
                _addSkillLabel.Content = addSkill.ReadSkill();
            }
        }

        private void RefreshList()
        {
            _listFindData.ItemsSource = listSkills.Select(i => i.ReadSkill()).ToList();
        }

        private void _addSkillButton_Click(object sender, RoutedEventArgs e)
        {
            if (_addSkillLabel.Content.ToString() != "" && listSkills.Where(i => i.Name == addSkill.Name).ToList().Count == 0)
            {
                listSkills.Add(addSkill);
                RefreshList();
            }
            _addSkillLabel.Content = "";
        }

        private void _listFindData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_listFindData.SelectedIndex != -1)
            {
                _changeSkillLabel.Content = _listFindData.SelectedItem.ToString();
            }
        }

        private void _changeSkillButton_Click(object sender, RoutedEventArgs e)
        {
            if (_listFindData.SelectedIndex != -1)
            {
                listSkills.First(i => i.ReadSkill() == _changeSkillLabel.Content.ToString()).Time = (int)_SelectedExperience.Value;
                RefreshList();
            }
            _changeSkillLabel.Content = "";
        }

        private void _deleteSkillButton_Click(object sender, RoutedEventArgs e)
        {
            if (_listFindData.SelectedIndex != -1)
            {
                listSkills.Remove(listSkills.First(i => i.ReadSkill() == _changeSkillLabel.Content.ToString()));
                RefreshList();
            }
            _changeSkillLabel.Content = "";
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SendData("close");
        }
    }
}
