using System;

namespace Simplify.Core
{
	/// <summary>
	/// Assembly information interface
	/// </summary>
	public interface IAssemblyInfo : IHideObjectMembers
	{
		/// <summary>
		/// Gets the assembly version.
		/// </summary>
		Version Version { get; }

		/// <summary>
		/// Gets the assembly title.
		/// </summary>
		string Title { get; }

		/// <summary>
		/// Gets the product name of the assembly.
		/// </summary>
		string ProductName { get; }

		/// <summary>
		/// Gets the description of the assembly.
		/// </summary>
		string Description { get; }

		/// <summary>
		/// Gets the copyright information of the assembly.
		/// </summary>
		string Copyright { get; }

		/// <summary>
		/// Gets the company name of the assembly.
		/// </summary>
		string CompanyName { get; }
	}
}