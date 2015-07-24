using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctoBotSharp.Service.Parser.Functions
{
    public class FunctionNames : Attribute
    {
        public string[] Names { get; set; }

        public FunctionNames(params string[] names)
        {
            Names = names;
        }
    }
}
