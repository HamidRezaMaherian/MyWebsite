using Bogus;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Repositories.Info;
using static MyWebsite.Infrastructure.Tests.Helpers;

namespace MyWebsite.Infrastructure.Tests.Repositories.Info;

[TestFixture]
public class AboutMeKeyValueRepoTests : BaseLanguageRepoTests<AboutMeKeyValue, IAboutMeKeyValueRepo>
{
	protected override IAboutMeKeyValueRepo CreateRepo()
	{
		return new AboutMeKeyValueRepo(_db);
	}

	protected override Faker<AboutMeKeyValue> CreateDataFaker()
	{
		return new Faker<AboutMeKeyValue>()
					.RuleSet("RequiredProps", (rs) =>
					{
						rs.RuleFor(i => i.Key, f => f.Random.Guid().ToString());
						rs.RuleFor(i => i.Value, f => f.Random.Word());
						rs.RuleFor(i => i.LangId, f => f.Random.Int(1, 2));
						rs.RuleFor(i => i.IsActive, f => f.Random.Bool());
					});
	}

	protected override IEqualityComparer<AboutMeKeyValue> CreateComparer()
	{
		return new AboutMeKeyValueComparer();
	}
}