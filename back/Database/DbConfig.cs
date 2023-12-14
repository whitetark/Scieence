using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DbConfig
    {
        public string User_Database_Name { get; set; }
        public string Accounts_Collection_Name { get; set; }
        public string Connection_String { get; set; }
        public string Pub_Database_Connection { get; set; }
    }
}
