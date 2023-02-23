using Bogus;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Exceptions;
using MyWebsite.Domain.Base;
using MyWebsite.Infrastructure.Persistent;
using System.Linq.Expressions;
using static MyWebsite.Infrastructure.Tests.Helpers;

namespace MyWebsite.Infrastructure.Tests.Repositories.Info;

[TestFixture]
public abstract class BaseLanguageRepoTests<TEntity, IRepo>
	where TEntity : BaseLanguage
	where IRepo : class
{
	internal ApplicationDbContext _db;
	private DbSet<TEntity> _dbSet;
	private dynamic _repo;
	private Faker<TEntity> _dataFaker;
	private List<TEntity> _seedData;

	private void RollBack(params TEntity[] entities)
	{
		_dbSet.RemoveRange(entities);
		_db.SaveChanges();
	}


	protected abstract IRepo CreateRepo();
	protected abstract Faker<TEntity> CreateDataFaker();

	protected abstract IEqualityComparer<TEntity> CreateComparer();


	[OneTimeSetUp]
	public async Task OneTimeSetup()
	{
		_db = new ApplicationDbContextFactory().CreateDbContext(null);
		_dbSet = _db.Set<TEntity>();
		if ((await _db.Database.GetPendingMigrationsAsync()).Any())
		{
			await _db.Database.MigrateAsync();
		}
		_dbSet.RemoveRange(_dbSet);

		_repo = CreateRepo();
		_dataFaker = CreateDataFaker();
		_seedData = _dataFaker.Generate(5, "RequiredProps,OptionalProps");
		await _dbSet.AddRangeAsync(_seedData);
		await _db.SaveChangesAsync();
	}
	[OneTimeTearDown]
	public async Task OneTimeTearDown()
	{
		_db.RemoveRange(_seedData);
		await _db.SaveChangesAsync();
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
		IEnumerable<TEntity> res = _repo.GetAll();
		Assert.That(res.Select(i => i.Id), Is.EquivalentTo(_seedData.Select(i => i.Id)));
	}
	[Test]
	public void GetAll_PassNonExistingPredicate()
	{
		Expression<Func<TEntity, bool>> predicate = i => false;
		IEnumerable<TEntity> res = _repo.GetAll(predicate);

		Assert.That(res, Is.Empty);
	}
	[Test]
	public void GetAll_PassExistingPredicates()
	{
		var comparedObj1 = _seedData.Skip(1).Take(1).First();
		var comparedObj2 = _seedData.Skip(3).Take(1).First();

		Expression<Func<TEntity, bool>> predicate = i => true;
		Expression<Func<TEntity, bool>> predicate2 = i => i.Id > comparedObj1.Id;
		Expression<Func<TEntity, bool>> predicate3 = i => i.LangId == comparedObj2.LangId;
		IEnumerable<TEntity> res = _repo.GetAll(predicate);
		IEnumerable<TEntity> res2 = _repo.GetAll(predicate2);
		IEnumerable<TEntity> res3 = _repo.GetAll(predicate3);

		Assert.That(res.Select(i => i.Id), Is.EquivalentTo(_seedData.Select(i => i.Id)));
		Assert.That(res2.Select(i => i.Id), Is.EquivalentTo(_seedData.Where(predicate2.Compile()).Select(i => i.Id)));
		Assert.That(res3.Select(i => i.Id), Is.EquivalentTo(_seedData.Where(predicate3.Compile()).Select(i => i.Id)));
	}
	[Test]
	public void GetById_PassInvalidArgument()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() =>
		{
			TEntity entity = _repo.GetById(0);
		});
		Assert.Throws<ArgumentOutOfRangeException>(() =>
		{
			TEntity entity = _repo.GetById(-3);
		});
	}
	[Test]
	public void GetById_PassNonExistingId()
	{
		var random = new Random();
		TEntity entity = _repo.GetById(random.Next(_seedData.Max(i => i.Id) + 1, int.MaxValue));
		TEntity entity2 = _repo.GetById(random.Next(_seedData.Max(i => i.Id) + 1, int.MaxValue));
		Assert.That(entity, Is.Null);
		Assert.That(entity2, Is.Null);
	}
	[Test]
	public void GetById_PassExistingId()
	{
		var validId = _seedData.Last().Id;
		TEntity entity = _repo.GetById(validId);
		Assert.That(entity.Id, Is.EqualTo(validId));
	}
	[Test]
	public void FirstOrDefault_PassNull()
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			TEntity entity = _repo.FirstOrDefault(null);
		});
	}
	[Test]
	public void FirstOrDefault_EmptyOverload()
	{
		TEntity res = _repo.FirstOrDefault();
		Assert.That(res.Id, Is.EqualTo(_seedData.FirstOrDefault()!.Id));
	}
	[Test]
	public void FirstOrDefault_PassNonExistingPredicate()
	{
		Expression<Func<TEntity, bool>> predicate = i => false;
		TEntity res = _repo.FirstOrDefault(predicate);
		Assert.That(res, Is.Null);
	}
	[Test]
	public void FirstOrDefault_PassExistingPredicate()
	{
		var comparedObj1 = _seedData.Skip(1).Take(1).First();
		var comparedObj2 = _seedData.Skip(3).Take(1).First();

		Expression<Func<TEntity, bool>> predicate = i => true;
		Expression<Func<TEntity, bool>> predicate2 = i => i.Id > comparedObj1.Id;
		Expression<Func<TEntity, bool>> predicate3 = i => i.LangId == comparedObj2.LangId;
		TEntity res = _repo.FirstOrDefault(predicate);
		TEntity res2 = _repo.FirstOrDefault(predicate2);
		TEntity res3 = _repo.FirstOrDefault(predicate3);

		Assert.That(res.Id, Is.EqualTo(_seedData.FirstOrDefault()!.Id));
		Assert.That(res2.Id, Is.EqualTo(_seedData.FirstOrDefault(predicate2.Compile())!.Id));
		Assert.That(res3.Id, Is.EqualTo(_seedData.FirstOrDefault(predicate3.Compile())!.Id));
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
		Assert.That(_dbSet.Find(entity.Id), Is.Null);
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

		Assert.That(_dbSet.Find(validEntity.Id), Is.Null);
		Assert.That(_dbSet.Find(invalidEntity.Id), Is.Null);
	}
	[Test]
	public void Update_PassExistingInvalidObject()
	{
		var entity = _dataFaker.Generate("RequiredProps,OptionalProps");
		_dbSet.Add(entity);
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
		_dbSet.Add(entity);
		_db.SaveChanges();
		_db.Entry(entity).State = EntityState.Detached;
		_dataFaker.Populate(entity, "RequiredProps,OptionalProps");

		_repo.Update(entity);

		IEqualityComparer<TEntity> comparer = CreateComparer();
		Assert.That(comparer.Equals(_dbSet.AsNoTracking().FirstOrDefault(i => i.Id == entity.Id), entity), Is.True);

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
		_dbSet.Add(entity);
		_db.SaveChanges();
		_db.Entry(entity).State = EntityState.Detached;
		_repo.Delete(entity.Id);

		Assert.That(_dbSet.Find(entity.Id), Is.Null);
	}
}

