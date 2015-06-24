using OctoBotSharp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace OctoBotSharp.Service
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> _repo;

        public Service(IRepository<TEntity> repo)
        {
            _repo = repo;
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return _repo.Find(keyValues);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _repo.GetAll();
        }

        public virtual IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> query)
        {
            return _repo.GetMany(query);
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> query)
        {
            return _repo.GetFirstOrDefault(query);
        }

        public virtual void Insert(TEntity entity)
        {
            _repo.Insert(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            _repo.InsertRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _repo.Update(entity);
        }

        public virtual void Delete(object id)
        {
            _repo.Delete(id);
        }

        public virtual void Delete(TEntity entity)
        {
            _repo.Delete(entity);
        }
    }
}
