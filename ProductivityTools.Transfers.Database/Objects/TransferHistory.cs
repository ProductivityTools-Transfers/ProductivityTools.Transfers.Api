using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Transfers.Database.Objects
{
    public class TransferHistory
    {
        public int TransferHistoryId { get; set; }
        public DateTime? Date { get; set; }
        public string? Source { get; set; }
        public string? Target { get; set; }
        public decimal? Value { get; set; }
        public string? ValueComment { get; set; }
    }
}
