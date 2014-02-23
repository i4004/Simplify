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
		#region Native objects operations

		/// <summary>
		/// Get object from single item table
		/// </summary>
		/// <typeparam name="T">Object type to get</typeparam>
		/// <returns></returns>
		public static T GetSingleObject<T>(this ISession session)
			where T : class
		{
			return session.CreateCriteria<T>()
				.UniqueResult<T>();
		}

		/// <summary>
		/// Get object from database by filter (in case of several objects returned exception will be thrown)
		/// </summary>
		/// <typeparam name="T">The type of object</typeparam>
		/// <param name="session">The nhibernate session.</param>
		/// <param name="filterFunc">Filtering function</param>
		/// <returns></returns>
		public static T GetObject<T>(this ISession session, Expression<Func<T, bool>> filterFunc)
			where T : class
		{
			var queryable = session.Query<T>();

			if (filterFunc != null)
				queryable = queryable.Where(filterFunc);

			return queryable.Select(x => x).SingleOrDefault();
		}

		/// <summary>
		/// Get first object from database by filter
		/// </summary>
		/// <typeparam name="T">The type of object</typeparam>
		/// <param name="session">The nhibernate session.</param>
		/// <param name="filterFunc">Filtering function</param>
		/// <returns></returns>
		public static T GetFirstObject<T>(this ISession session, Expression<Func<T, bool>> filterFunc)
			where T : class
		{
			var queryable = session.Query<T>();

			if (filterFunc != null)
				queryable = queryable.Where(filterFunc);

			return queryable.Select(x => x).FirstOrDefault();
		}

		/// <summary>
		/// Get and cache object from database by filter (in case of several objects returned exception will be thrown)
		/// </summary>
		/// <typeparam name="T">The type of object</typeparam>
		/// <param name="session">The nhibernate session.</param>
		/// <param name="filterFunc">Filtering function</param>
		/// <returns></returns>
		public static T GetObjectCacheable<T>(this ISession session, Expression<Func<T, bool>> filterFunc)
			where T : class
		{
			var queryable = session.Query<T>();

			if (filterFunc != null)
				queryable = queryable.Where(filterFunc);

			queryable = queryable.Cacheable();

			return queryable.Select(x => x).SingleOrDefault();
		}

		#endregion

		#region List operations

		/// <summary>
		/// Get list of sorted objects
		/// </summary>
		/// <typeparam name="T">The type of elements</typeparam>
		/// <typeparam name="TOrder">Order comparing value type</typeparam>
		/// <param name="session">The nhibernate session.</param>
		/// <param name="orderFunc">Ordering function</param>
		/// <param name="orderDescending">Descending sorting</param>
		/// <returns>List of objects</returns>
		public static IList<T> GetSortedList<T, TOrder>(this ISession session, Expression<Func<T, TOrder>> orderFunc, bool orderDescending = false)
		{
			var queryable = session.Query<T>();

			if (orderFunc != null)
				queryable = orderDescending ? queryable.OrderByDescending(orderFunc) : queryable.OrderBy(orderFunc);

			return queryable.Select(x => x).ToList();
		}

		/// <summary>
		/// Get list of objects
		/// </summary>
		/// <typeparam name="T">The type of elements</typeparam>
		/// <param name="session">The nhibernate session.</param>
		/// <param name="filterFunc">Filtering function</param>
		/// <returns>List of objects</returns>
		public static IList<T> GetList<T>(this ISession session, Expression<Func<T, bool>> filterFunc = null)
			where T : class
		{
			var queryable = session.Query<T>();

			if (filterFunc != null)
				queryable = queryable.Where(filterFunc);

			return queryable.Select(x => x).ToList();
		}

		/// <summary>
		/// Get list of objects
		/// </summary>
		/// <typeparam name="T">The type of elements</typeparam>
		/// <typeparam name="TOrder">Order comparing value type</typeparam>
		/// <param name="session">The nhibernate session.</param>
		/// <param name="filterFunc">Filtering function</param>
		/// <param name="orderFunc">Ordering function</param>
		/// <param name="orderDescending">Descending sorting</param>
		/// <returns>List of objects</returns>
		public static IList<T> GetList<T, TOrder>(this ISession session, Expression<Func<T, bool>> filterFunc, Expression<Func<T, TOrder>> orderFunc, bool orderDescending = false)
			where T : class
		{
			var queryable = session.Query<T>();

			if (filterFunc != null)
				queryable = queryable.Where(filterFunc);

			if (orderFunc != null)
				queryable = orderDescending ? queryable.OrderByDescending(orderFunc) : queryable.OrderBy(orderFunc);

			return queryable.Select(x => x).ToList();
		}

		/// <summary>
		/// Get sorted list of objects
		/// </summary>
		/// <typeparam name="T">The type of elements</typeparam>
		/// <typeparam name="TOrder">Order comparing value type</typeparam>
		/// <param name="session">The nhibernate session.</param>
		/// <param name="orderFunc">Ordering function</param>
		/// <param name="orderDescending">Descending sorting</param>
		/// <returns>Sorted list of objects</returns>
		public static IList<T> GetListSorted<T, TOrder>(this ISession session, Expression<Func<T, TOrder>> orderFunc, bool orderDescending = false)
			where T : class
		{
			var queryable = session.Query<T>();

			if (orderFunc != null)
				queryable = orderDescending ? queryable.OrderByDescending(orderFunc) : queryable.OrderBy(orderFunc);

			return queryable.Select(x => x).ToList();
		}

		#endregion
	}
}