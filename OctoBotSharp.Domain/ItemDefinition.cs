using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Domain
{
    public class ItemDefinition
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string IrcRenderChar { get; set; }

        public string IrcRenderColor { get; set; }

        public bool CanPurchase { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public virtual ICollection<ItemInstance> OwnedBy { get; set; }

        public static ItemDefinition Create()
        {
            return new ItemDefinition()
            {
                OwnedBy = new List<ItemInstance>(),
                CreatedOn = DateTimeOffset.Now,
            };
        }
    }
}
