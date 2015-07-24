using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Parser.Functions
{
    [FunctionNames("Rand", "Random")]
    public class RandFunction : FunctionBase
    {
        private readonly Random _rand = new Random();

        public override FunctionResult Invoke(InvocContext context, Arguments args)
        {
            if (args.Length != 1)
            {
                return FunctionResult.Fail("Unrecognised number of params");
            }

            if (args.CouldBeCastAsInt(0))
            {
                var result = _rand.Next(args.GetAsInt(0));

                return FunctionResult.Success(result, Core.Token.Int);
            }

            if (args.CouldBeCastAsDouble(0))
            {
                var result = _rand.NextDouble() * args.GetAsDouble(0);

                return FunctionResult.Success(result, Core.Token.Float);
            }

            return FunctionResult.Fail("Type cannot be used as argument");            
        }
    }
}
