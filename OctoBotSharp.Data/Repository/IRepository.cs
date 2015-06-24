using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> query);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> query);

        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entity);

        void Update(TEntity entity);

        void Delete(object id);
        void Delete(TEntity entity);
    }
}
