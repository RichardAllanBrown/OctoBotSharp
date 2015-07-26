using OctoBotSharp.Service.Interp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Parser
{
    public interface IInterpreter
    {
        string[] RunScript(string command);
    }

    public class Interpreter : IInterpreter
    {
        private readonly ILexer _lexer;
        private readonly IParser _parser;
        private readonly IExecutor _excutor;

        public Interpreter(ILexer lexer, IParser parser, IExecutor executor)
        {
            if (lexer == null)
                throw new ArgumentNullException("lexer");

            if (parser == null)
                throw new ArgumentNullException("parser");

            if (executor == null)
                throw new ArgumentNullException("executor");

            _lexer = lexer;
            _parser = parser;
            _excutor = executor;
        }

        public string[] RunScript(string command)
        {
            try
            {
                var lexerResult = _lexer.Tokenise(command);
                var parseResult = _parser.BuildTree(lexerResult);
                var executionResult = _excutor.Execute(parseResult);

                return executionResult.Response;
            }
            catch (Exception ex)
            {
                return FormatError(ex.Message);
            }
        }

        private string[] FormatError(string message)
        {
            return new string[] { string.Format("Error: {0}", message) };
        }
    }
}
