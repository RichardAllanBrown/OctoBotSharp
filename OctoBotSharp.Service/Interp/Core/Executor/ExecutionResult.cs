using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctoBotSharp.Service.Interp.Core
{
    public class ExecutionResult
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public string[] Response { get; set; }


        public static ExecutionResult Ok(string[] resp)
        {
            return new ExecutionResult()
            {
                Success = true,
                Error = string.Empty,
                Response = resp,
            };
        }

        public static ExecutionResult Fail(string error)
        {
            return new ExecutionResult()
            {
                Success = false,
                Error = error,
                Response = new string[0],
            };
        }
    }
}
