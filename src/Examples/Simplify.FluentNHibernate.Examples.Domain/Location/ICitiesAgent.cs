namespace Simplify.FluentNHibernate.Examples.Domain.Location
{
	public interface ICitiesAgent
	{
		ICity GetCity(string cityName);
	}
}