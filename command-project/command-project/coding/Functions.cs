using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command_project.coding
{
    internal class Functions
    {
        //Из поля баз данных перевод в лист навыков
        List<Skill> GetSkills(string skillsText)
        {
            //Из C#^5|SQL^2 в Skill(){Name = "C#", Time = 5}, Skill(){Name = "SQL", Time = 2}
            List<Skill> skills = new List<Skill>();

            List<string> skillsTextParts = skillsText.Split('|').ToList();
            foreach (string skill in skillsTextParts)
            {
                List<string> parts = skill.Split('^').ToList();
                skills.Add(new Skill() { Name = parts[0], Time = int.Parse(parts[1]) });
            }

            return skills;
        }


        //Проверка достаточно ли навыков у пользователя для резюме по листу его навыков и листу необходимых навыков
        bool IsEnoughSkills(List<Skill> userSkills, List<Skill> requiredSkills)
        {
            bool isEnough = true;

            foreach (Skill skill in requiredSkills)
            {
                if (requiredSkills.Select(i => i.Name).Contains(skill.Name))
                {
                    Skill userCurrentSkill = userSkills.First(i => i.Name == skill.Name);
                    Skill requiredCurrentSkill = requiredSkills.First(i => i.Name == skill.Name);
                    if (userCurrentSkill.Time < requiredCurrentSkill.Time)
                    {
                        isEnough = false;
                        break;
                    }
                }
                else
                {
                    isEnough = false;
                    break;
                }
            }

            return isEnough;
        }




        //Возвращает строку навыков пользователя с новым навыком
        string AddSkill(string userSkills, Skill skill)
        {
            return userSkills + "|" + skill.Name + "^" + skill.Time;
        }


        //Возвращает строку навыков с измененным стажем 
        string ChangeSkillTime(string userSkills, Skill skill)
        {
            string changedSkills = "";

            List<string> skillsTextParts = userSkills.Split('|').ToList();
            foreach (string skillText in skillsTextParts)
            {
                List<string> parts = skillText.Split('^').ToList();
                Skill currentSkill = new Skill() { Name = parts[0], Time = int.Parse(parts[1]) };

                if (currentSkill.Name == skill.Name)
                {
                    changedSkills += "|" + skill.DBFormat();
                }
                else
                {
                    changedSkills += "|" + currentSkill.DBFormat();
                }
            }

            return changedSkills.TrimStart('|');
        }


        //Возвращает строку навыков с удаленным навыком
        string DeleteSkill(string userSkills, string skillName)
        {
            string changedSkills = "";

            List<string> skillsTextParts = userSkills.Split('|').ToList();
            foreach (string skillText in skillsTextParts)
            {
                List<string> parts = skillText.Split('^').ToList();
                Skill currentSkill = new Skill() { Name = parts[0], Time = int.Parse(parts[1]) };

                if (currentSkill.Name != skillName)
                {
                    changedSkills += "|" + currentSkill.DBFormat();
                }
            }

            return changedSkills.TrimStart('|');
        }


        //Возвращает навыки в формате поля базы данных
        string SkillsIntoDBFormat(List<Skill> skills)
        {
            string result = "";
            skills.ForEach(i => result += "|" + i.DBFormat());
            return result.TrimStart('|');
        }




        //Возвращает пользователю навыки в понятном ему формате 
        string SkillsIntoText(List<Skill> skills)
        {
            string result = "";
            skills.ForEach(i => result += i.ReadSkill() + "\n");
            return result;
        }

        string SkillsIntoText(string skills)
        {
            string result = "";
            GetSkills(skills).ForEach(i => result += i.ReadSkill() + "\n");
            return result;
        }
    }

    class Skill
    {
        public string Name { get; set; }
        public int Time { get; set; }

        public Skill() { }

        public string ReadSkill() { return Name + " - " + Time + " years"; }
        public string DBFormat() { return Name + "^" + Time; }
    }
}
