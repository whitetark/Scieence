using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DbConfig
    {
        public required string User_Database_Name { get; set; }
        public required string Accounts_Collection_Name { get; set; }
        public required string Connection_String { get; set; }
        public required string Pub_Database_Connection { get; set; }
    }
}
