using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP._01.Classes
{
    public class Request
    {
        public int ID {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Equipment Equipment { get; set; }
        public TypeOfProblem TypeOfProblem { get; set; }
        public string Description { get; set; }
        public User Client { get; set; }
        public User Performer { get; set; }
        public User Manager { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }
    }
}
