using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Persistent;
using MyWebsite.Infrastructure.Repositories.Info;
using System.Linq.Expressions;

namespace MyWebsite.Infrastructure.Tests.Repositories.Info;

[TestFixture]
public class ContactMeRepoTests
{
	private ApplicationDbContext _db;
	private ContactMeRepo _repo;

	private void RollBack()
	{
		var oldEntity = SeedDataCreator.CreateContactMe().FirstOrDefault()!;
		_db.ContactMe.Update(oldEntity);
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
		_repo = new ContactMeRepo(_db);
	}
	[Test]
	public void CreateObj_PassNull()
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			var repo = new ContactMeRepo(null);
		});
	}
	[Test]
	public void CreateObj_PassLocalDb()
	{
		//Act
		var repo = new ContactMeRepo(_db);
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
		var actualData = SeedDataCreator.CreateContactMe();
		Assert.That(res, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 1)));
	}
	[Test]
	public async Task FirstOrDefaultAsync_PassExistingPredicates_ReturnExactValue()
	{
		//Arrange
		//Act
		var res = await _repo.FirstOrDefaultAsync(i => true);
		var res2 = await _repo.FirstOrDefaultAsync(i => i.Id == 2);
		var res3 = await _repo.FirstOrDefaultAsync(i => i.Email == "test@test");
		var res4 = await _repo.FirstOrDefaultAsync(i => i.Email == "test@test" && i.PhoneNumber == "09304422204");
		var res5 = await _repo.FirstOrDefaultAsync(i => i.Email == "test@test" && i.PhoneNumber == "09304422204" && i.LangId == 2);
		//Assert
		var actualData = SeedDataCreator.CreateContactMe();
		Assert.That(res, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 1)));
		Assert.That(res2, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 2)));
		Assert.That(res3, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 1)));
		Assert.That(res4, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 1)));
		Assert.That(res5, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 2)));
	}

	[Test]
	public async Task FirstOrDefaultAsync_PassNonExistingPredicates_ReturnNull()
	{
		//Arrange
		//Act
		var res = await _repo.FirstOrDefaultAsync(i => false);
		var res2 = await _repo.FirstOrDefaultAsync(i => i.Id != 1 && i.Id != 2);
		var res3 = await _repo.FirstOrDefaultAsync(i => i.Email != "test@test");
		var res4 = await _repo.FirstOrDefaultAsync(i => i.Email == "test@te" || i.PhoneNumber == "094422204");
		var res5 = await _repo.FirstOrDefaultAsync(i => i.Email == "te@test" || i.PhoneNumber == "0304422204" && i.LangId == 2);
		//Assert
		var actualData = SeedDataCreator.CreateContactMe();
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
			await _repo.UpdateAsync(new ContactMe());
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
		var entity = SeedDataCreator.CreateContactMe().FirstOrDefault();
		entity!.Email = null;
		entity!.PhoneNumber = null;
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
		var entity = SeedDataCreator.CreateContactMe().FirstOrDefault();
		entity!.Email = "info@hamidrm.ir";
		entity!.PhoneNumber = "09304422204";
		//Act
		await _repo.UpdateAsync(entity);
		//Assert
		var result = await _db.ContactMe.AsNoTracking().FirstOrDefaultAsync();
		Assert.That(result?.Email, Is.EqualTo(entity!.Email));
		Assert.That(result?.PhoneNumber, Is.EqualTo(entity!.PhoneNumber));
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
		var actualData = SeedDataCreator.CreateContactMe();
		Assert.That(res, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 1)));
	}
	[Test]
	public void FirstOrDefault_PassExistingPredicates_ReturnExactValue()
	{
		//Arrange
		//Act
		var res = _repo.FirstOrDefault(i => true);
		var res2 = _repo.FirstOrDefault(i => i.Id == 2);
		var res3 = _repo.FirstOrDefault(i => i.Email == "test@test");
		var res4 = _repo.FirstOrDefault(i => i.Email == "test@test" && i.PhoneNumber == "09304422204");
		var res5 = _repo.FirstOrDefault(i => i.Email == "test@test" && i.PhoneNumber == "09304422204" && i.LangId == 2);
		//Assert
		var actualData = SeedDataCreator.CreateContactMe();
		Assert.That(res, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 1)));
		Assert.That(res2, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 2)));
		Assert.That(res3, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 1)));
		Assert.That(res4, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 1)));
		Assert.That(res5, Is.EqualTo(actualData.FirstOrDefault(i => i.Id == 2)));
	}

	[Test]
	public void FirstOrDefault_PassNonExistingPredicates_ReturnNull()
	{
		//Arrange
		//Act
		var res = _repo.FirstOrDefault(i => false);
		var res2 = _repo.FirstOrDefault(i => i.Id != 1 && i.Id != 2);
		var res3 = _repo.FirstOrDefault(i => i.Email != "test@test");
		var res4 = _repo.FirstOrDefault(i => i.Email == "test@te" || i.PhoneNumber == "094422204");
		var res5 = _repo.FirstOrDefault(i => i.Email == "te@test" || i.PhoneNumber == "0304422204" && i.LangId == 2);
		//Assert
		var actualData = SeedDataCreator.CreateContactMe();
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
			_repo.Update(new ContactMe());
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
		var entity = SeedDataCreator.CreateContactMe().FirstOrDefault();
		entity!.Email = null;
		entity!.PhoneNumber = null;
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
		var entity = SeedDataCreator.CreateContactMe().FirstOrDefault();
		entity!.Email = "info@hamidrm.ir";
		entity!.PhoneNumber = "09304422204";
		//Act
		_repo.Update(entity);
		//Assert
		var result = _db.ContactMe.AsNoTracking().FirstOrDefault();
		Assert.That(result?.Email, Is.EqualTo(entity!.Email));
		Assert.That(result?.PhoneNumber, Is.EqualTo(entity!.PhoneNumber));
		//Annhilation
		RollBack();
	}

	#endregion
}