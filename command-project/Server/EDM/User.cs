using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work.EDM
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }        
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        //public int Age { get; set; }
        // status = (Worker,Employer,Admin)
        public string Status { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<CV> CVs { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public User()
        {
            CVs = new List<CV>();
            Requests = new List<Request>();
        }
    }
}
