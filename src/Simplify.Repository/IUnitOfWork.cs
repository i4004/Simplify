using System;

namespace Simplify.Repository
{
	/// <summary>
	/// Represent unit of work without explicit transaction
	/// </summary>
	public interface IUnitOfWork : IDisposable, IHideObjectMembers
	{
	}
}