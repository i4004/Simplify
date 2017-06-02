using System.Collections.Generic;
using Simplify.Repository.Model;

namespace Simplify.FluentNHibernate.Examples.Domain.Model.Location
{
	public interface ICity : IIdentityObject
	{
		IList<ICityName> CityNames { get; set; }

		string LocalizableName { get; }
	}
}