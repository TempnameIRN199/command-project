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
        public string sendIt = "";
        tClass myT = new tClass();
        public InfoWindow(tClass tClass)
        {
            InitializeComponent();
            myT = tClass;
            _txtUserName.Text = tClass.UserName;

            string tText = "Number: " + tClass.UserNumber + "\n" + "Email: " + tClass.UserEmail + "\n" + tClass.UserDescription;

            _txtUserInfo.Text = tText;
            _txtUserSkills.Text = coding.Functions.SkillsIntoText(tClass.UserSkills);

            _txtRequestName.Text = tClass.WorkName;
            _txtRequestInfo.Text = tClass.WorkDescription;
            _txtRequestSkills.Text = coding.Functions.SkillsIntoText(tClass.WorkSkills);

            _txtStatus.Text = tClass.Status;
            bool isGood = coding.Functions.IsEnoughSkills(coding.Functions.GetSkills(tClass.UserSkills), coding.Functions.GetSkills(tClass.WorkSkills));
            if (isGood)
            {
                _txtIsGood.Text = "Достатньо";
                _txtIsGood.Foreground = Brushes.Green;
            }
            else
            {
                _txtIsGood.Text = "Не достатньо";
                _txtIsGood.Foreground = Brushes.Red;
            }

            //if (tClass.Status == "Accepted byAdmin" || tClass.Status == "Declined byAdmin")
            if (!tClass.IsActiveWork)
            {
                _bAccept.IsEnabled = false;
                _bDecline.IsEnabled = false;
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            sendIt = "acceptByAdmin>" + myT.Id;
            Close();
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            sendIt = "declineByAdmin>" + myT.Id;
            Close();
        }
    }
}
