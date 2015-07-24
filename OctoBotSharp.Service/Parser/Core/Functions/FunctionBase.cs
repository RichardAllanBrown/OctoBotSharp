using OctoBotSharp.Domain;
using OctoBotSharp.Service.Parser.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Parser.Functions
{
    public abstract class FunctionBase
    {
        public FunctionResult Invoke(IEnumerable<ResultNode> values)
        {
            var context = new InvocContext();
            var arguments = new Arguments(values.ToArray());

            return Invoke(context, arguments);
        }

        public abstract FunctionResult Invoke(InvocContext context, Arguments args);
    }
}
