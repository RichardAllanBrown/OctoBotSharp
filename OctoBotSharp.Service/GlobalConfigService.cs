using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OctoBotSharp.Domain;
using OctoBotSharp.Data;
using OctoBotSharp.Data.Repository;

namespace OctoBotSharp.Service
{
    public interface IGlobalConfigService
    {
        GlobalConfig GetConfig();
        void UpdateConfig(GlobalConfig config);
    }

    public class GlobalConfigService : IGlobalConfigService
    {
        private readonly IRepository<GlobalConfig> _repo;

        public GlobalConfigService(IRepository<GlobalConfig> repo)
        {
            _repo = repo;
        }

        public GlobalConfig GetConfig()
        {
            return _repo.GetAll().First();
        }

        public void UpdateConfig(GlobalConfig config)
        {
            _repo.Update(config);
        }
    }
}
