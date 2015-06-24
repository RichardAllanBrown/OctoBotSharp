using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Areas.Admin.Models
{
    public class ItemDefinitionDetailViewModel : ItemDefinitionSummaryViewModel
    {
        public DateTimeOffset CreatedOn { get; set; }

        public OwnershipSummary[] Owners { get; set; }
    }
}