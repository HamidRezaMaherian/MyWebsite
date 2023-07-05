using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Presentation.Tests.Utils;
using System;
using System.Linq.Expressions;

public class FakeExperienceRepo : IExperienceRepo
{
	private readonly ICollection<Experience> _values;

	public FakeExperienceRepo(FakeExperienceDataGenerator dataGenerator)
	{
		_values = dataGenerator.UseSeed(5).Generate(5, $"{Helpers.Statics.REQUIRED_PROPS},{Helpers.Statics.OPTIONAL_PROPS}");
	}

	public void Delete(int id)
	{
		var entity = _values.FirstOrDefault(i => i.Id == id);
		_values.Remove(entity);
	}

	public Experience FirstOrDefault(Expression<Func<Experience, bool>> predicate)
	{
		return _values.FirstOrDefault(predicate.Compile());
	}

	public Experience FirstOrDefault()
	{
		return _values.FirstOrDefault();
	}

	public IEnumerable<Experience> GetAll(Expression<Func<Experience, bool>> predicate)
	{
		return _values;
	}

	public IEnumerable<Experience> GetAll()
	{
		return _values;
	}

	public Experience GetById(int v)
	{
		return _values.FirstOrDefault(i => i.Id == v);
	}

	public void Insert(Experience value)
	{
		_values.Add(value);
	}

	public void Update(Experience e)
	{
		Delete(e.Id);
		_values.Add(e);
	}
}
