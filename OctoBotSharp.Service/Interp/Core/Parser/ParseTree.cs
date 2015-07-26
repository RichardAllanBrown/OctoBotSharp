using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Interp.Core
{
    public class ParseTree
    {
        private readonly AbstractParseNode _root;
        public AbstractParseNode Root
        {
            get
            {
                return _root;
            }
        }

        public ParseTree()
        {
            _root = new RootNode();
        }

        public void DepthFirstTraversal(Action<AbstractParseNode> action)
        {
            DepthFirstTraversalFrom(action, _root);
        }

        public void DepthFirstTraversalFrom(Action<AbstractParseNode> action, AbstractParseNode fromNode)
        {
            if (fromNode.GetType() != typeof(RootNode))
                action.Invoke(fromNode);

            foreach (var childNode in fromNode.Children)
                DepthFirstTraversalFrom(action, childNode);
        }
    }

    public abstract class AbstractParseNode
    {
        private AbstractParseNode _parent;
        private List<AbstractParseNode> _nodes;
        private string _value;

        public AbstractParseNode Parent { get { return _parent; } }
        public string Value { get { return _value; } }
        public List<AbstractParseNode> Children { get { return _nodes; } }

        public AbstractParseNode(string value)
        {
            _nodes = new List<AbstractParseNode>();
            _value = value;
        }

        public virtual void AddChild(AbstractParseNode child)
        {
            child._parent = this;
            _nodes.Add(child);
        }
    }

    public class RootNode : AbstractParseNode
    {
        public RootNode()
            : base (null)
        {
        }
    }

    public class FunctionNode : AbstractParseNode
    {
        public FunctionNode(string funcName)
            : base (funcName)
        {
        }
    }

    public class ValueNode : AbstractParseNode
    {
        public Token Type { get; private set; }

        public ValueNode(string value, Token type)
            : base (value)
        {
            Type = type;
        }

        public override void AddChild(AbstractParseNode child)
        {
            throw new InvalidOperationException("Cannot all child nodes to values");
        }
    }

    public class ResultNode : ValueNode
    {
        public string DisplayMessage { get; private set; }

        public ResultNode(string value, Token type, string displayMessage)
            : base (value, type)
        {
            DisplayMessage = displayMessage;
        }
    }
}
