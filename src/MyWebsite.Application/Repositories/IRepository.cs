using MyWebsite.Domain.Base;
using System.Linq.Expressions;

namespace MyWebsite.Application.Repositories
{
   public interface IRepository<T> where T : BaseEntity
   {
      IEnumerable<T> Get(Expression<Func<T, bool>> condition = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int skip = 0, int take = 0, bool ShowAll = false, bool asNoTracking = false);
      Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> condition = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int skip = 0, int take = 0, bool ShowAll = false, bool asNoTracking = false);

      T Get(object id, bool asNoTracking = true);
      Task<T> GetAsync(object id, bool asNoTracking = true);
      T FirstOrDefault(Expression<Func<T, bool>> condition = null, bool asNoTracking = true, string includeProps = null);
      Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> condition = null, bool asNoTracking = true, string includeProps = null);

      bool SoftDelete(object id);
      bool SoftDelete(T entity);
      bool HardDelete(object id);
      bool HardDelete(T entity);

      bool Add(T entity);
      Task<bool> AddAsync(T entity);

      bool Update(T entity);
      bool Update(object id);

      int Count(Expression<Func<T, bool>> condition = null);
   }
}
