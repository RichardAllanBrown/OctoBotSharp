using FluentValidation;
using OctoBotSharp.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Areas.Admin.Validation
{
    public class UserBalanceAdjustValidator : AbstractValidator<UserBalanceAdjust>
    {
        public UserBalanceAdjustValidator()
        {
            RuleFor(x => x.Amount)
                .NotEmpty();

            RuleFor(x => x.Reason)
                .NotEmpty();
        }
    }
}