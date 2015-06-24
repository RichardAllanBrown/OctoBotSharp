using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctoBotSharp.Web.Areas.Admin.Models
{
    public class UserBalanceAdjust
    {
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        public string Reason { get; set; }

        public decimal Amount { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string UserName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public decimal CurrentBalance { get; set; }
    }
}