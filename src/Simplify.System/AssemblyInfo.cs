using System;
using System.Reflection;

namespace Simplify.System
{
	/// <summary>
	/// Provides the assembly information
	/// </summary>
	public class AssemblyInfo : IAssemblyInfo
	{
		private static IAssemblyInfo _entryAssemblyInfo;
		private readonly Assembly _infoAssembly;

		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyInfo"/> class.
		/// </summary>
		/// <param name="infoAssembly">The information assembly.</param>
		/// <exception cref="ArgumentNullException">infoAssembly</exception>
		public AssemblyInfo(Assembly infoAssembly)
		{
			_infoAssembly = infoAssembly ?? throw new ArgumentNullException("infoAssembly");
		}

		/// <summary>
		/// Gets or sets the entry assembly information.
		/// </summary>
		/// <exception cref="ArgumentNullException">value</exception>
		public static IAssemblyInfo Entry
		{
			get => _entryAssemblyInfo ?? (_entryAssemblyInfo = new AssemblyInfo(Assembly.GetEntryAssembly()));
			set => _entryAssemblyInfo = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Gets the assembly version.
		/// </summary>
		/// <value>
		/// The version.
		/// </value>
		public Version Version => _infoAssembly.GetName().Version;

		/// <summary>
		/// Gets the assembly title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title
		{
			get
			{
				var attributes = _infoAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

				if (attributes.Length > 0)
				{
					var titleAttribute = (AssemblyTitleAttribute)attributes[0];
					if (titleAttribute.Title.Length > 0) return titleAttribute.Title;
				}

				return global::System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		/// <summary>
		/// Gets the product name of the assembly.
		/// </summary>
		/// <value>
		/// The name of the product.
		/// </value>
		public string ProductName
		{
			get
			{
				var attributes = _infoAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				return attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}

		/// <summary>
		/// Gets the description of the assembly.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description
		{
			get
			{
				var attributes = _infoAssembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		/// <summary>
		/// Gets the copyright information of the assembly.
		/// </summary>
		/// <value>
		/// The copyright holder.
		/// </value>
		public string Copyright
		{
			get
			{
				var attributes = _infoAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		/// <summary>
		/// Gets the company name of the assembly.
		/// </summary>
		/// <value>
		/// The name of the company.
		/// </value>
		public string CompanyName
		{
			get
			{
				var attributes = _infoAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
	}
}