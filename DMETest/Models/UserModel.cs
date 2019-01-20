using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMETest.Models
{
    public class UserModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mediumPhoto { get; set; }
        public string largePhoto { get; set; }
    }
}