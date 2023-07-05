using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Presentation.Tests.Utils;
using System.Linq.Expressions;

public class FakeAboutMeKeyValueRepo : IAboutMeKeyValueRepo
{
	private readonly ICollection<AboutMeKeyValue> _values;

	public FakeAboutMeKeyValueRepo(FakeAboutMeKeyValueDataGenerator dataGenerator)
	{
		_values = dataGenerator.UseSeed(5).Generate(5, $"{Helpers.Statics.REQUIRED_PROPS},{Helpers.Statics.OPTIONAL_PROPS}");
	}

	public void Delete(int id)
	{
		var entity = _values.FirstOrDefault(i => i.Id == id);
		_values.Remove(entity);
	}

	public AboutMeKeyValue FirstOrDefault(Expression<Func<AboutMeKeyValue, bool>> predicate)
	{
		return _values.FirstOrDefault(predicate.Compile());
	}

	public AboutMeKeyValue FirstOrDefault()
	{
		return _values.FirstOrDefault();
	}

	public IEnumerable<AboutMeKeyValue> GetAll(Expression<Func<AboutMeKeyValue, bool>> predicate)
	{
		return _values;
	}

	public IEnumerable<AboutMeKeyValue> GetAll()
	{
		return _values;
	}

	public AboutMeKeyValue GetById(int v)
	{
		return _values.FirstOrDefault(i => i.Id == v);
	}

	public void Insert(AboutMeKeyValue value)
	{
		_values.Add(value);
	}

	public void Update(AboutMeKeyValue e)
	{
		Delete(e.Id);
		_values.Add(e);
	}
}
