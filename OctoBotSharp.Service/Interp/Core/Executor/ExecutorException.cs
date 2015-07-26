using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service.Interp.Core
{
    public class ExecutorException : InterpreterException, ISerializable
    {
        private const string PhaseName = "Execution Engine";

        public ExecutorException()
            : base()
        {
        }

        public ExecutorException(string message)
            : base(PhaseName, message)
        {
        }

        public ExecutorException(string message, Exception inner)
            : base(PhaseName, message, inner)
        {
        }

        protected ExecutorException(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }
    }
}
