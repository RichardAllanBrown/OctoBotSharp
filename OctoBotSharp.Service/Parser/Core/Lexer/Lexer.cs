using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Parser.Core
{
    public interface ILexer
    {
        IEnumerable<Match> Tokenise(string command);
    }

    public class Lexer : ILexer
    {
        private readonly IEnumerable<TokenDefinition> _tokenDefs;

        public Lexer(IEnumerable<TokenDefinition> tokenDefs)
        {
            _tokenDefs = tokenDefs;
        }

        public IEnumerable<Match> Tokenise(string command)
        {
            var currentIndex = 0;

            while (currentIndex < command.Length)
            {
                TokenDefinition foundDef = null;
                var matchLen = 0;
                var sub = command.Substring(currentIndex);

                foreach (var def in _tokenDefs)
                {
                    var match = def.Regex.Match(sub);
                    if (match.Success && match.Index == 0)
                    {
                        foundDef = def;
                        matchLen = match.Length;
                        break;
                    }
                }

                if (foundDef == null)
                    throw new Exception(string.Format("Unrecognised token {0} at {1}", command[currentIndex], currentIndex));

                var value = command.Substring(currentIndex, matchLen);

                if (!foundDef.Ignored)
                    yield return new Match(foundDef.Token, value);

                currentIndex += matchLen;
            }
        }

        public static Lexer CreateDefault()
        {
            var defaultTokens = new TokenDefinition[]
            {
                new TokenDefinition(@"([""'])(?:\\\1|.)*?\1", Token.QuotedString),
                new TokenDefinition(@"[-+]?\d*\.\d+([eE][-+]?\d+)?", Token.Float),
                new TokenDefinition(@"\s+", Token.Space, TokenDefinitionSettings.LexerIgnore),
                new TokenDefinition(@"[-+]?\d+", Token.Int),
                new TokenDefinition(@"\(", Token.OpenBracket),
                new TokenDefinition(@"\)", Token.CloseBracket),
                new TokenDefinition(@"\S+", Token.Word),
            };

            return new Lexer(defaultTokens);
        }
    }
}
