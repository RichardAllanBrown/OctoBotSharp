using AutoMapper;
using OctoBotSharp.Domain;
using OctoBotSharp.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Areas.Admin.Mappings
{
    public class UserTransactionsResolver : ValueResolver<OctoUser, TransactionSummary[]>
    {
        protected override TransactionSummary[] ResolveCore(OctoUser source)
        {
            var debitSummary = source.Debits.Select(x => new TransactionSummary()
                {
                    Change = -x.Amount,
                    Reason = x.Reason,
                    RecorededAt = x.CreatedAt,
                    Type = x.Code.ToString(),
                });

            var creditSummary = source.Credits.Select(x => new TransactionSummary()
                {
                    Change = x.Amount,
                    Reason = x.Reason,
                    RecorededAt = x.CreatedAt,
                    Type = x.Code.ToString(),
                });

            return debitSummary.Concat(creditSummary).ToArray();
        }
    }
}