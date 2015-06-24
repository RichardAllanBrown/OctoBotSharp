using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Models.Members
{
    public class UserSummary
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public decimal CurrentWealth { get; set; }
    }
}