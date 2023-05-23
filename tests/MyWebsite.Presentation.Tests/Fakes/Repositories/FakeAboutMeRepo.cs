using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using System.Linq.Expressions;

public class FakeAboutMeRepo : IAboutMeRepo
{
	private readonly IEnumerable<AboutMe> _values;
	public FakeAboutMeRepo()
	{
		_values = new AboutMe[]
		{
			new AboutMe(){Id=1,LangId=1,FilePath="no path",IsActive=true,IsDelete=false},
			new AboutMe(){Id=2,LangId=2,FilePath="no path",IsActive=true,IsDelete=false},
		};
	}
	public AboutMe FirstOrDefault(Expression<Func<AboutMe, bool>> condition = null)
	{
		return condition is null ? _values.FirstOrDefault() : _values.FirstOrDefault(condition.Compile());
	}

	public Task<AboutMe> FirstOrDefaultAsync(Expression<Func<AboutMe, bool>> condition = null)
	{
		return Task.FromResult(
			condition is null ? _values.FirstOrDefault() : _values.FirstOrDefault(condition.Compile())
			);
	}

	public void Update(AboutMe entity)
	{
		throw new NotImplementedException();
	}

	public Task UpdateAsync(AboutMe entity)
	{
		throw new NotImplementedException();
	}
}
