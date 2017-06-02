using Simplify.Repository.Model;

namespace Simplify.FluentNHibernate.Examples.Domain.Model.Location
{
	public interface ICityName : INamedObject
	{
		ICity City { get; set; }

		string Language { get; set; }
	}
}