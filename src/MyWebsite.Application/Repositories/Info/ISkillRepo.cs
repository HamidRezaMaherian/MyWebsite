using MyWebsite.Domain.Entities.Info;
using System.Linq.Expressions;

namespace MyWebsite.Application.Repositories
{
   public interface ISkillRepo
   {
		void Delete(int id);
		Skill FirstOrDefault(Expression<Func<Skill, bool>> predicate);
		Skill FirstOrDefault();
		IEnumerable<Skill> GetAll(Expression<Func<Skill, bool>> predicate);
		IEnumerable<Skill> GetAll();
		Skill GetById(int v);
		void Insert(Skill value);
		void Update(Skill e);
	}
}
