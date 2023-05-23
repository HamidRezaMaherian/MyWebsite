using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using System.Linq.Expressions;

public class FakeContactMeRepo : IContactMeRepo
{
	private readonly IEnumerable<ContactMe> _values;
	public FakeContactMeRepo()
	{
		_values = new ContactMe[]
		{
			new ContactMe(){Id=1,LangId=1,Email="test@test",PhoneNumber="+98920275820"},
			new ContactMe(){Id=1,LangId=2,Email="test@test",PhoneNumber="+98920275820"},
		};
	}
	public ContactMe FirstOrDefault(Expression<Func<ContactMe, bool>> condition = null)
	{
		return condition is null ? _values.FirstOrDefault() : _values.FirstOrDefault(condition.Compile());
	}

	public Task<ContactMe> FirstOrDefaultAsync(Expression<Func<ContactMe, bool>> condition = null)
	{
		return Task.FromResult(
			condition is null ? _values.FirstOrDefault() : _values.FirstOrDefault(condition.Compile())
			);
	}

	public void Update(ContactMe entity)
	{
		throw new NotImplementedException();
	}

	public Task UpdateAsync(ContactMe entity)
	{
		throw new NotImplementedException();
	}
}
