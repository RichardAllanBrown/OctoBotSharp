using OctoBotSharp.Service.Interp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Interp.Core
{
    public class LexerException : InterpreterException, ISerializable
    {
        private const string PhaseName = "Lexer";

        public LexerException()
            : base()
        {
        }

        public LexerException(string message)
            : base(PhaseName, message)
        {
        }

        public LexerException(string message, Exception inner)
            : base(PhaseName, message, inner)
        {
        }

        protected LexerException(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }
    }
}
