using Microsoft.AspNetCore.Components;

namespace MyWebsite.Presentation.Admin.Shared
{
	public abstract class BaseComponent:ComponentBase
	{
		[Inject]
		public IHttpContextAccessor httpContextAccessor { get; set; }
		protected override Task OnInitializedAsync()
		{
			httpContextAccessor.HttpContext!.Response.Headers.Add("X-Component", GetType().Name.Replace("Component",""));
			return base.OnInitializedAsync();
		}
	}
}