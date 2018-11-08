using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Simplify.Repository
{
	/// <summary>
	/// Represent generic repository pattern for easy NHibernate repositories implementation
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IGenericRepository<T>
		where T : class
	{
		/// <summary>
		/// Gets the single object by identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		T GetSingleByID(object id);

		/// <summary>
		/// Gets the single object by identifier exclusively.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		T GetSingleByIDExclusive(object id);

		/// <summary>
		/// Gets the single object by query.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		T GetSingleByQuery(Expression<Func<T, bool>> query);

		/// <summary>
		/// Gets the first object by query.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		T GetFirstByQuery(Expression<Func<T, bool>> query);

		/// <summary>
		/// Gets the multiple objects by query.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="customProcessing">The custom processing.</param>
		/// <returns></returns>
		IList<T> GetMultipleByQuery(Expression<Func<T, bool>> query = null, Func<IQueryable<T>, IQueryable<T>> customProcessing = null);

		/// <summary>
		/// Gets the multiple paged elements list.
		/// </summary>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="itemsPerPage">The items per page number.</param>
		/// <param name="query">The query.</param>
		/// <param name="customProcessing">The custom processing.</param>
		/// <returns></returns>
		IList<T> GetPaged(int pageIndex, int itemsPerPage,
			Expression<Func<T, bool>> query = null, Func<IQueryable<T>, IQueryable<T>> customProcessing = null);

		/// <summary>
		/// Gets the number of elements.
		/// </summary>
		/// <param name="query">The query.</param>
		int GetCount(Expression<Func<T, bool>> query = null);

		/// <summary>
		/// Adds the object.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>The generated identifier</returns>
		object Add(T entity);

		/// <summary>
		/// Deletes the object.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Delete(T entity);

		/// <summary>
		/// Updates the object.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Update(T entity);
	}
}