using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Interp.Core
{
    public interface IParser
    {
        ParseTree BuildTree(IEnumerable<Match> matches);
    }

    public class Parser : IParser
    {
        public ParseTree BuildTree(IEnumerable<Match> matches)
        {
            return ProcessMatches(matches);
        }

        private ParseTree ProcessMatches(IEnumerable<Match> matches)
        {
            var result = new ParseTree();

            var currentPos = result.Root;
            var expectFunc = false;
            var bracketCount = 0;

            foreach (var match in matches)
            {
                if (match.Token == Token.OpenBracket)
                {
                    if (expectFunc)
                        throw new ParserException("Expected function name after open bracket");

                    expectFunc = true;
                    bracketCount++;
                }
                else if (match.Token == Token.Word && expectFunc)
                {
                    var newNode = new FunctionNode(match.Value);
                    currentPos.AddChild(newNode);
                    currentPos = newNode;
                    expectFunc = false;
                }
                else if (match.Token == Token.CloseBracket)
                {
                    currentPos = currentPos.Parent;
                    expectFunc = false;
                    bracketCount--;

                    if (bracketCount < 0)
                        throw new ParserException("Inconsistent brackets");
                }
                else
                {
                    currentPos.AddChild(new ValueNode(match.Value, match.Token));
                    expectFunc = false;
                }
            }

            if (0 < bracketCount)
                throw new ParserException("Missing close brackets");

            return result;
        }
    }
}
