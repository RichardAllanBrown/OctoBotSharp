using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Domain
{
    public class GlobalConfig
    {
        public int Id { get; set; }

        public string CurrenyChar { get; set; }


        public static GlobalConfig CreateDefault()
        {
            return new GlobalConfig()
            {
                CurrenyChar = "£",
            };
        }
    }
}
