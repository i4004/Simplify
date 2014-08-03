using System;
using System.Linq.Expressions;

using Simplify.FluentNHibernate.Examples.Domain.Entities;

namespace Simplify.FluentNHibernate.Examples.Domain
{
	public interface ICitiesRepository
	{
		City GetCity(Expression<Func<City, bool>> filterFunc);
		CityName GetCityName(Expression<Func<CityName, bool>> filterFunc);
	}
}