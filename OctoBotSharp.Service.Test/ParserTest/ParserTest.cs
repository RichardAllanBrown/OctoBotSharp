using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OctoBotSharp.Service.Interp.Core;
using System.Collections.Generic;

namespace OctoBotSharp.Service.Test.ParserTest
{
    [TestClass]
    public class ParserTest
    {
        private IParser _parser;

        [TestInitialize]
        public void SetUp()
        {
            _parser = new Interp.Core.Parser();
        }

        [TestMethod]
        public void Parser_Success_FunctionOnly()
        {
            var tokens = new List<Match>()
            {
                new Match(Token.OpenBracket, "("),
                new Match(Token.Word, "Function"),
                new Match(Token.CloseBracket, ")"),
            };

            var result = _parser.BuildTree(tokens);

            Assert.IsNotNull(result, "Should be able to parse a simple single function expression");
        }

        [TestMethod]
        public void Parser_Success_FunctionWithArgs()
        {
            var tokens = new List<Match>()
            {
                new Match(Token.OpenBracket, "("),
                new Match(Token.Word, "Function"),
                new Match(Token.Int, "5"),
                new Match(Token.Int, "1"),
                new Match(Token.CloseBracket, ")"),
            };

            var result = _parser.BuildTree(tokens);

            Assert.IsNotNull(result, "Should be able to parse a simple single function expression");
        }

        [TestMethod]
        public void Parser_Success_FunctionCanBeNested()
        {
            var tokens = new List<Match>()
            {
                new Match(Token.OpenBracket, "("),
                new Match(Token.Word, "Function"),
                new Match(Token.Int, "5"),
                new Match(Token.OpenBracket, "("),
                new Match(Token.Word, "NestedFunction"),
                new Match(Token.Int, "5"),
                new Match(Token.CloseBracket, ")"),
                new Match(Token.CloseBracket, ")"),
            };

            var result = _parser.BuildTree(tokens);

            Assert.IsNotNull(result, "Should be able to parse a simple single function expression");
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void Parser_Exception_CloseBracketBeforeOpen()
        {
            var tokens = new List<Match>()
            {
                new Match(Token.CloseBracket, ")")
            };

            var result = _parser.BuildTree(tokens);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void Parser_Exception_MissingCloseBracket()
        {
            var tokens = new List<Match>()
            {
                new Match(Token.OpenBracket, "("),
                new Match(Token.Word, "Function"),
                new Match(Token.Int, "5"),
            };

            var result = _parser.BuildTree(tokens);
        }
    }
}
