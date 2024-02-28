using System;
using System.Collections.Generic;
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

namespace command_project.design.admin
{
    /// <summary>
    /// Логика взаимодействия для InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow(tClass tClass)
        {
            InitializeComponent();
            _txtUserName.Text = tClass.UserName;
            _txtUserInfo.Text = tClass.UserDescription;
            _txtUserSkills.Text = coding.Functions.SkillsIntoText(tClass.UserSkills);

            _txtRequestName.Text = tClass.WorkName;
            _txtRequestInfo.Text = tClass.WorkDescription;
            _txtRequestSkills.Text = coding.Functions.SkillsIntoText(tClass.WorkSkills);

            _txtStatus.Text = tClass.Status;
            bool isGood = coding.Functions.IsEnoughSkills(coding.Functions.GetSkills(tClass.UserSkills), coding.Functions.GetSkills(tClass.WorkSkills));
            if (isGood)
            {
                _txtIsGood.Text = "Достатньо";
                //_txtIsGood.Foreground = B
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
