using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Interp.Core
{
    public class Match
    {
        private readonly Token _token;
        public Token Token { get { return _token; } }

        private readonly string _value;
        public string Value { get { return _value; } }

        public Match(Token token, string value)
        {
            _token = token;
            _value = value;
        }
    }
}
