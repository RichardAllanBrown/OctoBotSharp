using OctoBotSharp.Service.Interp.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Interp.Core
{
    public interface IFunctionFactory
    {
        FunctionBase Build(string functionName);
    }

    public class FunctionFactory : IFunctionFactory
    {
        private readonly IDictionary<string, Type> _funcs;
        private readonly Func<Type, object> _builder;

        private FunctionFactory(IDictionary<string, Type> funcs, Func<Type, object> builder)
        {
            _funcs = funcs;
            _builder = builder;
        }

        public FunctionBase Build(string functionName)
        {
            if (!_funcs.ContainsKey(functionName))
                throw new ExecutorException(string.Format("{0} is not a recognised function", functionName));

            var type = _funcs[functionName];

            return (FunctionBase)_builder.Invoke(type);
        }
        
        public static FunctionFactory Create(Func<Type, object> buildFunction)
        {
            var functionNameDict = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);

            var functionTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.IsSubclassOf(typeof(FunctionBase)));

            foreach (var funcType in functionTypes)
            {
                var attr = funcType.GetCustomAttribute<FunctionNames>();
                if (attr != null)
                {
                    foreach (var name in attr.Names)
                        functionNameDict.Add(name, funcType);
                }
            }

            return new FunctionFactory(functionNameDict, buildFunction);
        }
    }
}
