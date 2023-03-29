using MyWebsite.Domain.Entities.Info;
using System.Linq.Expressions;

namespace MyWebsite.Application.Repositories
{
	public interface IAboutMeKeyValueRepo
	{
		void Delete(int id);
		AboutMeKeyValue FirstOrDefault(Expression<Func<AboutMeKeyValue, bool>> predicate);
		AboutMeKeyValue FirstOrDefault();
		IEnumerable<AboutMeKeyValue> GetAll(Expression<Func<AboutMeKeyValue, bool>> predicate);
		IEnumerable<AboutMeKeyValue> GetAll();
		AboutMeKeyValue GetById(int v);
		void Insert(AboutMeKeyValue value);
		void Update(AboutMeKeyValue e);

	}
}
