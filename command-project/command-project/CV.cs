using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command_project
{
    //резюме (Curriculum vitae)
    internal class CV
    {
        //айди в таблице
        private int Id { get; set; }
        //юзер, который сделал резюме
        private Users user { get; set; }
        //когда было сделано резюме
        private DateTime CVDate { get; set; }
        //пример C#^5|SQL^2 (сначала идет язык, знак ^ и цифра после него - сколько лет у юзера стаж работы с этим языком)
        private string Skills { get; set; }
        //юзер пишет инфо про себя(то что он хочет сказать работадателю)
        private string UserInfo { get; set; }
    }
}
