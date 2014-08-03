using System.Collections;
using System.Configuration;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;
using System.Xml.Linq;
using System.Xml.XPath;
using Simplify.Core;

namespace Simplify.WindowsServices
{
	/// <summary>
	/// Windows-services installer base class
	/// </summary>
	public class ServiceInstallerBase : Installer
	{
		private readonly ServiceProcessInstaller _serviceProcessInstaller = new ServiceProcessInstaller();
		private readonly ServiceInstaller _serviceInstaller = new ServiceInstaller();

		private readonly string _userName;
		private readonly string _password;

		/// <summary>
		/// ProcessingServiceInstaller initialization with information from service assembly
		/// </summary>
		/// <param name="serviceAssembly">Assembly from which to get assembly information</param>
		public ServiceInstallerBase(Assembly serviceAssembly = null)
		{
			var assemblyInfo = serviceAssembly != null ? new AssemblyInfo(serviceAssembly) : AssemblyInfo.Entry;

			Initialize(assemblyInfo.Description, assemblyInfo.Description, assemblyInfo.Title, ServiceAccount.LocalService, null, null);
		}

		/// <summary>
		/// ProcessingServiceInstaller initialization with information from service assembly and RunAs user name, password from config file
		/// </summary>
		/// <param name="serviceAssembly"></param>
		/// <param name="installRunAsUserFromConfig"></param>
		public ServiceInstallerBase(Assembly serviceAssembly, bool installRunAsUserFromConfig)
		{
			if(serviceAssembly == null)
				throw new ServiceInitializationException("Installation failed, serviceAssembly is null");

			IAssemblyInfo assemblyInfo = new AssemblyInfo(serviceAssembly);

			if (installRunAsUserFromConfig)
			{
				var config = ConfigurationManager.OpenExeConfiguration(serviceAssembly.Location);
				var configSection = config.GetSection("ServiceInstallerSettings");

				if (configSection != null)
				{
					var configSectionElement = XElement.Parse(configSection.SectionInformation.GetRawXml());

					foreach (var item in configSectionElement.XPathSelectElements("add"))
					{
						if (item.Attribute(XName.Get("key")) == null)
							continue;

						if (item.Attribute(XName.Get("key")).Value == "RunAsUserName")
							_userName = item.Attribute(XName.Get("value")).Value;

						if (item.Attribute(XName.Get("key")).Value == "RunAsUserPassword")
							_password = item.Attribute(XName.Get("value")).Value;
					}
				}
			}

			Initialize(assemblyInfo.Description, assemblyInfo.Description, assemblyInfo.Title, ServiceAccount.User, _userName, _password);
		}

		/// <summary>
		/// ProcessingServiceInstaller initialization
		/// </summary>
		/// <param name="description">Service description</param>
		/// <param name="displayName">Service display name</param>
		/// <param name="serviceName">Service name</param>
		public ServiceInstallerBase(string description, string displayName, string serviceName)
		{
			Initialize(description, displayName, serviceName, ServiceAccount.LocalService, null, null);
		}

		/// <summary>
		/// ProcessingServiceInstaller initialization with information from service assembly
		/// </summary>
		/// <param name="serviceAssembly">Assembly from which to get assembly information</param>
		/// <param name="account">Acount type under which to run this service</param>
		/// <param name="userName">User name under which to run this service</param>
		/// <param name="password">Password under which to run this service</param>
		public ServiceInstallerBase(ServiceAccount account, string userName, string password, Assembly serviceAssembly = null)
		{
			var assemblyInfo = serviceAssembly != null ? new AssemblyInfo(serviceAssembly) : AssemblyInfo.Entry;

			Initialize(assemblyInfo.Description, assemblyInfo.Description, assemblyInfo.Title, account, userName,
					   password);
		}

		/// <summary>
		/// ProcessingServiceInstaller initialization
		/// </summary>
		/// <param name="description">Service description</param>
		/// <param name="displayName">Service display name</param>
		/// <param name="serviceName">Service name</param>
		/// <param name="account">Acount type under which to run this service</param>
		/// <param name="userName">User name under which to run this service</param>
		/// <param name="password">Password under which to run this service</param>
		public ServiceInstallerBase(string description, string displayName, string serviceName, ServiceAccount account, string userName, string password)
		{
			Initialize(description, displayName, serviceName, account, userName, password);
		}

		private void Initialize(string description, string displayName, string serviceName, ServiceAccount account,
		                        string userName, string password)
		{
			_serviceInstaller.Description = description;
			_serviceInstaller.DisplayName = displayName;
			_serviceInstaller.ServiceName = serviceName;

			_serviceProcessInstaller.Account = account;
			_serviceProcessInstaller.Password = userName;
			_serviceProcessInstaller.Username = password;

			Installers.AddRange(new Installer[]
				{
					_serviceProcessInstaller,
					_serviceInstaller
				});
		}

		/// <summary>
		/// Perform the installation of service (used by system)
		/// </summary>
		/// <param name="stateSaver"></param>
		public override void Install(IDictionary stateSaver)
		{
			if (!string.IsNullOrEmpty(_userName) && !string.IsNullOrEmpty(_password))
			{
				Context.Parameters["USERNAME"] = _userName;
				Context.Parameters["PASSWORD"] = _password;
			}

			base.Install(stateSaver);
		}
	}
}
