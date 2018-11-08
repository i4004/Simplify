using System.Collections.Generic;
using Simplify.Repository;

namespace Simplify.FluentNHibernate.Examples.Domain.Location
{
	public interface ICity : IIdentityObject
	{
		IList<ICityName> CityNames { get; set; }

		string LocalizableName { get; }
	}
}