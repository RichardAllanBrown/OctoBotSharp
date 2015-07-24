using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctoBotSharp.Service.Parser.Core
{
    public class ParserResult
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public ParseTree ParserTree { get; set; }

        public static ParserResult Failure(string error)
        {
            return new ParserResult()
            {
                Success = false,
                Error = error,
                ParserTree = null,
            };
        }

        public static ParserResult Ok(ParseTree tree)
        {
            return new ParserResult()
            {
                Success = true,
                Error = null,
                ParserTree = tree,
            };
        }
    }
}
