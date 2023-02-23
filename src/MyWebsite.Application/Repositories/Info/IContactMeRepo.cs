using MyWebsite.Application.Repositories.Contract;
using MyWebsite.Domain.Entities.Info;
using System.Linq.Expressions;

namespace MyWebsite.Application.Repositories
{
	public interface IContactMeRepo
	{
		ContactMe FirstOrDefault(Expression<Func<ContactMe, bool>> condition = null);
		Task<ContactMe> FirstOrDefaultAsync(Expression<Func<ContactMe, bool>> condition = null);
		void Update(ContactMe entity);
		Task UpdateAsync(ContactMe entity);
	}
}
