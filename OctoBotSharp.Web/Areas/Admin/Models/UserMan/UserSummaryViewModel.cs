using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Areas.Admin.Models
{
    public class UserSummaryViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public int Id { get; set; }
    }
}