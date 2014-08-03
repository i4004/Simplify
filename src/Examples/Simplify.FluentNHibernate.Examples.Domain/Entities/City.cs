using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Simplify.FluentNHibernate.Examples.Domain.Entities
{
	public class City
	{
		private IList<CityName> _cityNames = new List<CityName>();

		public virtual int ID { get; set; }

		public virtual IList<CityName> CityNames
		{
			get { return _cityNames; }
			set { _cityNames = value; }
		}

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
