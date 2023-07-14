using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyWebsite.Presentation.Admin.Shared;

namespace MyWebsite.Presentation.Admin.Pages.Home
{
	public partial class Home
	{
		[Inject]
		public IJSRuntime _jsRuntime { get; set; }
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await _jsRuntime.InvokeVoidAsync("ApexChart.week", "chart_2", null, "primary");
			await _jsRuntime.InvokeVoidAsync("ApexChart.week", "chart_3", null, "danger");
			await _jsRuntime.InvokeVoidAsync("ApexChart.week", "chart_4", null, "warning");
			await _jsRuntime.InvokeVoidAsync("ApexChart.week", "chart_5", null, "info");
		}
	}
}
