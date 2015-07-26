using OctoBotSharp.Service.Interp.Core;
using System.Collections.Generic;
using System.Linq;

namespace OctoBotSharp.Service.Interp.Functions
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
