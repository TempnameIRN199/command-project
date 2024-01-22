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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace command_project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Из поля баз данных перевод в лист навыков
        List<Skill> GetSkills(string skillsText)
        {
            //Из C#^5|SQL^2 в Skill(){Name = "C#", Time = 5}, Skill(){Name = "SQL", Time = 2}
            List<Skill> skills = new List<Skill>();

            List<string> skillsTextParts = skillsText.Split('|').ToList();
            foreach (string skill in skillsTextParts)
            {
                List<string> parts = skillsText.Split('^').ToList();
                skills.Add(new Skill() { Name = parts[0], Time = int.Parse(parts[1]) });
            }

            return skills;
        }
    }

    class Skill
    {
        public string Name { get; set; }
        public int Time { get; set; }

        public Skill() { }
    }
}
