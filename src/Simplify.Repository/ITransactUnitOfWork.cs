using System;
using System.Data;

namespace Simplify.Repository
{
	/// <summary>
	/// Represent unit of work with auto-open transaction
	/// </summary>
	public interface ITransactUnitOfWork : IUnitOfWork
	{
		/// <summary>
		/// Commits transaction.
		/// </summary>
		void Commit();

		/// <summary>
		/// Rollbacks transaction.
		/// </summary>
		void Rollback();
	}
}