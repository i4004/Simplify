using Simplify.FluentNHibernate.Examples.Domain.Model.Location;

namespace Simplify.FluentNHibernate.Examples.Domain.Cities
{
	public interface ICitiesService
	{
		ICity GetCity(string cityName);
	}
}