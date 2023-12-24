using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class AccountResponse
    {
        public Account account { get; set; }
        public List<DbPublication> publications { get; set; }

        public AccountResponse(Account account, List<DbPublication> publications) {
            this.account = account;
            this.publications = publications;
        }
    }
}
