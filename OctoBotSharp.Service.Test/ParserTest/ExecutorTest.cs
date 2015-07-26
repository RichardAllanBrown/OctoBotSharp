using System;
using System.Linq;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OctoBotSharp.Service.Interp.Core;
using OctoBotSharp.Service.Interp.Functions;

namespace OctoBotSharp.Service.Test.ParserTest
{
    [TestClass]
    public class ExecutorTest
    {
        private IExecutor _executor;

        [TestInitialize]
        public void SetUp()
        {
            var mockFuncFactory = new Mock<IFunctionFactory>();
            mockFuncFactory.Setup(x => x.Build(It.Is<string>(functionName => functionName == "rand"))).Returns(new RandFunction());
            mockFuncFactory.Setup(x => x.Build(It.Is<string>(functionName => functionName == "add"))).Returns(new AddFunction());

            _executor = new Executor(mockFuncFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ExecutorException))]
        public void Executor_EmptyTree_ThrowsException()
        {
            var result = _executor.Execute(new ParseTree());
        }

        [TestMethod]
        public void Executor_ValueOnly_ReturnsValue()
        {
            var valNode = new ValueNode("simple", Token.Word);

            var tree = new ParseTree();
            tree.Root.AddChild(valNode);

            var result = _executor.Execute(tree);

            Assert.AreEqual(valNode.Value, result.Response.First(), "When only given a value node, then the value should simple be returned as the result");
        }

        [TestMethod]
        public void Executor_Function_ReturnsAValue()
        {
            var tree = new ParseTree();
            tree.Root.AddChild(GetRandNode());

            var result = _executor.Execute(tree);

            Assert.IsTrue(result.Success, "Should be able to process simple funcs such as rand");
        }

        private FunctionNode GetRandNode()
        {
            var funcNode = new FunctionNode("rand");
            funcNode.AddChild(new ValueNode("5", Token.Int));

            return funcNode;
        }

        [TestMethod]
        public void Executor_NestedFunctions_One_Success()
        {
            var tree = new ParseTree();
            var addNode = new FunctionNode("add");
            addNode.AddChild(GetRandNode());
            addNode.AddChild(new ValueNode("3", Token.Int));
            tree.Root.AddChild(addNode);

            var result = _executor.Execute(tree);

            Assert.IsTrue(result.Success, "Should be able to process nested functions");
        }

        [TestMethod]
        public void Executor_NestedFunctions_Many_Success()
        {
            var tree = new ParseTree();
            var addNode = new FunctionNode("add");
            addNode.AddChild(GetRandNode());
            addNode.AddChild(GetRandNode());
            addNode.AddChild(GetRandNode());
            addNode.AddChild(new ValueNode("3", Token.Int));
            tree.Root.AddChild(addNode);

            var result = _executor.Execute(tree);

            Assert.IsTrue(result.Success, "Should be able to process many nested functions");
        }
    }
}
