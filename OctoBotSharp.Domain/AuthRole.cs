using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Domain
{
    public static class AuthRole
    {
        public const string UserManagement = "UserManagement";
        public const string ItemDefinitions = "ItemDefinitions";
        public const string UserFinancesAdjustment = "UserFinancesAdjustment";
        public const string GlobalConfig = "GlobalConfig";

        public static IEnumerable<string> GetAll()
        {
            yield return UserManagement;
            yield return ItemDefinitions;
            yield return UserFinancesAdjustment;
            yield return GlobalConfig;
        }
    }
}
