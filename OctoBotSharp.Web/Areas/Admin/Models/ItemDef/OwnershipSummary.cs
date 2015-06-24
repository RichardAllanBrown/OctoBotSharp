using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctoBotSharp.Web.Areas.Admin.Models
{
    public class OwnershipSummary
    {
        public string UserName { get; set; }

        public DateTimeOffset ObtainedAt { get; set; }
    }
}
