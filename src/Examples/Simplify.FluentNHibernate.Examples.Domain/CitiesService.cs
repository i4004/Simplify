using System;

using Simplify.FluentNHibernate.Examples.Domain.Entities;

namespace Simplify.FluentNHibernate.Examples.Domain
{
	public class CitiesService : ICItiesService
	{
		private readonly ICitiesRepository _repository;

		public CitiesService(ICitiesRepository repository)
		{
			_repository = repository;
		}

		public City GetCity(string cityName)
		{
			if (cityName == null) throw new ArgumentNullException("cityName");

			var retrievedCityName = _repository.GetCityName(x => x.Name == cityName);
			return retrievedCityName != null ? retrievedCityName.City : null;
		}
	}
}
