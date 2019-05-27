using System.Data;

namespace Simplify.FluentNHibernate.Examples.Domain.Location
{
	public class CitiesAgent : ICitiesAgent
	{
		private readonly ICitiesService _service;
		private readonly IExampleUnitOfWork _unitOfWork;

		public CitiesAgent(ICitiesService service, IExampleUnitOfWork unitOfWork)
		{
			_service = service;
			_unitOfWork = unitOfWork;
		}

		public ICity GetCity(string cityName)
		{
			_unitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted);

			var item = _service.GetCity(cityName);

			_unitOfWork.Commit();

			return item;
		}
	}
}