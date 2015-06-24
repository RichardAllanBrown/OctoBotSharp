using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Areas.Admin.Models
{
    public class ItemDefinitionSummaryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string IrcChar { get; set; }

        public string IrcHexColor { get; set; }
    }
}