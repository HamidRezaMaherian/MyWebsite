using MyWebsite.Domain.Entities.Info;
using System.Linq.Expressions;

namespace MyWebsite.Application.Repositories
{
	public interface IEducationRepo
	{
		void Delete(int id);
		Education FirstOrDefault(Expression<Func<Education, bool>> predicate);
		Education FirstOrDefault();
		IEnumerable<Education> GetAll(Expression<Func<Education, bool>> predicate);
		IEnumerable<Education> GetAll();
		Education GetById(int v);
		void Insert(Education value);
		void Update(Education e);
	}
}
