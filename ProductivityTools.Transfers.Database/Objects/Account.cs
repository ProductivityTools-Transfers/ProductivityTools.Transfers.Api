using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductivityTools.Transfers.Database.Objects
{
    public class Account
    {
        public int? AccountId { get; set; }
        public string Name { get; set; }
        public decimal Pillow { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }

        [JsonIgnore]
        public ICollection<Transfer>? TransfersSource { get; set; }
        [JsonIgnore]
        public ICollection<Transfer>? TransfersTarget { get; set; }
    }
}
