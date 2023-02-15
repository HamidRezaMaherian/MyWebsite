using MyWebsite.Application.Repositories.Contract;
using MyWebsite.Domain.Entities.Info;
using System.Linq.Expressions;

namespace MyWebsite.Application.Repositories
{
	public interface IExperienceRepo
	{
		void Delete(int id);
		Experience FirstOrDefault(Expression<Func<Experience, bool>> predicate);
		Experience FirstOrDefault();
		IEnumerable<Experience> GetAll(Expression<Func<Experience, bool>> predicate);
		IEnumerable<Experience> GetAll();
		Experience GetById(int v);
		void Insert(Experience value);
		void Update(Experience e);
	}
}
