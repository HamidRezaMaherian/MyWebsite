using Microsoft.EntityFrameworkCore;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Persistent;

namespace MyWebsite.Infrastructure.Tests.Repositories.Info;

[TestFixture]
public class AboutMeRepoTests
{
	private ApplicationDbContext _db;
	private AboutMeRepo _repo;

	private void RollBack()
	{
		var oldEntity = SeedDataCreator.CreateAboutMe().FirstOrDefault()!;
		_db.AboutMe.Update(oldEntity);
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
		_repo = new AboutMeRepo(_db);
	}
	[Test]
	public void CreateObj_PassNull()
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			var repo = new AboutMeRepo(null);
		});
	}
	[Test]
	public void CreateObj_PassLocalDb()
	{
		//Act
		var repo = new AboutMeRepo(_db);
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
		var actualData = SeedDataCreator.CreateAboutMe();
		Assert.That(res, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 1)));
	}
	[Test]
	public async Task FirstOrDefaultAsync_PassExistingPredicates_ReturnExactValue()
	{
		//Act
		var res = await _repo.FirstOrDefaultAsync(i => true);
		var res2 = await _repo.FirstOrDefaultAsync(i => i.Id == 2);
		var res3 = await _repo.FirstOrDefaultAsync(i => i.FilePath == "no file" && i.LangId == 1);
		var res4 = await _repo.FirstOrDefaultAsync(i => i.LangId == 2);
		//Assert
		var actualData = SeedDataCreator.CreateAboutMe();
		Assert.That(res, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 1)));
		Assert.That(res2, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 2)));
		Assert.That(res3, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 1)));
		Assert.That(res4, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 2)));
	}

	[Test]
	public async Task FirstOrDefaultAsync_PassNonExistingPredicates_ReturnNull()
	{
		//Arrange
		//Act
		var res = await _repo.FirstOrDefaultAsync(i => false);
		var res2 = await _repo.FirstOrDefaultAsync(i => i.Id != 1 && i.Id != 2);
		var res3 = await _repo.FirstOrDefaultAsync(i => i.FilePath != "no file");
		var res4 = await _repo.FirstOrDefaultAsync(i => i.FilePath == "n file" || i.LangId == 3);
		var res5 = await _repo.FirstOrDefaultAsync(i => i.FilePath != "no file" && i.LangId == 2);
		//Assert
		var actualData = SeedDataCreator.CreateAboutMe();
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
	public async Task UpdateAsync_PassNonExistingEntity_ThrowException()
	{
		try
		{
			await _repo.UpdateAsync(new AboutMe());
		}
		catch (Exception)
		{
			Assert.Pass();
			return;
		}
		RollBack();
		Assert.Fail("no exception thrown");
	}
	[Test]
	public async Task UpdateAsync_PassExistingInvalidEntity_ThrowException()
	{
		var entity = SeedDataCreator.CreateAboutMe().FirstOrDefault();
		entity!.FilePath = null;
		entity!.LangId = 4;
		try
		{
			await _repo.UpdateAsync(entity);
		}
		catch (Exception)
		{
			Assert.Pass();
			return;
		}
		RollBack();
		Assert.Fail("no exception thrown");
	}
	[Test]
	public async Task UpdateAsync_PassExistingValidEntity_UpdateEntity()
	{
		//Arrange
		var entity = SeedDataCreator.CreateAboutMe().FirstOrDefault();
		entity!.FilePath = "KJFGOE502K30FOJWO320930FJOW7490283";
		entity!.LangId = 2;
		//Act
		await _repo.UpdateAsync(entity);
		//Assert
		var result = await _db.AboutMe.AsNoTracking().FirstOrDefaultAsync();
		Assert.That(result?.FilePath, Is.EqualTo(entity!.FilePath));
		Assert.That(result?.LangId, Is.EqualTo(entity!.LangId));
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
		var actualData = SeedDataCreator.CreateAboutMe();
		Assert.That(res, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 1)));
	}
	[Test]
	public void FirstOrDefault_PassExistingPredicates_ReturnExactValue()
	{
		//Act
		var res = _repo.FirstOrDefault(i => true);
		var res2 = _repo.FirstOrDefault(i => i.Id == 2);
		var res3 = _repo.FirstOrDefault(i => i.FilePath == "no file" && i.LangId == 1);
		var res4 = _repo.FirstOrDefault(i => i.LangId == 2);
		//Assert
		var actualData = SeedDataCreator.CreateAboutMe();
		Assert.That(res, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 1)));
		Assert.That(res2, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 2)));
		Assert.That(res3, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 1)));
		Assert.That(res4, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 2)));
	}

	[Test]
	public void FirstOrDefault_PassNonExistingPredicates_ReturnNull()
	{
		//Arrange
		//Act
		var res = _repo.FirstOrDefault(i => false);
		var res2 = _repo.FirstOrDefault(i => i.Id != 1 && i.Id != 2);
		var res3 = _repo.FirstOrDefault(i => i.FilePath != "no file");
		var res4 = _repo.FirstOrDefault(i => i.FilePath == "n file" || i.LangId == 3);
		var res5 = _repo.FirstOrDefault(i => i.FilePath != "no file" && i.LangId == 2);
		//Assert
		var actualData = SeedDataCreator.CreateAboutMe();
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
		try
		{
			_repo.Update(new AboutMe());
		}
		catch (Exception)
		{
			Assert.Pass();
			return;
		}
		RollBack();
		Assert.Fail("no exception thrown");
	}
	[Test]
	public void Update_PassExistingInvalidEntity_ThrowException()
	{
		var entity = SeedDataCreator.CreateAboutMe().FirstOrDefault();
		entity!.FilePath = null;
		entity!.LangId = 4;
		try
		{
			_repo.Update(entity);
		}
		catch (Exception)
		{
			Assert.Pass();
			return;
		}
		RollBack();
		Assert.Fail("no exception thrown");
	}
	[Test]
	public void Update_PassExistingValidEntity_UpdateEntity()
	{
		//Arrange
		var entity = SeedDataCreator.CreateAboutMe().FirstOrDefault();
		entity!.FilePath = "KJFGOE502K30FOJWO320930FJOW7490283";
		entity!.LangId = 2;
		//Act
		_repo.Update(entity);
		//Assert
		var result = _db.AboutMe.AsNoTracking().FirstOrDefault();
		Assert.That(result?.FilePath, Is.EqualTo(entity!.FilePath));
		Assert.That(result?.LangId, Is.EqualTo(entity!.LangId));
		//Annhilation
		RollBack();
	}

	#endregion
}