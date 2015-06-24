using FluentValidation;
using Microsoft.Practices.Unity;
using OctoBotSharp.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Validation
{
    public class UnityValidatorFactory : IValidatorFactory
    {
        private readonly IUnityContainer _container;

        public UnityValidatorFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IValidator GetValidator(Type type)
        {
            var genericValidatorType = typeof(IValidator<>);
            var targetType = genericValidatorType.MakeGenericType(type);

            if (!_container.Registrations.Any(x => x.RegisteredType == targetType))
                return null;

            return _container.Resolve(targetType) as IValidator;
        }

        public IValidator<T> GetValidator<T>()
        {
            if (!_container.Registrations.Any(x => x.RegisteredType == typeof(IValidator<T>)))
                return null;

            return _container.Resolve<IValidator<T>>();
        }
    }
}