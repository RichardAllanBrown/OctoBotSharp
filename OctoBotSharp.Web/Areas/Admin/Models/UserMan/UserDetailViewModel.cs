using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Areas.Admin.Models
{
    public class UserDetailViewModel : UserSummaryViewModel
    {
        public string[] Permissions { get; set; }

        public decimal CurrentBalance { get; set; }

        public TransactionSummary[] Transactions { get; set; }
    }
}