using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Domain
{
    public class ItemOwnershipHistory
    {
        public int Id { get; set; }

        public string Reason { get; set; }

        public int ReceiverId { get; set; }

        public virtual OctoUser Receiver { get; set; }

        public DateTimeOffset ReceivedAt { get; set; }

        public int ItemInstanceId { get; set; }

        public virtual ItemInstance ItemInstance { get; set; }


        public static ItemOwnershipHistory Create(ItemInstance item, OctoUser receiver, string reason)
        {
            return new ItemOwnershipHistory()
            {
                Reason = reason,
                Receiver = receiver,
                ItemInstance = item,
                ReceivedAt = DateTimeOffset.Now,
            };
        }
    }
}
