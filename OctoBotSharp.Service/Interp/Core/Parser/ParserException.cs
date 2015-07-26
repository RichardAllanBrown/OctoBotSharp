using OctoBotSharp.Service.Interp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Interp.Core
{
    public class ParserException : InterpreterException, ISerializable
    {
        private const string PhaseName = "Parser";

        public ParserException()
            : base()
        {
        }

        public ParserException(string message)
            : base(PhaseName, message)
        {
        }

        public ParserException(string message, Exception inner)
            : base(PhaseName, message, inner)
        {
        }

        protected ParserException(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }
    }
}
