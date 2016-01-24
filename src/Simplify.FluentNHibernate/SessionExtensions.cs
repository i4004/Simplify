using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using NHibernate;
using NHibernate.Linq;

namespace Simplify.FluentNHibernate
{
	/// <summary>
	/// NHibernate.ISession extensions
	/// </summary>
	public static class SessionExtensions
	{
		#region Single objects operations

		/// <summary>
		/// Get an object from single item table
		/// </summary>
		/// <typeparam name="T">Object type to get</typeparam>
		/// <param name="session">The NHibernate session.</param>
		/// <returns></returns>
		public static T GetSingleObject<T>(this ISession session)
			where T : class
		{
			return GetSingleObject<T>(session, LockMode.None);
		}

		/// <summary>
		/// Get an object from single item table
		/// </summary>
		/// <typeparam name="T">Object type to get</typeparam>
		/// <param name="session">The NHibernate session.</param>
		/// <param name="lockMode">The lock mode.</param>
		/// <returns></returns>
		public static T GetSingleObject<T>(this ISession session, LockMode lockMode)
			where T : class
		{
			return session.CreateCriteria<T>()
				.SetLockMode(lockMode)
				.UniqueResult<T>();
		}

		/// <summary>
		/// Get an object from database by filter (in case of several objects returned exception will be thrown)
		/// </summary>
		/// <typeparam name="T">The type of the object</typeparam>
		/// <param name="session">The NHibernate session.</param>
		/// <param name="query">Query</param>
		/// <returns></returns>
		public static T GetObject<T>(this ISession session, Expression<Func<T, bool>> query = null)
			where T : class
		{
			var queryable = session.Query<T>();

			if (query != null)
				queryable = queryable.Where(query);

			return queryable.Select(x => x).SingleOrDefault();
		}

		/// <summary>
		/// Get a first object from database by filter
		/// </summary>
		/// <typeparam name="T">The type of the object</typeparam>
		/// <param name="session">The NHibernate session.</param>
		/// <param name="query">Query</param>
		/// <returns></returns>
		public static T GetFirstObject<T>(this ISession session, Expression<Func<T, bool>> query = null)
			where T : class
		{
			var queryable = session.Query<T>();

			if (query != null)
				queryable = queryable.Where(query);

			return queryable.Select(x => x).FirstOrDefault();
		}

		/// <summary>
		/// Get and cache an object from database by filter (in case of several objects returned exception will be thrown)
		/// </summary>
		/// <typeparam name="T">The type of the object</typeparam>
		/// <param name="session">The NHibernate session.</param>
		/// <param name="query">Query</param>
		/// <returns></returns>
		public static T GetObjectCacheable<T>(this ISession session, Expression<Func<T, bool>> query = null)
			where T : class
		{
			var queryable = session.Query<T>();

			if (query != null)
				queryable = queryable.Where(query);

			queryable = queryable.Cacheable();

			return queryable.Select(x => x).SingleOrDefault();
		}

		#endregion

		#region List operations

		/// <summary>
		/// Get a list of sorted objects
		/// </summary>
		/// <typeparam name="T">The type of elements</typeparam>
		/// <typeparam name="TOrder">Order comparing values type</typeparam>
		/// <param name="session">The NHibernate session.</param>
		/// <param name="orderExpression">Ordering expression</param>
		/// <param name="orderDescending">Descending sorting</param>
		/// <returns>List of objects</returns>
		public static IList<T> GetSortedList<T, TOrder>(this ISession session, Expression<Func<T, TOrder>> orderExpression = null, bool orderDescending = false)
		{
			var queryable = session.Query<T>();

			if (orderExpression != null)
				queryable = orderDescending ? queryable.OrderByDescending(orderExpression) : queryable.OrderBy(orderExpression);

			return queryable.Select(x => x).ToList();
		}

		/// <summary>
		/// Get a list of objects
		/// </summary>
		/// <typeparam name="T">The type of elements</typeparam>
		/// <param name="session">The NHibernate session.</param>
		/// <param name="query">Query</param>
		/// <returns>List of objects</returns>
		public static IList<T> GetList<T>(this ISession session, Expression<Func<T, bool>> query = null)
			where T : class
		{
			var queryable = session.Query<T>();

			if (query != null)
				queryable = queryable.Where(query);

			return queryable.Select(x => x).ToList();
		}

		/// <summary>
		/// Get a list of objects
		/// </summary>
		/// <typeparam name="T">The type of elements</typeparam>
		/// <typeparam name="TOrder">Order comparing values type</typeparam>
		/// <param name="session">The NHibernate session.</param>
		/// <param name="query">Query</param>
		/// <param name="orderExpression">Filtering expression</param>
		/// <param name="orderDescending">Descending sorting</param>
		/// <returns>List of objects</returns>
		public static IList<T> GetList<T, TOrder>(this ISession session, Expression<Func<T, bool>> query = null, Expression<Func<T, TOrder>> orderExpression = null, bool orderDescending = false)
			where T : class
		{
			var queryable = session.Query<T>();

			if (query != null)
				queryable = queryable.Where(query);

			if (orderExpression != null)
				queryable = orderDescending ? queryable.OrderByDescending(orderExpression) : queryable.OrderBy(orderExpression);

			return queryable.Select(x => x).ToList();
		}

		/// <summary>
		/// Get sorted list of objects
		/// </summary>
		/// <typeparam name="T">The type of elements</typeparam>
		/// <typeparam name="TOrder">Order comparing value type</typeparam>
		/// <param name="session">The NHibernate session.</param>
		/// <param name="orderExpression">Ordering function</param>
		/// <param name="orderDescending">Descending sorting</param>
		/// <returns>Sorted list of objects</returns>
		public static IList<T> GetListSorted<T, TOrder>(this ISession session, Expression<Func<T, TOrder>> orderExpression = null, bool orderDescending = false)
			where T : class
		{
			var queryable = session.Query<T>();

			if (orderExpression != null)
				queryable = orderDescending ? queryable.OrderByDescending(orderExpression) : queryable.OrderBy(orderExpression);

			return queryable.Select(x => x).ToList();
		}

		#endregion
	}
}
