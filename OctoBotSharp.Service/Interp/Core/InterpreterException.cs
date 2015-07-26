using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Interp.Core
{
    public abstract class InterpreterException : Exception, ISerializable
    {
        protected string InterpreterPhase { get; set; }

        public override string Message
        {
            get
            {
                return string.Format("{0} Error: {1}", InterpreterPhase, base.Message);
            }
        }

        public InterpreterException()
            : base()
        {
        }

        public InterpreterException(string interpreterPhase, string message)
            : base(message)
        {
            InterpreterPhase = interpreterPhase;
        }

        public InterpreterException(string interpreterPhase, string message, Exception inner)
            : base (message, inner)
        {
            InterpreterPhase = interpreterPhase;
        }

        protected InterpreterException(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }
    }
}
