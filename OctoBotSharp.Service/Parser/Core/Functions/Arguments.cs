using OctoBotSharp.Service.Parser.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctoBotSharp.Service.Parser.Functions
{
    public class Arguments
    {
        private ResultNode[] _argNodes;

        public Arguments(ResultNode[] argNodes)
        {
            _argNodes = argNodes;
        }

        public int Length
        {
            get
            {
                return _argNodes.Length;
            }
        }

        public bool CouldBeCastAsInt(int pos)
        {
            return NodeIsOneOf(pos, Token.Int, Token.Float);
        }

        private bool NodeIsOneOf(int pos, params Token[] types)
        {
            return types.Contains(_argNodes[pos].Type);
        }

        public bool CouldBeCastAsDouble(int pos)
        {
            return NodeIsOneOf(pos, Token.Int, Token.Float);
        }

        public bool AllCouldBeCastAsInt()
        {
            return Enumerable.Range(0, _argNodes.Length).All(CouldBeCastAsInt);
        }

        public bool AllCouldBeCastAsDouble()
        {
            return Enumerable.Range(0, _argNodes.Length).All(CouldBeCastAsDouble);
        }

        public int GetAsInt(int pos)
        {
            // TODO: Check that it isn't truly a double?
            return Convert.ToInt32(_argNodes[pos].Value);
        }

        public string GetAsString(int pos)
        {
            return GetStringValue(_argNodes[pos]);
        }

        public double GetAsDouble(int pos)
        {
            return Convert.ToDouble(_argNodes[pos].Value);
        }

        public string GetAsStringToEnd(int fromPos)
        {
            var strings = _argNodes.Skip(fromPos - 1).Select(GetStringValue);

            return string.Join(" ", strings);
        }

        private string GetStringValue(ResultNode arg)
        {
            if (arg.Type == Token.QuotedString)
                return arg.Value.Substring(1, arg.Value.Length - 2);

            return arg.Value;
        }
    }
}
