using FluentValidation;
using OctoBotSharp.Web.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Validation.Auth
{
    public class NewUserRegistrationValidator : AbstractValidator<NewUserRegistration>
    {
        public NewUserRegistrationValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty();

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty();

            RuleFor(x => x.EmailAddress)
                .NotEmpty();

            RuleFor(x => x.Username)
                .NotEmpty();

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Password and Confirm Password do not match");

            RuleFor(x => x.EmailAddress)
                .EmailAddress();
        }
    }
}
