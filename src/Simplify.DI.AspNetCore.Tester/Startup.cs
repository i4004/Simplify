using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Simplify.DI.AspNetCore.Tester.Setup;
using System;

namespace Simplify.DI.AspNetCore.Tester
{
	public class Startup
	{
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			// Registrations using `services` here

			DIContainer.Current.RegisterFromServiceCollection(services);

			// Registrations using `DIContainer.Current` here
			IocRegistrations.Register();

			DIContainer.Current.Verify();

			return DIContainer.Current.CreateServiceProvider();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.Run(x => x.Response.WriteAsync("Hello World!"));
		}
	}
}