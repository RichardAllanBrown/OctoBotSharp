using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Interp.Core
{
    public class TokenDefinition
    {
        private readonly Regex _regex;
        public Regex Regex { get { return _regex; } }

        private readonly Token _token;
        public Token Token { get { return _token; } }

        private readonly bool _ignored;
        public bool Ignored { get { return _ignored; } }

        public TokenDefinition(string regex, Token token, TokenDefinitionSettings settings = TokenDefinitionSettings.Default)
        {
            _regex = new Regex(string.Format("^{0}", regex));
            _token = token;
            _ignored = settings.HasFlag(TokenDefinitionSettings.LexerIgnore);
        }
    }

    public enum TokenDefinitionSettings : byte
    {
        Default = 0,

        LexerIgnore = 1,
    }
}
