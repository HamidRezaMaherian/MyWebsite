using Bogus;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Presentation.Tests.Utils;

public class FakeProjectDataGenerator : Faker<Project>
{
	public FakeProjectDataGenerator()
	{
		RuleSet(Helpers.Statics.REQUIRED_PROPS, (rs) =>
		 {
			 rs.RuleFor(i => i.Name, f => f.Name.JobTitle());
			 rs.RuleFor(i => i.ImagePath, f => f.Internet.UrlRootedPath());
			 rs.RuleFor(i => i.LangId, f => f.Random.Int(1, 2));
			 rs.RuleFor(i => i.IsActive, f => f.Random.Bool());
			 rs.RuleFor(i => i.Link, f => f.Internet.Url());
			 rs.RuleFor(i => i.Role, f => f.Name.JobType());
			 rs.RuleFor(i => i.DateTime, f => f.Date.Timespan().ToString());
			 rs.RuleFor(i => i.Techs, f => f.Name.JobDescriptor().ToString());
			 rs.RuleFor(i => i.Type, f => f.Name.JobType().ToString());
		 });
	}

}
