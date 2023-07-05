using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Presentation.Tests.Utils;
using System;
using System.Linq.Expressions;

public class FakeEducationRepo : IEducationRepo
{
	private readonly ICollection<Education> _values;

	public FakeEducationRepo(FakeEducationDataGenerator dataGenerator)
	{
		_values = dataGenerator.UseSeed(5).Generate(5, $"{Helpers.Statics.REQUIRED_PROPS},{Helpers.Statics.OPTIONAL_PROPS}");
	}

	public void Delete(int id)
	{
		var entity = _values.FirstOrDefault(i => i.Id == id);
		_values.Remove(entity);
	}

	public Education FirstOrDefault(Expression<Func<Education, bool>> predicate)
	{
		return _values.FirstOrDefault(predicate.Compile());
	}

	public Education FirstOrDefault()
	{
		return _values.FirstOrDefault();
	}

	public IEnumerable<Education> GetAll(Expression<Func<Education, bool>> predicate)
	{
		return _values;
	}

	public IEnumerable<Education> GetAll()
	{
		return _values;
	}

	public Education GetById(int v)
	{
		return _values.FirstOrDefault(i => i.Id == v);
	}

	public void Insert(Education value)
	{
		_values.Add(value);
	}

	public void Update(Education e)
	{
		Delete(e.Id);
		_values.Add(e);
	}
}
