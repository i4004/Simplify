using Simplify.Repository;

namespace Simplify.FluentNHibernate.Examples.Domain.Location
{
	public interface ICityName : INamedObject
	{
		ICity City { get; set; }

		string Language { get; set; }
	}
}