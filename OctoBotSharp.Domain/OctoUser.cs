using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OctoBotSharp.Domain
{
    public class OctoUser : IdentityUser<int, OctoLogin, OctoUserRole, OctoClaim>
    {
        public decimal Money { get; set; }

        public virtual ICollection<ItemInstance> Items { get; set; }

        public virtual ICollection<Transaction> Debits { get; set; }

        public virtual ICollection<Transaction> Credits { get; set; }

        public virtual ICollection<ItemOwnershipHistory> RecievedItemHistory { get; set; }


        public static OctoUser Create(string email, string username)
        {
            return new OctoUser()
            {
                Email = email,
                EmailConfirmed = false,
                UserName = username,
                Money = 0,
                Items = new List<ItemInstance>(),
                Debits = new List<Transaction>(),
                Credits = new List<Transaction>(),
                RecievedItemHistory = new List<ItemOwnershipHistory>(),
            };
        }
    }
}
