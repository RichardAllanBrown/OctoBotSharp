using FluentValidation.Mvc;
using OctoBotSharp.Web.App_Start.GlobalEventing;
using OctoBotSharp.Web.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctoBotSharp.Web.App_Start
{
    public class FluentValidationInit : IRunAtStartup
    {
        public void Execute()
        {
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            FluentValidationModelValidatorProvider.Configure(provider =>
            {
                provider.ValidatorFactory = new UnityValidatorFactory(UnityConfig.GetConfiguredContainer());
                provider.AddImplicitRequiredValidator = false;
            });
        }
    }
}