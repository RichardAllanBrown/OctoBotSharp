using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Parser.Core
{
    public enum Token
    {
        QuotedString,
        Float,
        Int,
        Space,
        OpenBracket,
        CloseBracket,
        Word,
    }
}
