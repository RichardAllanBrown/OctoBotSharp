using OctoBotSharp.Service.Parser.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Parser
{
    public interface IScriptRunner
    {
        string[] RunScript(string command);
    }

    public class ScriptRunner : IScriptRunner
    {
        private readonly ILexer _lexer;
        private readonly IParser _parser;
        private readonly IExecutor _excutor;

        public ScriptRunner(ILexer lexer, IParser parser, IExecutor executor)
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
            IEnumerable<Match> lexerResult;

            try
            {
                lexerResult = _lexer.Tokenise(command);
                if (!lexerResult.Any())
                    return new string[0];
            }
            catch (Exception ex)
            {
                return FormatError("Lexer", ex.Message);
            }

            var parserResult = _parser.BuildTree(lexerResult);
            if (!parserResult.Success)
                return FormatError("Parser", parserResult.Error);

            var executorResult = _excutor.Execute(parserResult.ParserTree);
            if (!executorResult.Success)
                return FormatError("Execution", executorResult.Error);

            return executorResult.Response;
        }

        private string[] FormatError(string area, string message)
        {
            return new string[] { string.Format("{0} Error: {1}", area, message) };
        }
    }
}
