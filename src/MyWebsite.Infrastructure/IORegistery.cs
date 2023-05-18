using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWebsite.Infrastructure.Persistent;
using System.Reflection;

namespace MyWebsite.Infrastructure
{
	public static class IORegistery
	{
		public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContextPool<ApplicationDbContext>((options) => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			var currentAssembly = Assembly.GetAssembly(typeof(IORegistery));
			var repositories = currentAssembly!.DefinedTypes.Where(i => i.GetCustomAttribute<RepositoryConcreteAttribute>() is not null);
			foreach (var item in repositories)
			{
				var baseInterface = item.GetCustomAttribute<RepositoryConcreteAttribute>()!.RepositoryType;;
				services.Add(new ServiceDescriptor(baseInterface!, item, ServiceLifetime.Scoped));
			}
		}
		public static async Task MigrateDatabase(this IServiceCollection services)
		{
			var dbContext = services.BuildServiceProvider().GetService<ApplicationDbContext>();
			if ((await dbContext!.Database.GetPendingMigrationsAsync()).Any())
			{
				await dbContext!.Database.MigrateAsync();
			}
		}
	}
}
