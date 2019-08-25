using System.Data;

namespace Simplify.FluentNHibernate.Examples.Domain.Location
{
	public class TransactCitiesService : ICitiesService
	{
		private readonly ICitiesService _baseService;
		private readonly IExampleUnitOfWork _unitOfWork;

		public TransactCitiesService(ICitiesService baseService, IExampleUnitOfWork unitOfWork)
		{
			_baseService = baseService;
			_unitOfWork = unitOfWork;
		}

		public ICity GetCity(string cityName)
		{
			_unitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted);

			var item = _baseService.GetCity(cityName);

			_unitOfWork.Commit();

			return item;
		}
	}
}