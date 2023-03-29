using MyWebsite.Domain.Entities.Info;
using System.Linq.Expressions;

namespace MyWebsite.Application.Repositories
{
	public interface IMainInfoRepo
	{
		MainInfo FirstOrDefault(Expression<Func<MainInfo, bool>> condition = null);
		Task<MainInfo> FirstOrDefaultAsync(Expression<Func<MainInfo, bool>> condition = null);
		void Update(MainInfo entity);
		Task UpdateAsync(MainInfo entity);
	}
}
