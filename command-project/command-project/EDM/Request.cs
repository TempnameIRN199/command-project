using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command_project.EDM
{
    public class Request
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        public string RequestInfo { get; set; }
        public virtual ICollection<RequestCV> RequestCVs { get; set; }
        public Request()
        {
            RequestCVs = new List<RequestCV>();
        }
    }
}
