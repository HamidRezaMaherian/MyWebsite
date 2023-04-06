using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Presentation.Tests.Utils;
using System.Linq.Expressions;

public class FakeSkillRepo : ISkillRepo
{
	private readonly ICollection<Skill> _values;

	public FakeSkillRepo(FakeSkillDataGenerator dataGenerator)
	{
		_values = dataGenerator.UseSeed(5).Generate(5, $"{Helpers.Statics.REQUIRED_PROPS},{Helpers.Statics.OPTIONAL_PROPS}");
	}

	public void Delete(int id)
	{
		var entity = _values.FirstOrDefault(i => i.Id == id);
		_values.Remove(entity);
	}

	public Skill FirstOrDefault(Expression<Func<Skill, bool>> predicate)
	{
		return _values.FirstOrDefault(predicate.Compile());
	}

	public Skill FirstOrDefault()
	{
		return _values.FirstOrDefault();
	}

	public IEnumerable<Skill> GetAll(Expression<Func<Skill, bool>> predicate)
	{
		return _values;
	}

	public IEnumerable<Skill> GetAll()
	{
		return _values;
	}

	public Skill GetById(int v)
	{
		return _values.FirstOrDefault(i => i.Id == v);
	}

	public void Insert(Skill value)
	{
		_values.Add(value);
	}

	public void Update(Skill e)
	{
		Delete(e.Id);
		_values.Add(e);
	}
}
