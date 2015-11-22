using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate;
using Simplify.FluentNHibernate;

namespace Simplify.Repository.FluentNHibernate.Repositories
{
	/// <summary>
	/// Provides generic repository pattern for easy NHibernate repositories implementation
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class GenericRepository<T>
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
		protected GenericRepository(ISession session)
		{
			Session = session;
		}

		/// <summary>
		/// Gets the single object by identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		protected T GetSingleByID(object id)
		{
			return Session.Get<T>(id);
		}

		/// <summary>
		/// Gets the single by object identifier exclusively.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		protected T GetSingleByIDExclusive(object id)
		{
			return Session.Get<T>(id, LockMode.Upgrade);
		}

		/// <summary>
		/// Gets the single object by query.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		protected T GetSingleByQuery(Expression<Func<T, bool>> query)
		{
			return Session.GetObject(query);
		}

		/// <summary>
		/// Gets the first object by query.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		protected T GetFirstByQuery(Expression<Func<T, bool>> query)
		{
			return Session.GetFirstObject(query);
		}

		/// <summary>
		/// Gets the multiple objects by query.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		protected IList<T> GetMultipleByQuery(Expression<Func<T, bool>> query)
		{
			return Session.GetList(query);
		}

		/// <summary>
		/// Gets the multiple objects by query.
		/// </summary>
		/// <typeparam name="TOrder">The type of the order.</typeparam>
		/// <param name="query">The query.</param>
		/// <param name="orderFunc">The ordering function.</param>
		/// <param name="orderDescending">if set to <c>true</c> then will be sorted descending.</param>
		/// <returns></returns>
		protected IList<T> GetMultipleByQueryOrderedList<TOrder>(Expression<Func<T, bool>> query, Expression<Func<T, TOrder>> orderFunc, bool orderDescending = false)
		{
			return Session.GetList(query, orderFunc, orderDescending);
		}

		/// <summary>
		/// Adds the object.
		/// </summary>
		/// <param name="entity">The entity.</param>
		protected object Add(T entity)
		{
			return Session.Save(entity);
		}

		/// <summary>
		/// Adds or update the object.
		/// </summary>
		/// <param name="entity">The entity.</param>
		protected void AddOrUpdate(T entity)
		{
			Session.SaveOrUpdate(entity);
		}

		/// <summary>
		/// Deletes the object.
		/// </summary>
		/// <param name="entity">The entity.</param>
		protected void Delete(T entity)
		{
			Session.Delete(entity);
		}

		/// <summary>
		/// Updates the object.
		/// </summary>
		/// <param name="entity">The entity.</param>
		protected void Update(T entity)
		{
			Session.Update(entity);
		}
	}
}