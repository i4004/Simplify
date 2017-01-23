using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate;
using Simplify.FluentNHibernate;
using Simplify.Repository.Repositories;

namespace Simplify.Repository.FluentNHibernate.Repositories
{
	/// <summary>
	/// Provides generic repository pattern for easy NHibernate repositories implementation
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class GenericRepository<T> : IGenericRepository<T>
		where T : class
	{
		/// <summary>
		/// The NHibernate session
		/// </summary>
		protected readonly ISession Session;

		/// <summary>
		/// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
		/// </summary>
		/// <param name="session">The session.</param>
		public GenericRepository(ISession session)
		{
			Session = session;
		}

		/// <summary>
		/// Gets the single object by identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public T GetSingleByID(object id)
		{
			return Session.Get<T>(id);
		}

		/// <summary>
		/// Gets the single object by identifier exclusively.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public T GetSingleByIDExclusive(object id)
		{
			return Session.Get<T>(id, LockMode.Upgrade);
		}

		/// <summary>
		/// Gets the single object by query.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public T GetSingleByQuery(Expression<Func<T, bool>> query)
		{
			return Session.GetObject(query);
		}

		/// <summary>
		/// Gets the first object by query.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public T GetFirstByQuery(Expression<Func<T, bool>> query)
		{
			return Session.GetFirstObject(query);
		}

		/// <summary>
		/// Gets the multiple objects by query.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public IList<T> GetMultipleByQuery(Expression<Func<T, bool>> query)
		{
			return Session.GetList(query);
		}

		/// <summary>
		/// Gets the multiple objects by query.
		/// </summary>
		/// <typeparam name="TOrder">The type of the order.</typeparam>
		/// <param name="query">The query.</param>
		/// <param name="orderExpression">The ordering expression.</param>
		/// <param name="orderDescending">if set to <c>true</c> then will be sorted descending.</param>
		/// <returns></returns>
		public IList<T> GetMultipleByQueryOrdered<TOrder>(Expression<Func<T, bool>> query, Expression<Func<T, TOrder>> orderExpression, bool orderDescending = false)
		{
			return Session.GetList(query, orderExpression, orderDescending);
		}

		/// <summary>
		/// Gets the multiple paged elements list.
		/// </summary>
		/// <typeparam name="TOrder">The type of the order.</typeparam>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="itemsPerPage">The items per page number.</param>
		/// <param name="query">The query.</param>
		/// <param name="orderExpression">The ordering expression.</param>
		/// <param name="orderDescending">if set to <c>true</c> then will be sorted descending.</param>
		/// <returns></returns>
		public IList<T> GetPaged<TOrder>(int pageIndex, int itemsPerPage,
			Expression<Func<T, bool>> query = null,
			Expression<Func<T, TOrder>> orderExpression = null,
			bool orderDescending = false)
		{
			return Session.GetListPaged(pageIndex, itemsPerPage, query, orderExpression, orderDescending);
		}

		/// <summary>
		/// Gets the number of elements.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public int GetCount(Expression<Func<T, bool>> query = null)
		{
			return Session.GetCount(query);
		}

		/// <summary>
		/// Adds the object.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public object Add(T entity)
		{
			return Session.Save(entity);
		}

		/// <summary>
		/// Adds or update the object.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public void AddOrUpdate(T entity)
		{
			Session.SaveOrUpdate(entity);
		}

		/// <summary>
		/// Deletes the object.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public void Delete(T entity)
		{
			Session.Delete(entity);
		}

		/// <summary>
		/// Updates the object.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public void Update(T entity)
		{
			Session.Update(entity);
		}
	}
}