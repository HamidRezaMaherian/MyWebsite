using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyWebsite.Infrastructure.Persistent;

namespace MyWebsite.Infrastructure.Tests;
public static class Helpers
{
	internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseSqlServer(Statics.dbConnectionString);

			return new ApplicationDbContext(optionsBuilder.Options);
		}
	}
}