using System.Collections;

namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides objects comparer based on IIdentityObject
	/// </summary>
	/// <seealso cref="IEqualityComparer" />
	public class CommonEqualityComparer : IEqualityComparer
	{
		/// <summary>
		/// Determines whether the specified <see cref="object" />, is equal to this instance.
		/// </summary>
		/// <param name="x">The <see cref="object" /> to compare with this instance.</param>
		/// <param name="y">The y.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public new virtual bool Equals(object x, object y)
		{
			if (x == null || y == null)
				return false;

			if (x is IIdentityObject identityObjectA && y is IIdentityObject identityObjectB)
				return identityObjectA.ID == identityObjectB.ID;

			return x.Equals(y);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
		/// </returns>
		public int GetHashCode(object obj)
		{
			return obj.GetHashCode();
		}
	}
}