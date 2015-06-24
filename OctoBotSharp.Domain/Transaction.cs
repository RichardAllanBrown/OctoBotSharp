using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Domain
{
    public class Transaction
    {
        public int Id { get; set; }

        public TransactionCode Code { get; set; }

        public string Reason { get; set; }

        public int? CreditedUserId { get; set; }

        public int? DebitedUserId { get; set; }

        public decimal Amount { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public virtual OctoUser CreditedUser { get; set; }

        public virtual OctoUser DebitedUser { get; set; }
    }

    public enum TransactionCode : byte
    {
        NotSet = 0,

        Tip = 1,

        ItemPurchase = 2,

        Adjustment = 3,
    }
}
