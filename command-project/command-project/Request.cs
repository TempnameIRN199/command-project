using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command_project
{
    //шаблон для вакансий
    internal class Requests
    {
        private int Id { get; set; }
        //работадатель, который оставил вакансию
        private Users Employer { get; set; }
        //когда выставили вакансию
        private DateTime RequestTime { get; set; }
        //описание вакансии
        private string RequstInfo { get; set; }

    }
}
