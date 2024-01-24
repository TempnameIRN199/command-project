using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command_project
{
    //когда юзер принимает вакансию
    //если что,работодатель есть в вакансии(он ее создавал)
    internal class RequestUser
    {
        private int Id { get; set; }
        //работник,который принял вакансию
        private Users Worker { get; set; }
        //какую именно вакансию
        private Requests Request { get; set; }
        //когда работник принял вакансию
        private DateTime RequestDate { get; set; }
        //принял ли работодатель работника(3 варианта:Accepted UnderReview Denied)
        private string Status { get; set; }
    }
}
