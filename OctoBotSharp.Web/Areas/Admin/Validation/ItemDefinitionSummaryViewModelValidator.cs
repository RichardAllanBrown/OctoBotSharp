using FluentValidation;
using OctoBotSharp.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Areas.Admin.Validation
{
    public class ItemDefinitionViewModelValidator : AbstractValidator<ItemDefinitionViewModel>
    {
        public ItemDefinitionViewModelValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.IrcChar)
                .NotEmpty();

            RuleFor(x => x.IrcColour)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}