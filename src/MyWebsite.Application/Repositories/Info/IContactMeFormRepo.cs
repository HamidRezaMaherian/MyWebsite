using MyWebsite.Domain.Entities.Info;
using System.Linq.Expressions;

namespace MyWebsite.Application.Repositories
{
	public interface IContactMeFormRepo
	{
		void Delete(int id);
		ContactMeForm FirstOrDefault(Expression<Func<ContactMeForm, bool>> predicate);
		ContactMeForm FirstOrDefault();
		IEnumerable<ContactMeForm> GetAll(Expression<Func<ContactMeForm, bool>> predicate);
		IEnumerable<ContactMeForm> GetAll();
		ContactMeForm GetById(int v);
		void Insert(ContactMeForm value);
		void Update(ContactMeForm e);
	}
}
