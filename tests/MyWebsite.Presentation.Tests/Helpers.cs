using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyWebsite.Presentation.Model;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace MyWebsite.Presentation.Tests.Utils
{
	internal static class Helpers
	{
		public static void RegisterFakeRepositories(this IServiceCollection services)
		{
			var fakeRepos = Assembly.GetExecutingAssembly().GetTypes().Where(i => i.Name.StartsWith("Fake") && i.Name.EndsWith("Repo"));
			var dataGenerators = Assembly.GetExecutingAssembly().GetTypes().Where(i => i.Name.StartsWith("Fake") && i.Name.EndsWith("DataGenerator")).ToImmutableArray();
			foreach (var dg in dataGenerators)
				services.AddSingleton(dg);

			foreach (var fr in fakeRepos)
				services.Replace(new ServiceDescriptor(fr.GetInterfaces().First(), fr, ServiceLifetime.Scoped));
		}
		internal class HomeVMValueComparer : IEqualityComparer<HomeVM>
		{
			public bool Equals(HomeVM x, HomeVM y)
			{
				return x.ContactMe.Id == y.ContactMe.Id
						 && x.AboutMe.Id == y.AboutMe.Id
						 && x.TempInfo.Id == y.TempInfo.Id
						 && Enumerable.SequenceEqual(x.Educations.Select(i => i.Id), y.Educations.Select(i => i.Id))
						 && Enumerable.SequenceEqual(x.Experiences.Select(i => i.Id), y.Experiences.Select(i => i.Id))
						 && Enumerable.SequenceEqual(x.AboutMeKeyValues.Select(i => i.Id), y.AboutMeKeyValues.Select(i => i.Id))
						 && Enumerable.SequenceEqual(x.Projects.Select(i => i.Id), y.Projects.Select(i => i.Id))
						 && Enumerable.SequenceEqual(x.Skills.Select(i => i.Id), y.Skills.Select(i => i.Id));
			}

			public int GetHashCode([DisallowNull] HomeVM obj)
			{
				return obj.GetHashCode();
			}
		}

		public static class Statics
		{
			public const string REQUIRED_PROPS = "RequiredProps";
			public const string OPTIONAL_PROPS = "RequiredProps";
		}
	}
}
