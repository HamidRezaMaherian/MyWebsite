using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using System.Linq.Expressions;

public class FakeMainInfoRepo : IMainInfoRepo
{
	private readonly IEnumerable<MainInfo> _values;
	public FakeMainInfoRepo()
	{
		_values = new MainInfo[]
		{
				new MainInfo() { Id = 1, LangId = 1, Description = "no desc", DarkImagePath = "no image", LightImagePath = "no image", IsActive = true },
				new MainInfo() { Id = 2, LangId = 2, Description = "no desc", DarkImagePath = "no image", LightImagePath = "no image", IsActive = true }
		};
	}
	public MainInfo FirstOrDefault(Expression<Func<MainInfo, bool>> condition = null)
	{
		return condition is null ? _values.FirstOrDefault() : _values.FirstOrDefault(condition.Compile());
	}

	public Task<MainInfo> FirstOrDefaultAsync(Expression<Func<MainInfo, bool>> condition = null)
	{
		return Task.FromResult(
			condition is null ? _values.FirstOrDefault() : _values.FirstOrDefault(condition.Compile())
			);
	}

	public void Update(MainInfo entity)
	{
		throw new NotImplementedException();
	}

	public Task UpdateAsync(MainInfo entity)
	{
		throw new NotImplementedException();
	}
}
