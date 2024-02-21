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

namespace command_project.Employer
{
    /// <summary>
    /// Interaction logic for Chats.xaml
    /// </summary>
    public partial class Chats : Window
    {
        private MainWindow profileWindow;
        private Chats chatsWindow;
        private CreateVacancy createVacancyWindow;
        private MyVacancy myVacancyWindow;
        public Chats()
        {
            InitializeComponent();
            // Видаліть рядок, який створює новий екземпляр Chats
            // chatsWindow = new Chats(); 
            createVacancyWindow = new CreateVacancy();
            myVacancyWindow = new MyVacancy();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTabControl.SelectedItem == ProfileTabItem)
            {
                if (!this.IsVisible)
                    this.Show();
            }
            else if (MainTabControl.SelectedItem == ChatsTabItem)
            {
                if (!chatsWindow.IsVisible)
                    chatsWindow.Show();
            }
            else if (MainTabControl.SelectedItem == CreateVacancyTabItem)
            {
                if (!createVacancyWindow.IsVisible)
                    createVacancyWindow.Show();
            }
            else if (MainTabControl.SelectedItem == MyVacancyTabItem)
            {
                if (!myVacancyWindow.IsVisible)
                    myVacancyWindow.Show();
            }
        }
    }
}
