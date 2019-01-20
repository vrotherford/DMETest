using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class User
    {
        public Guid id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string thumbnail { get; set; }
        public string largeSizePhoto { get; set; }
        public string password { get; set; }
        public DateTime birth { get; set; }

    }
}
