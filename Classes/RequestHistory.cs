using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP._01.Classes
{
    public class RequestHistory
    {
        public int ID {  get; set; }
        public Classes.Request Request { get; set; }
        public User Performer { get; set; }
        public string Comment {  get; set; }
    }
}
