using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work.EDM
{
    public class CV
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public string Skills { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        public string UserInfo { get; set; }
        public virtual ICollection<RequestCV> RequestCVs { get; set; }
        public CV() 
        {
            RequestCVs = new List<RequestCV>();
        }
    }
}
