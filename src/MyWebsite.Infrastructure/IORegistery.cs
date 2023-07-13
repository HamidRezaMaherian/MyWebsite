using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyWebsite.Infrastructure.Persistent;
using System.Data.Common;
using System.Reflection;

namespace MyWebsite.Infrastructure
{
	public static class IORegistery
	{
		public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.RegisterDataBase(configuration.GetConnectionString("DefaultConnection"));
			var currentAssembly = Assembly.GetAssembly(typeof(IORegistery));
			var repositories = currentAssembly!.DefinedTypes.Where(i => i.GetCustomAttribute<RepositoryConcreteAttribute>() is not null);
			foreach (var item in repositories)
			{
				var baseInterface = item.GetCustomAttribute<RepositoryConcreteAttribute>()!.RepositoryType; ;
				services.Add(new ServiceDescriptor(baseInterface!, item, ServiceLifetime.Scoped));
			}
		}
		public static void RegisterDataBase(this IServiceCollection services, string connectionString)
		{
			services.RemoveAll<DbContextOptions<ApplicationDbContext>>();
			services.RemoveAll<DbConnection>();
			services.RemoveAll<ApplicationDbContext>();
			services.AddDbContextPool<ApplicationDbContext>((options) => options.UseSqlServer(connectionString));
		}
		public static async Task MigrateDatabase(this IServiceProvider services)
		{
			var dbContext = services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
			if ((await dbContext!.Database.GetPendingMigrationsAsync()).Any())
			{
				await dbContext!.Database.MigrateAsync();
			}
		}
	}
}
