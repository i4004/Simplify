using DryIoc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector.Advanced;
using Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection.Tester.Setup;
using Simplify.DI.Provider.SimpleInjector;
using System;
using System.Linq;
using System.Reflection;

namespace Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection.Tester
{
	public class Startup
	{
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			// SimpleInjector specific workaround

			var container = new SimpleInjectorDIProvider();
			container.Container.Options.ConstructorResolutionBehavior = new GreediestConstructorBehavior();
			container.Container.Options.AllowOverridingRegistrations = true;

			// DryIoc specific workaround

			//var container = new DryIocDIProvider
			//{
			//	Container = new Container()
			//		.With(rules => rules.With(FactoryMethod.ConstructorWithResolvableArguments))
			//};

			DIContainer.Current = container;

			// Registrations using `services`
			services.Register();

			// Registrations using `DIContainer.Current`
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

	public class GreediestConstructorBehavior : IConstructorResolutionBehavior
	{
		public ConstructorInfo GetConstructor(Type implementationType) => (
				from ctor in implementationType.GetConstructors()
				orderby ctor.GetParameters().Length descending
				select ctor)
			.First();
	}
}