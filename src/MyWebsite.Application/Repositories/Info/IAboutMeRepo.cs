using MyWebsite.Application.Repositories.Contract;
using MyWebsite.Domain.Entities.Info;
using System.Linq.Expressions;

namespace MyWebsite.Application.Repositories
{
	public interface IAboutMeRepo
	{
		AboutMe FirstOrDefault(Expression<Func<AboutMe, bool>> condition = null);
		Task<AboutMe> FirstOrDefaultAsync(Expression<Func<AboutMe, bool>> condition = null);
		void Update(AboutMe entity);
		Task UpdateAsync(AboutMe entity);
	}
}
