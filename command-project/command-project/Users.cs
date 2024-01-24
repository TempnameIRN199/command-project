using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command_project
{
    internal class Users
    {
        //айди юзера в таблице пользователей
        private int Id { get; set; }
        private string Name { get; set; }
        private string SecondName { get; set; }
        private int Age { get; set; }
        //айди юзера в приложении
        private string UserId { get; set; }
        private string Country { get; set; }
        private string City { get; set; }
        //кто пользователь(Worker,Employer,Admin)
        private string Status { get; set; }
        //номер телефона
        private string PhoneNumber { get; set; }

    }
}
