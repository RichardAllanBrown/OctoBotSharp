using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OctoBotSharp.Service.Interp;
using OctoBotSharp.Service.Interp.Core;

namespace OctoBotSharp.Service.Test.ParserTest
{
    [TestClass]
    public class InterpreterTest
    {
        private IInterpreter _interpreter;

        [TestInitialize]
        public void SetUp()
        {
            var lexer = Lexer.CreateDefault();
            var parser = new Parser();
            var funcFactory = FunctionFactory.Create(x => Activator.CreateInstance(x));
            var executor = new Executor(funcFactory);

            _interpreter = new Interpreter(lexer, parser, executor);
        }

        [TestMethod]
        [TestProperty("type", "intergration")]
        public void InterpreterTest_SimpleAddition()
        {
            var code = "(add 2 4 7)";

            var result = _interpreter.RunScript(code);

            Assert.AreEqual("13", result[0], "A simple script to add numbers should return the result");
        }

        [TestMethod]
        [TestProperty("type", "intergration")]
        public void InterpreterTest_NestedAddition()
        {
            var code = "(add 2 12 (add 4 7 2))";

            var result = _interpreter.RunScript(code);

            Assert.AreEqual("27", result[0], "A simple script to add numbers should return the result");
        }
    }
}
