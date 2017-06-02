using Simplify.FluentNHibernate.Examples.Domain.Model.Location;

namespace Simplify.FluentNHibernate.Examples.Domain.Location
{
	public interface ICitiesService
	{
		ICity GetCity(string cityName);
	}
}