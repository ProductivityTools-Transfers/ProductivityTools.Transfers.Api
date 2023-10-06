using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Transfers.Database.Objects
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Name { get; set; }

        public List<Transfer> Transfers { get; set; }
    }
}
