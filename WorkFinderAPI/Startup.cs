using DatabaseDao;
using JsonConvertor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using DatabaseConfiguration;

namespace WorkFinderAPI
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<Storage>(_ => new PostgreStorage(Configuration.DefaultConnection));
			services.AddScoped<JsonConvertorEngine>(_ => new JsonConvertorEngine());
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseHsts();
			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
