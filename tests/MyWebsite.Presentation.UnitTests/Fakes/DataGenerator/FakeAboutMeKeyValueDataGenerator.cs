using Bogus;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Presentation.Tests.Utils;

public class FakeAboutMeKeyValueDataGenerator : Faker<AboutMeKeyValue>
{
	public FakeAboutMeKeyValueDataGenerator()
	{
		RuleSet(Helpers.Statics.REQUIRED_PROPS, (rs) =>
		 {
			 rs.RuleFor(i => i.Key, f => f.Random.Guid().ToString());
			 rs.RuleFor(i => i.Value, f => f.Random.Word());
			 rs.RuleFor(i => i.LangId, f => f.Random.Int(1, 2));
			 rs.RuleFor(i => i.IsActive, f => f.Random.Bool());
		 });
	}
}
