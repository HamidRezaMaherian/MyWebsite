using MyWebsite.Presentation;
using static MyWebsite.Infrastructure.IORegistery;
public class Program
{
	public static async Task Main(string[] args)
	{

		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		builder.Services.AddHealthChecks();
		builder.Services.RegisterInfrastructure(builder.Configuration);

		var app = builder.Build();

		await app.Services.MigrateDatabase();
		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseStaticFiles();
		app.UseRouting();
		app.UseAuthorization();
		app.UseHealthChecks("/healthstatus");
		app.MapControllerRoute(
			 name: "default",
			 pattern: "{controller=Home}/{action=Index}/{id?}");
		app.UseRouteHeaderMiddleware();
		app.Run();
	}
}
