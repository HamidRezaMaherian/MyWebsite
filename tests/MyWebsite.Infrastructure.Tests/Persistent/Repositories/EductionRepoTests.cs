using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyWebsite.Application.Exceptions;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Persistent;
using MyWebsite.Infrastructure.Repositories.Info;
using NuGet.Frameworks;
using static MyWebsite.Infrastructure.Tests.Helpers;

namespace MyWebsite.Infrastructure.Tests.Repositories.Info;

[TestFixture]
public class EducationRepoTests
{
	private ApplicationDbContext _db;
	private IEducationRepo _repo;
	private Faker<Education> _dataFaker;
	private List<Education> _seedData;

	private void RollBack(params Education[] entities)
	{
		_db.Educations.RemoveRange(entities);
		_db.SaveChanges();
	}

	[OneTimeSetUp]
	public async Task OneTimeSetup()
	{
		_db = new ApplicationDbContextFactory().CreateDbContext(null);
		if ((await _db.Database.GetPendingMigrationsAsync()).Any())
		{
			await _db.Database.MigrateAsync();
		}
		_db.Educations.RemoveRange(_db.Educations);

		_repo = new EducationRepo(_db);
		_dataFaker = new Faker<Education>()
			.RuleSet("RequiredProps", (rs) =>
			{
				rs.RuleFor(i => i.Title, f => f.Name.JobTitle());
				rs.RuleFor(i => i.SubTitle, f => f.Name.JobDescriptor());
				rs.RuleFor(i => i.LangId, f => f.Random.Int(1, 2));
				rs.RuleFor(i => i.IsActive, f => f.Random.Bool());
			})
			.RuleSet("OptionalProps", (rs) =>
			{
				rs.RuleFor(i => i.Link, f => f.Internet.Url());
				rs.RuleFor(i => i.Role, f => f.Name.JobType());
				rs.RuleFor(i => i.TimeSpan, f => f.Date.Timespan().ToString());
			});

		_seedData = _dataFaker.Generate(5, "RequiredProps,OptionalProps");
		await _db.Educations.AddRangeAsync(_seedData);
		await _db.SaveChangesAsync();
	}
	[OneTimeTearDown]
	public async Task OneTimeTearDown()
	{
		_db.RemoveRange(_seedData);
		await _db.SaveChangesAsync();
	}

