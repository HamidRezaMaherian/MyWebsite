using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Exceptions;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Persistent;
using MyWebsite.Infrastructure.Repositories.Info;
using static MyWebsite.Infrastructure.Tests.Helpers;

namespace MyWebsite.Infrastructure.Tests.Repositories.Info;

[TestFixture]
public class MainInfoRepoTests
{
	private ApplicationDbContext _db;
	private MainInfoRepo _repo;
	private MainInfoValueComparer _comparer;

	private void RollBack()
	{
		var oldEntity = SeedDataCreator.CreateMainInfo().FirstOrDefault()!;
		_db.MainInfos.Update(oldEntity);
		_db.SaveChanges();
		_db.Entry(oldEntity).State = EntityState.Detached;
	}

	[OneTimeSetUp]
	public async Task OneTimeSetup()
	{
		_db = new Helpers.ApplicationDbContextFactory().CreateDbContext(null);
		if ((await _db.Database.GetPendingMigrationsAsync()).Any())
		{
			await _db.Database.MigrateAsync();
		}
		_repo = new MainInfoRepo(_db);
		_comparer = new MainInfoValueComparer();
	}
	[Test]
	public void CreateObj_PassNull()
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			var repo = new MainInfoRepo(null);
		});
	}
	[Test]
	public void CreateObj_PassLocalDb()
	{
		//Act
		var repo = new MainInfoRepo(_db);
		//Assert
		Assert.Pass();
	}
	#region AsyncMethods

	[Test]
	public async Task FirstOrDefaultAsync_PassNull_ThrowException()
	{
		//Arrange
		//Act
		var res = await _repo.FirstOrDefaultAsync();
		//Assert
		var actualData = SeedDataCreator.CreateMainInfo();
		Assert.That(_comparer.Equals(res, actualData.FirstOrDefault(i => i.Id == 1)));
	}
	[Test]
	public async Task FirstOrDefaultAsync_PassExistingPredicates_ReturnExactValue()
	{
		//Arrange
		//Act
		var res = await _repo.FirstOrDefaultAsync(i => true);
		var res2 = await _repo.FirstOrDefaultAsync(i => i.Id == 2);
		var res3 = await _repo.FirstOrDefaultAsync(i => i.Description == "no desc");
		var res4 = await _repo.FirstOrDefaultAsync(i => i.Description == "no desc" && i.LightImagePath == "no image");
		var res5 = await _repo.FirstOrDefaultAsync(i => i.Description == "no desc" && i.LightImagePath == "no image" && i.LangId == 2);
		//Assert
		var actualData = SeedDataCreator.CreateMainInfo();
		Assert.That(_comparer.Equals(res, actualData.FirstOrDefault()), Is.True);
		Assert.That(_comparer.Equals(res2, actualData.FirstOrDefault(i => i.Id == 2)), Is.True);
		Assert.That(_comparer.Equals(res3, actualData.FirstOrDefault(i => i.Description == "no desc")), Is.True);
		Assert.That(_comparer.Equals(res4, actualData.FirstOrDefault(i => i.Description == "no desc" && i.LightImagePath == "no image")), Is.True);
		Assert.That(_comparer.Equals(res5, actualData.FirstOrDefault(i => i.Description == "no desc" && i.LightImagePath == "no image" && i.LangId == 2)), Is.True);
	}

	[Test]
	public async Task FirstOrDefaultAsync_PassNonExistingPredicates_ReturnNull()
	{
		//Arrange
		//Act
		var res = await _repo.FirstOrDefaultAsync(i => false);
		var res2 = await _repo.FirstOrDefaultAsync(i => i.Id != 1 && i.Id != 2);
		var res3 = await _repo.FirstOrDefaultAsync(i => i.Description != "no desc");
		var res4 = await _repo.FirstOrDefaultAsync(i => i.Description == "desc" || i.DarkImagePath == "drkimage");
		var res5 = await _repo.FirstOrDefaultAsync(i => i.Description == "description" || i.DarkImagePath == "drkimage" && i.LangId == 2);
		//Assert
		var actualData = SeedDataCreator.CreateMainInfo();
		Assert.That(res, Is.Null);
		Assert.That(res2, Is.Null);
		Assert.That(res3, Is.Null);
		Assert.That(res4, Is.Null);
		Assert.That(res5, Is.Null);
	}

	[Test]
	public void UpdateAsync_PassNull_ThrowException()
	{
		Assert.ThrowsAsync<ArgumentNullException>(async () =>
		{
			await _repo.UpdateAsync(null);
		});
	}
	[Test]
	public void UpdateAsync_PassNonExistingEntity_ThrowException()
	{
		Assert.ThrowsAsync<DbOperationException>(async () => await _repo.UpdateAsync(new MainInfo()));
		RollBack();
	}
	[Test]
	public void UpdateAsync_PassExistingInvalidEntity_ThrowException()
	{
		var entity = SeedDataCreator.CreateMainInfo().FirstOrDefault();
		entity!.Description = null;
		entity!.LightImagePath = null;
		entity!.LangId = 4;

		Assert.ThrowsAsync<DbOperationException>(async () => await _repo.UpdateAsync(entity));
		RollBack();
	}
	[Test]
	public async Task UpdateAsync_PassExistingValidEntity_UpdateEntity()
	{
		//Arrange
		var entity = SeedDataCreator.CreateMainInfo().FirstOrDefault();
		entity!.Description = "lorem ipsum";
		entity!.DarkImagePath = "fakePath";
		//Act
		await _repo.UpdateAsync(entity);
		//Assert
		var result = await _db.MainInfos.AsNoTracking().FirstOrDefaultAsync();
		Assert.That(result?.Description, Is.EqualTo(entity!.Description));
		Assert.That(result?.DarkImagePath, Is.EqualTo(entity!.DarkImagePath));
		//Annhilation
		RollBack();
	}
	#endregion

	#region NonAsyncMethods
	[Test]
	public void FirstOrDefault_PassNull_ThrowException()
	{
		//Arrange
		//Act
		var res = _repo.FirstOrDefault();
		//Assert
		var actualData = SeedDataCreator.CreateMainInfo();
		Assert.That(_comparer.Equals(res, actualData.FirstOrDefault(i => i.Id == 1)));
	}
	[Test]
	public void FirstOrDefault_PassExistingPredicates_ReturnExactValue()
	{
		//Arrange
		//Act
		var res = _repo.FirstOrDefault(i => true);
		var res2 = _repo.FirstOrDefault(i => i.Id == 2);
		var res3 = _repo.FirstOrDefault(i => i.Description == "no desc");
		var res4 = _repo.FirstOrDefault(i => i.Description == "no desc" && i.LightImagePath == "no image");
		var res5 = _repo.FirstOrDefault(i => i.Description == "no desc" && i.LightImagePath == "no image" && i.LangId == 2);
		//Assert
		var actualData = SeedDataCreator.CreateMainInfo();
		Assert.That(_comparer.Equals(res, actualData.FirstOrDefault()), Is.True);
		Assert.That(_comparer.Equals(res2, actualData.FirstOrDefault(i => i.Id == 2)), Is.True);
		Assert.That(_comparer.Equals(res3, actualData.FirstOrDefault(i => i.Description == "no desc")), Is.True);
		Assert.That(_comparer.Equals(res4, actualData.FirstOrDefault(i => i.Description == "no desc" && i.LightImagePath == "no image")), Is.True);
		Assert.That(_comparer.Equals(res5, actualData.FirstOrDefault(i => i.Description == "no desc" && i.LightImagePath == "no image" && i.LangId == 2)), Is.True);
	}

	[Test]
	public void FirstOrDefault_PassNonExistingPredicates_ReturnNull()
	{
		//Arrange
		//Act
		var res = _repo.FirstOrDefault(i => false);
		var res2 = _repo.FirstOrDefault(i => i.Id != 1 && i.Id != 2);
		var res3 = _repo.FirstOrDefault(i => i.Description != "no desc");
		var res4 = _repo.FirstOrDefault(i => i.Description == "desc" || i.DarkImagePath == "drkimage");
		var res5 = _repo.FirstOrDefault(i => i.Description == "description" || i.DarkImagePath == "drkimage" && i.LangId == 2);
		//Assert
		var actualData = SeedDataCreator.CreateMainInfo();
		Assert.That(res, Is.Null);
		Assert.That(res2, Is.Null);
		Assert.That(res3, Is.Null);
		Assert.That(res4, Is.Null);
		Assert.That(res5, Is.Null);
	}

	[Test]
	public void Update_PassNull_ThrowException()
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			_repo.Update(null);
		});

	}
	[Test]
	public void Update_PassNonExistingEntity_ThrowException()
	{
		Assert.Throws<DbOperationException>(() => _repo.Update(new MainInfo()));
		RollBack();
	}
	[Test]
	public void Update_PassExistingInvalidEntity_ThrowException()
	{
		var entity = SeedDataCreator.CreateMainInfo().FirstOrDefault();
		entity!.LightImagePath = null;
		entity!.DarkImagePath = null;
		entity!.LangId = 4;


		Assert.Throws<DbOperationException>(() => _repo.Update(entity));
		RollBack();
	}
	[Test]
	public void Update_PassExistingValidEntity_UpdateEntity()
	{
		//Arrange
		var entity = SeedDataCreator.CreateMainInfo().FirstOrDefault();
		entity!.DarkImagePath = "validImagePath";
		entity!.LightImagePath = "validImagePath";
		//Act
		_repo.Update(entity);
		//Assert
		var result = _db.MainInfos.AsNoTracking().FirstOrDefault();
		Assert.That(result?.LightImagePath, Is.EqualTo(entity!.LightImagePath));
		Assert.That(result?.DarkImagePath, Is.EqualTo(entity!.DarkImagePath));
		//Annhilation
		RollBack();
	}

	#endregion
}