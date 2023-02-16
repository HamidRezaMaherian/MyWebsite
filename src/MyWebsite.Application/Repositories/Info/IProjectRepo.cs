using MyWebsite.Application.Repositories.Contract;
using MyWebsite.Domain.Entities.Info;
using System.Linq.Expressions;

namespace MyWebsite.Application.Repositories
{
   public interface IProjectRepo
   {
		void Delete(int id);
		Project FirstOrDefault(Expression<Func<Project, bool>> predicate);
		Project FirstOrDefault();
		IEnumerable<Project> GetAll(Expression<Func<Project, bool>> predicate);
		IEnumerable<Project> GetAll();
		Project GetById(int v);
		void Insert(Project value);
		void Update(Project e);
	}
}
