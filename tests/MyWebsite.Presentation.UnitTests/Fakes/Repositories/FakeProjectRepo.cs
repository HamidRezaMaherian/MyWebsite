using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Presentation.UnitTests.Utils;
using System.Linq.Expressions;

public class FakeProjectRepo : IProjectRepo
{
	private readonly ICollection<Project> _values;

	public FakeProjectRepo(FakeProjectDataGenerator dataGenerator)
	{
		_values = dataGenerator.UseSeed(5).Generate(5, $"{Helpers.Statics.REQUIRED_PROPS},{Helpers.Statics.OPTIONAL_PROPS}");
	}

	public void Delete(int id)
	{
		var entity = _values.FirstOrDefault(i => i.Id == id);
		_values.Remove(entity);
	}

	public Project FirstOrDefault(Expression<Func<Project, bool>> predicate)
	{
		return _values.FirstOrDefault(predicate.Compile());
	}

	public Project FirstOrDefault()
	{
		return _values.FirstOrDefault();
	}

	public IEnumerable<Project> GetAll(Expression<Func<Project, bool>> predicate)
	{
		return _values;
	}

	public IEnumerable<Project> GetAll()
	{
		return _values;
	}

	public Project GetById(int v)
	{
		return _values.FirstOrDefault(i => i.Id == v);
	}

	public void Insert(Project value)
	{
		_values.Add(value);
	}

	public void Update(Project e)
	{
		Delete(e.Id);
		_values.Add(e);
	}
}
