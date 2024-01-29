using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command_project.EDM
{
    public class RequestCV
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        public int CVId { get; set; }
        public virtual CV CV { get; set; }
        public int RequestId { get; set; }
        public virtual Request Request { get; set; }
    }
}
