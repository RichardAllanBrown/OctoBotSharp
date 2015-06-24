using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Areas.Admin.Models
{
    public class TransactionSummary
    {
        public string Type { get; set; }

        public string Reason { get; set; }

        public decimal Change { get; set; }

        public DateTimeOffset RecorededAt { get; set; }
    }
}
