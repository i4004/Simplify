using System.Data;

namespace Simplify.Repository
{
	/// <summary>
	/// Represent unit of work with manual open transaction
	/// </summary>
	public interface IBeginTransactUnitOfWork : ITransactUnitOfWork
	{
		/// <summary>
		/// Begins the transaction.
		/// </summary>
		/// <param name="isolationLevel">The isolation level.</param>
		void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
	}
}