﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Transfers.Database.Objects
{
    public class TransferHistory
    {
        public int TransferId { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
