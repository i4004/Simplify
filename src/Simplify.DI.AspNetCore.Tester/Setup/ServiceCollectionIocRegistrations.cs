using Microsoft.Extensions.DependencyInjection;

namespace Simplify.DI.AspNetCore.Tester.Setup
{
	public static class ServiceCollectionIocRegistrations
	{
		public static void Register(this IServiceCollection services)
		{
			services.AddScoped<Dependency2>();
		}
	}
}