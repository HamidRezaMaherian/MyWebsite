using Microsoft.AspNetCore.Components;

namespace MyWebsite.Presentation.Admin.Shared
{
	public abstract class BaseComponent : ComponentBase
	{
		[Inject]
		protected IHttpContextAccessor HttpContextAccessor { get; set; }
		protected override Task OnInitializedAsync()
		{
			HttpContextAccessor.HttpContext!.Response.Headers.Add("X-Component", GetType().Name.Replace("Component", ""));
			return base.OnInitializedAsync();
		}
	}
}