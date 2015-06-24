using FluentValidation;
using OctoBotSharp.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Areas.Admin.Validation
{
    public class GlobalConfigViewModelValidator : AbstractValidator<GlobalConfigViewModel>
    {
        public GlobalConfigViewModelValidator()
        {
            RuleFor(x => x.MoneyChar)
                .Length(1);
        }
    }
}