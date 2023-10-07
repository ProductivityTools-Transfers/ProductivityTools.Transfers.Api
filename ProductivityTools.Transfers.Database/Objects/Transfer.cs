using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Transfers.Database.Objects
{
    public class Transfer
    {
        public int? TransferId { get; set; }
        public int? SourceId { get; set; }
        public Account? Source { get; set; }
        public int? TargetId { get; set; }
        public Account? Target { get; set; }
        public decimal? Value { get; set; }
    }
}
