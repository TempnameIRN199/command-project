using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Definitions.Charts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using command_project.coding;
using System.Diagnostics;

namespace command_project.design.admin
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    /// 

    //class Abc
    //{
    //    public string a { get; set;}
    //    public string b { get; set;}
    //    public string c { get; set;}
    //}

    public partial class Menu : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        List<string> list = new List<string>();
        int numberOfMonths = 12;

        string login = "";
        int serverPort = 0;
        int clientPort = 0;

        List<Skill> listSkills = new List<Skill>();
        Skill addSkill = new Skill();
        Skill currentSkill = new Skill();

        List<string> skills = new List<string>();

        List<tClass> tab1List = new List<tClass>();

        public Menu(string login, int serverPort, int clientPort)
        {
            InitializeComponent();
            this.login = login;
            this.serverPort = serverPort;
            this.clientPort = clientPort;

            ThreadPool.QueueUserWorkItem(ReceiveData, 0);


            //_VerifiedResumes.ItemsSource = new List<string> { "item1", "item2" };




            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Registered Users",
                    Values = new ChartValues<int>(),
                    LineSmoothness = 0.8,
                    PointGeometry = new EllipseGeometry(new Point(0, 0), 10, 10),
                    Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 255)),
                    StrokeThickness = 4
                }
            };

            for (int i = 0; i < numberOfMonths; i++)
            {
                DateTime currentDate = DateTime.Now.AddMonths(i);
                list.Add(currentDate.ToString("yyyy-MM"));

                int userCount = GetUserDataForMonth(currentDate);
                SeriesCollection[0].Values.Add(userCount);
            }
            statsDash.AxisX.Add(new Axis
            {
                Labels = list.ToArray(),
                Title = "Дати",
            });
            statsDash.AxisY.Add(new Axis
            {
                Title = "Кількість резюме",
                MinValue = -500,
                MaxValue = 500
            });

            DataContext = this;

            statsDash.AxisX[0].LabelFormatter = value => new DateTime((long)(value * TimeSpan.FromDays(30).Ticks)).ToString("yyyy-MM");
            statsDash.AxisY[0].LabelFormatter = value => value.ToString("N");
        }

        private int GetUserDataForMonth(DateTime month)
        {
            //string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"D:\Coding\Learn\Управління проектами\command-project\command-project\command-project\design\statsTest.txt");
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"D:\ItStep\Current\command-project\command-project\command-project\design\statsTest.txt");
            //string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"C:\adsdadsdadas\pro2\command-project\command-project\design\statsTest.txt");
            for (int i = 0; i < numberOfMonths; i++)
            {
                if (month.ToString("yyyy-MM") == list[i])
                {
                    string[] lines = System.IO.File.ReadAllLines(filePath);

                    if (i < lines.Length)
                    {
                        if (int.TryParse(lines[i], out int result))
                        {
                            return result;
                        }
                    }
                }
            }
            return 0;
        }

        void ReceiveData(object state)
        {
            Thread.Sleep(200);
            string sendIt = "getAdmin";
            SendData(sendIt);
            while (true)
            {
                UdpClient client = new UdpClient(clientPort);
                IPEndPoint ipEnd = null;
                byte[] responce = client.Receive(ref ipEnd);
                string text = Encoding.Unicode.GetString(responce);
                List<string> texts = text.Split('>').ToList();
                if (texts[0] == "infoForAdmin")
                {
                    skills = texts[1].Split('|').ToList();
                    Dispatcher.Invoke(new Action(() => _Skills.ItemsSource = skills));
                    Dispatcher.Invoke(new Action(() => _addSkillsView.ItemsSource = skills));
                }
                if (texts[0] == "adminTab1Info")
                {
                    tab1List.Clear();
                    for (int i = 1; i < texts.Count; i++)
                    {
                        if (texts[i] == "") continue;
                        List<string> rcvs = texts[i].Split('&').ToList();
                        tab1List.Add(new tClass() 
                        { 
                            UserName = rcvs[0], UserDescription = rcvs[1], UserSkills = rcvs[2],
                            WorkName = rcvs[3], WorkDescription = rcvs[4], WorkSkills = rcvs[5],
                            Status = rcvs[6]
                        });
                    }
                    tab1List.ForEach(i => i.Check(listSkills));
                    Dispatcher.Invoke(new Action(() => _VerifiedResumes.ItemsSource = tab1List.Select(i => i.Show())));
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

        private void Window_Closed(object sender, EventArgs e)
        {
            SendData("close");
        }

        private void _txtFindSkills_TextChanged(object sender, TextChangedEventArgs e)
        {
            _Skills.ItemsSource = skills.Where(i => i.ToLower().Contains(_txtFindSkills.Text.ToLower()));
        }



        private void _Skills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_Skills.SelectedIndex >= 0)
            {
                addSkill = new Skill() { Name = _Skills.SelectedItem.ToString(), Time = (int)_Experience.Value };
                _addSkillLabel.Content = addSkill.ReadSkill();
            }
        }
        private void _Experience_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_Skills.SelectedIndex >= 0)
            {
                addSkill = new Skill() { Name = _Skills.SelectedItem.ToString(), Time = (int)_Experience.Value };
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

            tab1List.ForEach(i => i.Check(listSkills));
            Dispatcher.Invoke(new Action(() => _VerifiedResumes.ItemsSource = tab1List.Select(i => i.Show())));
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

            tab1List.ForEach(i => i.Check(listSkills));
            Dispatcher.Invoke(new Action(() => _VerifiedResumes.ItemsSource = tab1List.Select(i => i.Show())));
        }

        private void _deleteSkillButton_Click(object sender, RoutedEventArgs e)
        {
            if (_listFindData.SelectedIndex != -1)
            {
                listSkills.Remove(listSkills.First(i => i.ReadSkill() == _changeSkillLabel.Content.ToString()));
                RefreshList();
            }
            _changeSkillLabel.Content = "";

            tab1List.ForEach(i => i.Check(listSkills));
            Dispatcher.Invoke(new Action(() => _VerifiedResumes.ItemsSource = tab1List.Select(i => i.Show())));
        }

        private void myTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myTabControl.SelectedIndex == 1)
            {
                string sendIt = "getAdminTab1Info>" + pageLabel.Content + ">" + (_Status.SelectedItem as ComboBoxItem).Content;
                SendData(sendIt);
            }
        }
        private void _Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sendIt = "getAdminTab1Info>" + pageLabel.Content + ">" + (_Status.SelectedItem as ComboBoxItem).Content;
            SendData(sendIt);
        }

        private void _addSkillsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!skills.Contains(_txtAddSkills.Text)) SendData("addSkill>" + _txtAddSkills.Text);
        }

        private void _minusPage_Click(object sender, RoutedEventArgs e)
        {
            int page = int.Parse(pageLabel.Content.ToString());
            if (page <= 1)
            {
                return;
            }
            else
            {
                page--;
                pageLabel.Content = page.ToString();
                string sendIt = "getAdminTab1Info>" + pageLabel.Content + ">" + (_Status.SelectedItem as ComboBoxItem).Content;
                SendData(sendIt);
            }
        }

        private void _plusPage_Click(object sender, RoutedEventArgs e)
        {
            int page = int.Parse(pageLabel.Content.ToString());
            page++;
            pageLabel.Content = page.ToString();
            string sendIt = "getAdminTab1Info>" + pageLabel.Content + ">" + (_Status.SelectedItem as ComboBoxItem).Content;
            SendData(sendIt);
        }

        private void git_Click(object sender, RoutedEventArgs e)
        {
            if (sender == Mark)
            {
                Process.Start("https://github.com/Mark111P");
            }
            else if (sender == Semen)
            {
                Process.Start("https://github.com/merphyth");
            }
            else if (sender == Ilya)
            {
                Process.Start("https://github.com/o4hools");
            }
            else if (sender == Sanya)
            {
                Process.Start("https://github.com/kriomanser2000");
            }
            else if (sender == Victor)
            {
                Process.Start("https://github.com/TempnameIRN199");
            }
        }

        private void _VerifiedResumes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_VerifiedResumes.SelectedItem != null)
            {
                InfoWindow infoWindow = new InfoWindow(tab1List[_VerifiedResumes.SelectedIndex]);
                infoWindow.Show();
            }
            else { MessageBox.Show("Виберіть резюме"); }
        }

        private void _VerifiedResumes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _VerifiedResumes.ItemsSource = null;
            _VerifiedResumes.ItemsSource = tab1List.Select(i => i.Show());
        }

        // Зробити сортування за алфавітом для кожного стовпця з _VerifiedResumes
    }

    public class tClass
    {
        public string UserName { get; set; }
        public string UserDescription { get; set; }
        public string UserSkills { get; set; }
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public string WorkSkills { get; set; }
        public string isEnough;
        public string Status { get; set; }

        public tClass() 
        { 
            isEnough = "Достатньо навичок";
        }
        public string Show()
        {
            return UserName + " " + WorkName + " " + isEnough + " " + Status;
        }
        public void Check(List<Skill> skills)
        {
            bool t = Functions.IsEnoughSkills(Functions.GetSkills(UserSkills), skills);

            if (t)
            {
                isEnough = "Достатньо навичок";
            }
            else
            {
                isEnough = "Не достатньо навичок";
            }
        }
    }
}
