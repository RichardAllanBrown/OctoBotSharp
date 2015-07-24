using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Parser.Functions
{
    [FunctionNames("add")]
    public class AddFunction : FunctionBase
    {
        public override FunctionResult Invoke(InvocContext context, Arguments args)
        {
            if (args.Length <= 0)
                return FunctionResult.Fail("Function requires at least one argument");

            if (args.AllCouldBeCastAsInt())
            {
                var result = Enumerable.Range(0, args.Length).Sum(x => args.GetAsInt(x));

                return FunctionResult.Success(result, Core.Token.Int);
            }

            if (args.AllCouldBeCastAsDouble())
            {
                var result = Enumerable.Range(0, args.Length).Sum(x => args.GetAsDouble(x));

                return FunctionResult.Success(result, Core.Token.Float);
            }

            return FunctionResult.Fail("Cannot call method with arguments");
        }
    }
}