	[Test]
	[Ignore("no need")]
	public void CreateObj_PassNull()
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			IEducationRepo repo = new EducationRepo(null);
		});
	}
	[Test]
	[Ignore("no need")]
	public void CreateObj_PassLocalDb()
	{
		IEducationRepo repo = new EducationRepo(_db);
		Assert.Pass();
	}
	[Test]
	public void GetAll_PassNull()
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			_repo.GetAll(null);
		});
	}
	[Test]
	public void GetAll_EmptyOverload()
	{
		IEnumerable<Education> res = _repo.GetAll();
		Assert.That(res.Select(i => i.Id), Is.EquivalentTo(_seedData.Select(i => i.Id)));
	}
	[Test]
	public void GetAll_PassNonExistingPredicate()
	{

		IEnumerable<Education> res = _repo.GetAll(i => false);

		Assert.That(res, Is.Empty);
	}
	[Test]
	public void GetAll_PassExistingPredicates()
	{
		var comparedObj1 = _seedData.Skip(1).Take(1).First();
		var comparedObj2 = _seedData.Skip(3).Take(1).First();

		IEnumerable<Education> res = _repo.GetAll(i => true);
		IEnumerable<Education> res2 = _repo.GetAll(i => i.Id > comparedObj1.Id);
		IEnumerable<Education> res3 = _repo.GetAll(i => i.Title == comparedObj2.Title);

		Assert.That(res.Select(i => i.Id), Is.EquivalentTo(_seedData.Select(i => i.Id)));
		Assert.That(res2.Select(i => i.Id), Is.EquivalentTo(_seedData.Where(i => i.Id > comparedObj1.Id).Select(i => i.Id)));
		Assert.That(res3.Select(i => i.Id), Is.EquivalentTo(_seedData.Where(i => i.Title == comparedObj2.Title).Select(i => i.Id)));
	}
	[Test]
	public void GetById_PassInvalidArgument()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() =>
		{
			Education entity = _repo.GetById(0);
		});
		Assert.Throws<ArgumentOutOfRangeException>(() =>
		{
			Education entity = _repo.GetById(-3);
		});
	}
	[Test]
	public void GetById_PassNonExistingId()
	{
		Education entity = _repo.GetById(_seedData.Max(i => i.Id) + 1);
		Education entity2 = _repo.GetById(_seedData.Min(i => i.Id) - 1);
		Assert.That(entity, Is.Null);
		Assert.That(entity2, Is.Null);
	}
	[Test]
	public void GetById_PassExistingId()
	{
		var validId = _seedData.Last().Id;
		Education entity = _repo.GetById(validId);
		Assert.That(entity.Id, Is.EqualTo(validId));
	}
	[Test]
	public void FirstOrDefault_PassNull()
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			Education entity = _repo.FirstOrDefault(null);
		});
	}
	[Test]
	public void FirstOrDefault_EmptyOverload()
	{
		Education res = _repo.FirstOrDefault();
		Assert.That(res.Id, Is.EqualTo(_seedData.FirstOrDefault()!.Id));
	}
	[Test]
	public void FirstOrDefault_PassNonExistingPredicate()
	{
		Education res = _repo.FirstOrDefault(i => false);
		Assert.That(res, Is.Null);
	}
	[Test]
	public void FirstOrDefault_PassExistingPredicate()
	{
		var comparedObj1 = _seedData.Skip(1).Take(1).First();
		var comparedObj2 = _seedData.Skip(3).Take(1).First();

		Education res = _repo.FirstOrDefault(i => true);
		Education res2 = _repo.FirstOrDefault(i => i.Id > comparedObj1.Id);
		Education res3 = _repo.FirstOrDefault(i => i.Title == comparedObj2.Title);

		Assert.That(res.Id, Is.EqualTo(_seedData.FirstOrDefault()!.Id));
		Assert.That(res2.Id, Is.EqualTo(_seedData.FirstOrDefault(i => i.Id > comparedObj1.Id)!.Id));
		Assert.That(res3.Id, Is.EqualTo(_seedData.FirstOrDefault(i => i.Title == comparedObj2.Title)!.Id));
	}
	[Test]
	public void Insert_PassNull()
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			_repo.Insert(null);
		});
	}
	[Test]
	public void Insert_PassInvalidObject()
	{
		var entity = _dataFaker.Generate("OptionalProps");
		Assert.Throws(Is.InstanceOf<DbOperationException>(), () => _repo.Insert(entity));
		Assert.That(_db.Educations.Find(entity.Id), Is.Null);
	}
	[Test]
	public void Insert_PassValidObject()
	{
		var entity = _dataFaker.Generate("RequiredProps");
		var entity2 = _dataFaker.Generate("RequiredProps,OptionalProps");

		_repo.Insert(entity);
		_repo.Insert(entity2);

		Assert.That(_db.Entry(entity).State, Is.EqualTo(EntityState.Unchanged));
		Assert.That(_db.Entry(entity2).State, Is.EqualTo(EntityState.Unchanged));

		RollBack(entity, entity2);
	}
	[Test]
	public void Update_PassNull()
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			_repo.Update(null);
		});
	}
	[Test]
	public void Update_PassNonExistingObject()
	{
		var validEntity = _dataFaker.Generate("RequiredProps,OptionalProps");
		var invalidEntity = _dataFaker.Generate("OptionalProps");

		Assert.Throws<DbOperationException>(() => _repo.Update(invalidEntity));
		Assert.Throws<DbOperationException>(() => _repo.Update(validEntity));

		Assert.That(_db.Educations.Find(validEntity.Id), Is.Null);
		Assert.That(_db.Educations.Find(invalidEntity.Id), Is.Null);
	}
	[Test]
	public void Update_PassExistingInvalidObject()
	{
		var entity = _dataFaker.Generate("RequiredProps,OptionalProps");
		_db.Educations.Add(entity);
		_db.SaveChanges();
		_db.Entry(entity).State = EntityState.Detached;
		var editedEntity = _dataFaker.Generate("OptionalProps");
		editedEntity.Id = entity.Id;

		Assert.Throws<DbOperationException>(() =>
		{
			_repo.Update(editedEntity);
		});
		Assert.That(_db.ChangeTracker.HasChanges(), Is.False);

		RollBack(entity);
	}
	[Test]
	public void Update_PassExistingValidObject()
	{
		var entity = _dataFaker.Generate("RequiredProps,OptionalProps");
		_db.Educations.Add(entity);
		_db.SaveChanges();
		_db.Entry(entity).State = EntityState.Detached;
		_dataFaker.Populate(entity, "RequiredProps,OptionalProps");

		_repo.Update(entity);

		EducationValueComparer comparer = new EducationValueComparer();
		Assert.That(comparer.Equals(_db.Educations.AsNoTracking().FirstOrDefault(i => i.Id == entity.Id), entity), Is.True);

		RollBack(entity);
	}
	[Test]
	public void Delete_PassInvalidId()
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			_repo.Delete(-1);
		});
		Assert.Throws<ArgumentNullException>(() =>
		{
			_repo.Delete(0);
		});
	}
	[Test]
	public void Delete_PassNonExistingId()
	{
		Assert.Throws<DbOperationException>(() =>
		{
			_repo.Delete(_seedData.Max(i => i.Id) + 1);
		});
	}
	[Test]
	public void Delete_PassExistingId()
	{
		var entity = _dataFaker.Generate("RequiredProps,OptionalProps");
		_db.Educations.Add(entity);
		_db.SaveChanges();
		_db.Entry(entity).State = EntityState.Detached;
		_repo.Delete(entity.Id);

		Assert.That(_db.Educations.Find(entity.Id), Is.Null);
	}
}