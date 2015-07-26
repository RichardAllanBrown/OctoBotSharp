using OctoBotSharp.Service.Interp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctoBotSharp.Service.Interp.Functions
{
    public class FunctionResult
    {
        private readonly bool _success;
        private readonly string _message;
        private readonly object _value;
        private readonly Token _token;

        public virtual bool IsSuccess { get { return _success; } }
        public virtual string Message { get { return _message; } }
        public virtual object Value { get { return _value; } }
        public virtual Token Token { get { return _token; } }

        private FunctionResult(bool success, string message, string value, Token token)
        {
            _success = success;
            _message = message;
            _value = value;
            _token = token;
        }

        public static FunctionResult Success(object value, Token token)
        {
            return Success(value, token, value.ToString());
        }

        public static FunctionResult Success(object value, Token token, string message)
        {
            return new FunctionResult(true, message, value.ToString(), token);
        }

        public static FunctionResult Fail(string message)
        {
            return new FunctionResult(false, message, null, Token.Word);
        }
    }
}
