using AutoMapper;
using OctoBotSharp.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace OctoBotSharp.Web.Infrastructure
{
    public static class AutoMapperExtentions
    {
        public static IResolverConfigurationExpression<TSource, TResolver> ConstructedByUnity<TSource, TResolver>(this IResolverConfigurationExpression<TSource, TResolver> source) where TResolver : IValueResolver
        {
            return source.ConstructedBy(() => UnityConfig.GetConfiguredContainer().Resolve<TResolver>());
        }
    }
}