[TestFixture]
public abstract class BaseRepoTests<TEntity, IRepo>
	where TEntity : BaseEntity
	where IRepo : class
{
	internal ApplicationDbContext _db;
	private DbSet<TEntity> _dbSet;
	private dynamic _repo;
	private Faker<TEntity> _dataFaker;
	private List<TEntity> _seedData;

	private void RollBack(params TEntity[] entities)
	{
		_dbSet.RemoveRange(entities);
		_db.SaveChanges();
	}


	protected abstract IRepo CreateRepo();
	protected abstract Faker<TEntity> CreateDataFaker();

	protected abstract IEqualityComparer<TEntity> CreateComparer();


	[OneTimeSetUp]
	public async Task OneTimeSetup()
	{
		_db = new ApplicationDbContextFactory().CreateDbContext(null);
		_dbSet = _db.Set<TEntity>();
		if ((await _db.Database.GetPendingMigrationsAsync()).Any())
		{
			await _db.Database.MigrateAsync();
		}
		_dbSet.RemoveRange(_dbSet);

		_repo = CreateRepo();
		_dataFaker = CreateDataFaker();
		_seedData = _dataFaker.Generate(5, "RequiredProps,OptionalProps");
		await _dbSet.AddRangeAsync(_seedData);
		await _db.SaveChangesAsync();
	}
	[OneTimeTearDown]
	public async Task OneTimeTearDown()
	{
		_db.RemoveRange(_seedData);
		await _db.SaveChangesAsync();
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
		IEnumerable<TEntity> res = _repo.GetAll();
		Assert.That(res.Select(i => i.Id), Is.EquivalentTo(_seedData.Select(i => i.Id)));
	}
	[Test]
	public void GetAll_PassNonExistingPredicate()
	{
		Expression<Func<TEntity, bool>> predicate = i => false;
		IEnumerable<TEntity> res = _repo.GetAll(predicate);

		Assert.That(res, Is.Empty);
	}
	[Test]
	public void GetAll_PassExistingPredicates()
	{
		var comparedObj1 = _seedData.Skip(1).Take(1).First();
		var comparedObj2 = _seedData.Skip(3).Take(1).First();

		Expression<Func<TEntity, bool>> predicate = i => true;
		Expression<Func<TEntity, bool>> predicate2 = i => i.Id > comparedObj1.Id;
		Expression<Func<TEntity, bool>> predicate3 = i => !i.IsActive;
		IEnumerable<TEntity> res = _repo.GetAll(predicate);
		IEnumerable<TEntity> res2 = _repo.GetAll(predicate2);
		IEnumerable<TEntity> res3 = _repo.GetAll(predicate3);

		Assert.That(res.Select(i => i.Id), Is.EquivalentTo(_seedData.Select(i => i.Id)));
		Assert.That(res2.Select(i => i.Id), Is.EquivalentTo(_seedData.Where(predicate2.Compile()).Select(i => i.Id)));
		Assert.That(res3.Select(i => i.Id), Is.EquivalentTo(_seedData.Where(predicate3.Compile()).Select(i => i.Id)));
	}
	[Test]
	public void GetById_PassInvalidArgument()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() =>
		{
			TEntity entity = _repo.GetById(0);
		});
		Assert.Throws<ArgumentOutOfRangeException>(() =>
		{
			TEntity entity = _repo.GetById(-3);
		});
	}
	[Test]
	public void GetById_PassNonExistingId()
	{
		var random = new Random();
		TEntity entity = _repo.GetById(random.Next(_seedData.Max(i => i.Id) + 1, int.MaxValue));
		TEntity entity2 = _repo.GetById(random.Next(_seedData.Max(i => i.Id) + 1, int.MaxValue));
		Assert.That(entity, Is.Null);
		Assert.That(entity2, Is.Null);
	}
	[Test]
	public void GetById_PassExistingId()
	{
		var validId = _seedData.Last().Id;
		TEntity entity = _repo.GetById(validId);
		Assert.That(entity.Id, Is.EqualTo(validId));
	}
	[Test]
	public void FirstOrDefault_PassNull()
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			TEntity entity = _repo.FirstOrDefault(null);
		});
	}
	[Test]
	public void FirstOrDefault_EmptyOverload()
	{
		TEntity res = _repo.FirstOrDefault();
		Assert.That(res.Id, Is.EqualTo(_seedData.FirstOrDefault()!.Id));
	}
	[Test]
	public void FirstOrDefault_PassNonExistingPredicate()
	{
		Expression<Func<TEntity, bool>> predicate = i => false;
		TEntity res = _repo.FirstOrDefault(predicate);
		Assert.That(res, Is.Null);
	}
	[Test]
	public void FirstOrDefault_PassExistingPredicate()
	{
		var comparedObj1 = _seedData.Skip(1).Take(1).First();
		var comparedObj2 = _seedData.Skip(3).Take(1).First();

		Expression<Func<TEntity, bool>> predicate = i => true;
		Expression<Func<TEntity, bool>> predicate2 = i => i.Id > comparedObj1.Id;
		Expression<Func<TEntity, bool>> predicate3 = i => !i.IsActive;
		TEntity res = _repo.FirstOrDefault(predicate);
		TEntity res2 = _repo.FirstOrDefault(predicate2);
		TEntity res3 = _repo.FirstOrDefault(predicate3);

		Assert.That(res.Id, Is.EqualTo(_seedData.FirstOrDefault()!.Id));
		Assert.That(res2.Id, Is.EqualTo(_seedData.FirstOrDefault(predicate2.Compile())!.Id));
		Assert.That(res3.Id, Is.EqualTo(_seedData.FirstOrDefault(predicate3.Compile())!.Id));
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
		Assert.That(_dbSet.Find(entity.Id), Is.Null);
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

		Assert.That(_dbSet.Find(validEntity.Id), Is.Null);
		Assert.That(_dbSet.Find(invalidEntity.Id), Is.Null);
	}
	[Test]
	public void Update_PassExistingInvalidObject()
	{
		var entity = _dataFaker.Generate("RequiredProps,OptionalProps");
		_dbSet.Add(entity);
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
		_dbSet.Add(entity);
		_db.SaveChanges();
		_db.Entry(entity).State = EntityState.Detached;
		_dataFaker.Populate(entity, "RequiredProps,OptionalProps");

		_repo.Update(entity);

		IEqualityComparer<TEntity> comparer = CreateComparer();
		Assert.That(comparer.Equals(_dbSet.AsNoTracking().FirstOrDefault(i => i.Id == entity.Id), entity), Is.True);

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
		_dbSet.Add(entity);
		_db.SaveChanges();
		_db.Entry(entity).State = EntityState.Detached;
		_repo.Delete(entity.Id);

		Assert.That(_dbSet.Find(entity.Id), Is.Null);
	}
}