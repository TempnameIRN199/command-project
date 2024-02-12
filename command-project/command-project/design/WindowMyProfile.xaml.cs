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

namespace command_project.design.admin.choose
{
    /// <summary>
<<<<<<<< HEAD:command-project/command-project/design/admin/choose/ChooseWindow.xaml.cs
    /// Логика взаимодействия для ChooseWindow.xaml
    /// </summary>
    public partial class ChooseWindow : Window
    {
        public ChooseWindow()
========
    /// Логика взаимодействия для WindowMyProfile.xaml
    /// </summary>
    public partial class WindowMyProfile : Window
    {
        public WindowMyProfile()
>>>>>>>> design:command-project/command-project/design/WindowMyProfile.xaml.cs
        {
            InitializeComponent();
        }

<<<<<<<< HEAD:command-project/command-project/design/admin/choose/ChooseWindow.xaml.cs
        private void Button_Click(object sender, RoutedEventArgs e)
========
        private void cbMyProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
>>>>>>>> design:command-project/command-project/design/WindowMyProfile.xaml.cs
        {

        }
    }
}
