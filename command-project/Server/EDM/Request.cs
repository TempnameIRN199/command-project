using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work.EDM
{
    public class Request
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        public string RequestInfo { get; set; }
        public string Skills { get; set; }
        public virtual ICollection<RequestCV> RequestCVs { get; set; }
        public Request() 
        {
            RequestCVs = new List<RequestCV>();
        }

    }
}
