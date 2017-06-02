using System;
using System.Linq;
using Simplify.FluentNHibernate.Examples.Domain.Model.Location;
using Simplify.Repository.Repositories;

namespace Simplify.FluentNHibernate.Examples.Domain.Location
{
	public class CitiesService : ICitiesService
	{
		private readonly IGenericRepository<ICity> _repository;

		public CitiesService(IGenericRepository<ICity> repository)
		{
			_repository = repository;
		}

		public ICity GetCity(string cityName)
		{
			if (cityName == null) throw new ArgumentNullException(nameof(cityName));

			return _repository.GetSingleByQuery(x => x.CityNames.Any(n => n.Name == cityName));
		}
	}
}