using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Models.Auth
{
    public class ForgottenPassword
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}