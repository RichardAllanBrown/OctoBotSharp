using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Domain
{
    public class ItemInstance
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public virtual OctoUser Owner { get; set; }

        public int ItemId { get; set; }

        public virtual ItemDefinition Item { get; set; }

        public virtual ICollection<ItemOwnershipHistory> History { get; set; }


        public static ItemInstance Create(OctoUser owner, ItemDefinition item, string reason)
        {
            return new ItemInstance()
            {
                Owner = owner,
                Item = item,
                History = new List<ItemOwnershipHistory>()
                {
                    new ItemOwnershipHistory()
                    {
                        Reason = reason,
                        ReceivedAt = DateTimeOffset.Now,
                        Receiver = owner,
                    }
                },
            };
        }
    }
}
