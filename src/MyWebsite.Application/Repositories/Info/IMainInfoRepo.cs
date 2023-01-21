using MyWebsite.Application.Repositories.Contract;
using MyWebsite.Domain.Entities.Info;
using System.Linq.Expressions;

namespace MyWebsite.Application.Repositories
{
   public interface IMainInfoRepo : ILangRepository<MainInfo>
   {
      IEnumerable<Type> Get<Type>(Expression<Func<Type, bool>> condition = null, Func<IQueryable<Type>, IOrderedQueryable<Type>> orderBy = null, int skip = 0, int take = 0, bool ShowAll = false, bool asNoTracking = false) where Type : MainInfo;
      Task<IEnumerable<Type>> GetAsync<Type>(Expression<Func<Type, bool>> condition = null, Func<IQueryable<Type>, IOrderedQueryable<Type>> orderBy = null, int skip = 0, int take = 0, bool ShowAll = false, bool asNoTracking = false) where Type : MainInfo;
      Type FirstOrDefault<Type>(Expression<Func<Type, bool>> condition = null, bool asNoTracking = true, string includeProps = null) where Type : MainInfo;
      Task<Type> FirstOrDefaultAsync<Type>(Expression<Func<Type, bool>> condition = null, bool asNoTracking = true, string includeProps = null) where Type : MainInfo;

   }
}
