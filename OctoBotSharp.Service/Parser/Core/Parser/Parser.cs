using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Parser.Core
{
    public interface IParser
    {
        ParserResult BuildTree(IEnumerable<Match> matches);
    }

    public class Parser : IParser
    {
        public ParserResult BuildTree(IEnumerable<Match> matches)
        {
            try
            {
                var result = ProcessMatches(matches);

                return ParserResult.Ok(result);
            }
            catch (Exception ex)
            {
                return ParserResult.Failure(ex.Message);
            }
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
                        throw new InvalidOperationException("Cannot have consecutive open brackets");

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
                        throw new InvalidOperationException("Expected open bracket");
                }
                else
                {
                    currentPos.AddChild(new ValueNode(match.Value, match.Token));
                    expectFunc = false;
                }
            }

            if (0 < bracketCount)
                throw new InvalidOperationException("Expected additional close brackets");

            return result;
        }

        private string GetValue(Match match)
        {
            return match.Value;
        }
    }
}
