using OctoBotSharp.Data.Repository;
using OctoBotSharp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service
{
    public interface ITransactionService
    {
        ServiceOperationResult TipUser(OctoUser from, OctoUser to, decimal amount, string reason);
        ServiceOperationResult AdjustBalance(OctoUser to, decimal amount, string reason);
    }

    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _repo;

        public TransactionService(IRepository<Transaction> repo)
        {
            _repo = repo;
        }

        public ServiceOperationResult TipUser(OctoUser from, OctoUser to, decimal amount, string reason)
        {
            if (amount <= 0)
                return ServiceOperationResult.Fail("Amount must be greater than 0");

            if (from.Money < amount)
                return ServiceOperationResult.Fail("Not enough money");

            from.Money -= amount;
            to.Money += amount;

            var trnx = new Transaction()
            {
                Amount = amount,
                CreditedUser = to,
                DebitedUser = from,
                CreatedAt = DateTimeOffset.Now,
                Code = TransactionCode.Tip,
                Reason = reason ?? "Tip",
            };

            _repo.Insert(trnx);

            return ServiceOperationResult.Ok;
        }

        public ServiceOperationResult AdjustBalance(OctoUser user, decimal amount, string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                return ServiceOperationResult.Fail("A reason must be specified");

            if (amount == 0)
                return ServiceOperationResult.Fail("Cannot adjust a users balance by zero");

            if (user.Money + amount < 0)
                return ServiceOperationResult.Fail("Cannot award money that would push user into a negative balance");

            user.Money += amount;

            var trnx = new Transaction()
            {
                Amount = Math.Abs(amount),
                Code = TransactionCode.Adjustment,
                Reason = reason,
                CreatedAt = DateTimeOffset.Now,
            };

            if (amount < 0)
                trnx.DebitedUser = user;
            else
                trnx.CreditedUser = user;

            _repo.Insert(trnx);

            return ServiceOperationResult.Ok;
        }
    }
}
