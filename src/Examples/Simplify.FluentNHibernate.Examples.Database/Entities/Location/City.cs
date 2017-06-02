using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Simplify.FluentNHibernate.Examples.Domain.Model.Location;
using Simplify.Repository.FluentNHibernate.Entities;

namespace Simplify.FluentNHibernate.Examples.Database.Entities.Location
{
	public class City : IdentityObject, ICity
	{
		public virtual IList<ICityName> CityNames { get; set; } = new List<ICityName>();

		public virtual string LocalizableName
		{
			get
			{
				if (CityNames.Count == 0) return "";

				var cityName = CityNames.FirstOrDefault(p => p.Language == Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);

				return cityName != null ? cityName.Name : "";
			}
		}
	}
}