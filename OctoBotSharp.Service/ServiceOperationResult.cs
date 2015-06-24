using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service
{
    public class ServiceOperationResult
    {
        public bool Success { get; private set; }

        public string[] Errors { get; private set; }

        private ServiceOperationResult(bool success, params string[] errors)
        {
            Success = success;
            Errors = errors;
        }

        public static ServiceOperationResult Ok
        {
            get
            {
                return new ServiceOperationResult(success: true);
            }
        }

        public static ServiceOperationResult Fail(params string[] errors)
        {
            return new ServiceOperationResult(success: false, errors: errors);
        }
    }
}
