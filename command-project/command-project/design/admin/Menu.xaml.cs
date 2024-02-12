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
using command_project.design.admin.choose;

namespace command_project.design.admin
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        List<string> list = new List<string>();
        int numberOfMonths = 12;

        public Menu()
        {
            InitializeComponent();

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
            //
            //for (int i = 0; i < numberOfMonths; i++)
            //{
            //    if (month.ToString("yyyy-MM") == list[i])
            //    {
            //        string[] lines = System.IO.File.ReadAllLines(filePath);
            //
            //        if (i < lines.Length)
            //        {
            //            if (int.TryParse(lines[i], out int result))
            //            {
            //                return result;
            //            }
            //        }
            //    }
            //}
            return 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChooseWindow chooseWindow = new ChooseWindow();
            chooseWindow.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void _txtFindSkills_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _Skills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _Experience_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void _changeSkillButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _deleteSkillButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _listFindData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _addSkillButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _txtRejectedFindSkills_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _RejectedSkills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _RejectedExperience_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void _addRejectedSkillButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _listRejectedFindData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _changeRejectedSkillButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _deleteRejectedSkillButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _txtApprovedFindSkills_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _ApprovedSkills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _ApprovedExperience_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void _addApprovedSkillButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _listApprovedFindData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        // Зробити сортування за алфавітом для кожного стовпця з _VerifiedResumes
    }
}
