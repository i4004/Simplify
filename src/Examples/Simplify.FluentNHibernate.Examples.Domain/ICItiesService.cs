using Simplify.FluentNHibernate.Examples.Domain.Entities;

namespace Simplify.FluentNHibernate.Examples.Domain
{
	public interface ICItiesService
	{
		City GetCity(string cityName);
	}
}