using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Parser.Core
{
    public interface IExecutor
    {
        ExecutionResult Execute(ParseTree tree);
    }

    public class Executor : IExecutor
    {
        private readonly IFunctionFactory _funcFactory;

        public Executor(IFunctionFactory funcFactory)
        {
            _funcFactory = funcFactory;
        }

        public ExecutionResult Execute(ParseTree tree)
        {
            var result = SolveNode(tree.Root);

            return ExecutionResult.Ok(new string[] { result.DisplayMessage });
        }

        private ResultNode SolveNode(AbstractParseNode node)
        {
            if (node is FunctionNode)
            {
                for (int i = 0; i < node.Children.Count; i++)
                    node.Children[i] = SolveNode(node.Children[i]);

                return SolveFunctionNode((FunctionNode)node);
            }
            else if (node is ValueNode)
            {
                return GetResultNode((ValueNode)node);
            }
            else if (node is RootNode)
            {
                var firstChild = node.Children.FirstOrDefault();
                if (firstChild == null)
                    throw new InvalidOperationException("Asked to solve an empty tree!");

                return SolveNode(firstChild);
            }

            throw new ArgumentOutOfRangeException("Unrecognized node type!");
        }

        private ResultNode SolveFunctionNode(FunctionNode node)
        {
            if (!node.Children.All(x => x is ResultNode))
                throw new InvalidOperationException("Should not be asked to solve a function node where it has any non result node children");

            var funcClass = _funcFactory.Build(node.Value);

            var result = funcClass.Invoke(node.Children.Cast<ResultNode>());
            if (result.IsSuccess)
                return new ResultNode(result.Value.ToString(), result.Token, result.Message);

            throw new InvalidOperationException("Funcation call failure");
        }

        private ResultNode GetResultNode(ValueNode node)
        {
            return new ResultNode(node.Value, node.Type, node.Value);
        }
    }
}
