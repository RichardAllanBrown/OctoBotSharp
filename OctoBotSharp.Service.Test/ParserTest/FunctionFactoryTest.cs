using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OctoBotSharp.Service.Interp.Core;
using OctoBotSharp.Service.Interp.Functions;

namespace OctoBotSharp.Service.Test.ParserTest
{
    [TestClass]
    public class FunctionFactoryTest
    {
        private IFunctionFactory _factory;

        [TestInitialize]
        public void Setup()
        {
            _factory = FunctionFactory.Create(x => Activator.CreateInstance(x));
        }

        [TestMethod]
        public void FuncFactory_BuildsRandFunc()
        {
            var instance = _factory.Build("rand");

            Assert.AreEqual(typeof(RandFunction), instance.GetType(), "Factory should return correct type of function object");
        }

        [TestMethod]
        [ExpectedException(typeof(ExecutorException))]
        public void FuncFactory_BuildUnrecognisedFunction_ThrowsException()
        {
            var instance = _factory.Build("UnknownFunctionName");
        }
    }
}
