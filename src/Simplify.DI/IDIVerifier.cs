namespace Simplify.DI
{
	/// <summary>
	/// Represents DI container verifier
	/// </summary>
	public interface IDIVerifier
	{
		/// <summary>
		/// Performs container objects graph verification
		/// </summary>
		void Verify();
	}
}