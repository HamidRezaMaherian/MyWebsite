using MyWebsite.Domain.Entities.Info;

namespace MyWebsite.Presentation.Model
{
	internal class HomeVM
	{
		public MainInfo TempInfo { get; init; }
		public IReadOnlyCollection<Project> Projects { get; init; }
		public IReadOnlyCollection<AboutMeKeyValue> AboutMeKeyValues { get; init; }
		public IReadOnlyCollection<Skill> Skills { get; init; }
		public IReadOnlyCollection<Experience> Experiences { get; init; }
		public IReadOnlyCollection<Education> Educations { get; init; }
		public ContactMe ContactMe { get; init; }
		public AboutMe AboutMe { get; init; }

	}
}