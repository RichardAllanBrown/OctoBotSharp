using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OctoBotSharp.Service.Interp.Core;
using System.Collections.Generic;

namespace OctoBotSharp.Service.Test.ParserTest
{
    [TestClass]
    public class LexerTest
    {
        [TestMethod]
        public void Lexer_EmptyString_ShouldReturnOneTokenOfEnd()
        {
            var lexer = new Lexer(Enumerable.Empty<TokenDefinition>());

            var result = lexer.Tokenise("");

            AssertTokens("Empty string should have no tokens", result);
        }

        private void AssertTokens(string message, IEnumerable<Match> LexerResult, params Token[] expectedTokens)
        {
            CollectionAssert.AreEqual(expectedTokens, LexerResult.Select(x => x.Token).ToArray(), message);
        }

        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        public void Lexer_NoMatchingTokens_ShouldThrowException()
        {
            var lexer = new Lexer(Enumerable.Empty<TokenDefinition>());

            var result = lexer.Tokenise("   ").ToArray();

            Assert.Fail("Exception should have been thrown");
        }

        [TestMethod]
        public void Lexer_CanMatchAToken_ShouldReturnAppropriateTokens()
        {
            var def = new[] { new TokenDefinition(@"[-+]?\d+", Token.Int) };
            var lexer = new Lexer(def);

            var result = lexer.Tokenise("4");

            AssertTokens("String with only one int should only have int token", result, Token.Int);
        }

        [TestMethod]
        public void Lexer_DoesNotReturnIgnoreTokens_ReturnsRightTokens()
        {
            var def = new[] { new TokenDefinition(@"[-+]?\d+", Token.Int, TokenDefinitionSettings.LexerIgnore) };
            var lexer = new Lexer(def);

            var result = lexer.Tokenise("1");

            AssertTokens("Lexer should not return token when it's set to ignore", result);
        }

        [TestMethod]
        public void Lexer_CanHandleMultipleTokenDefs()
        {
            var def = new[]
            { 
                new TokenDefinition(@"[-+]?\d+", Token.Int, TokenDefinitionSettings.LexerIgnore),
                new TokenDefinition(@"\(", Token.OpenBracket),
                new TokenDefinition(@"\)", Token.CloseBracket),
            };

            var lexer = new Lexer(def);

            var result = lexer.Tokenise("(3)23(())");
            var expected = new [] { Token.OpenBracket, Token.CloseBracket, Token.OpenBracket, Token.OpenBracket, Token.CloseBracket, Token.CloseBracket };

            AssertTokens("Brackets should be lexed", result, expected);
        }

        [TestMethod]
        public void Lexer_DefaultImp_SimpleCase()
        {
            var lexer = Lexer.CreateDefault();

            var result = lexer.Tokenise("(tip 2 5.3 rokusho 'Here is a message!')");

            AssertTokens("Simple default imp should return valid tokens", result, Token.OpenBracket, Token.Word, Token.Int, Token.Float, Token.Word, Token.QuotedString, Token.CloseBracket);
            AssertValues("Values of tokens should be correct", result, "(", "tip", "2", "5.3", "rokusho", "'Here is a message!'", ")");
        }

        private void AssertValues(string message, IEnumerable<Match> result, params string[] expectedValues)
        {
            CollectionAssert.AreEquivalent(result.Select(x => x.Value).ToArray(), expectedValues, message);
        }
    }
}
