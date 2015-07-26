using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OctoBotSharp.Service.Interp.Core;
using OctoBotSharp.Service.Interp.Functions;

namespace OctoBotSharp.Service.Test.ParserTest
{
    [TestClass]
    public class FunctionFactoryTest
    {
        [TestMethod]
        public void FuncFactory_BuildsRandFunc()
        {
            var factory = FunctionFactory.Create(SimpleBuildFunc());

            var instance = factory.Build("rand");

            Assert.AreEqual(typeof(RandFunction), instance.GetType(), "Factory should return correct type of function object");
        }

        private static Func<Type, object> SimpleBuildFunc()
        {
            return x => Activator.CreateInstance(x);
        }
    }
}
