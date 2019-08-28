using DryIoc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Simplify.DI.AspNetCore.Tester.Setup;
using Simplify.DI.Provider.DryIoc;
using System;

namespace Simplify.DI.AspNetCore.Tester
{
	public class Startup
	{
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			// Registrations using `services` here

			// DryIoc specific workaround

			var container = new DryIocDIProvider
			{
				Container = new Container()
					.With(rules => rules.With(FactoryMethod.ConstructorWithResolvableArguments))
			};

			DIContainer.Current = container;

			// Registrations using `DIContainer.Current` here
			IocRegistrations.Register();

			return DIContainer.Current.IntegrateWithMicrosoftDependencyInjectionAndVerify(services);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.Run(x => x.Response.WriteAsync("Hello World!"));
		}
	}
}