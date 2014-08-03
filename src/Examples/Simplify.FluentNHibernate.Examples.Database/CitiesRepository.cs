using System;
using System.Linq.Expressions;

using NHibernate;

using Simplify.FluentNHibernate.Examples.Domain;
using Simplify.FluentNHibernate.Examples.Domain.Entities;

namespace Simplify.FluentNHibernate.Examples.Database
{
	public class CitiesRepository : ICitiesRepository
	{
		private readonly ISession _session;

		public CitiesRepository(ISession session)
		{
			_session = session;
		}

		public City GetCity(Expression<Func<City, bool>> filterFunc)
		{
			return _session.GetObject(filterFunc);
		}

		public CityName GetCityName(Expression<Func<CityName, bool>> filterFunc)
		{
			return _session.GetObject(filterFunc);			
		}
	}
}