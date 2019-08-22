using Simplify.System;
using System.Collections;
using System.Configuration;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Simplify.Scheduler
{
	/// <summary>
	/// Windows-services installer base class
	/// </summary>
	public class ServiceInstallerBase : Installer
	{
		/// <summary>
		/// The service installer settings section name
		/// </summary>
		public const string ServiceInstallerSettingsSectionName = "ServiceInstallerSettings";

		/// <summary>
		/// The RunAs user name field name
		/// </summary>
		public const string RunAsUserNameFieldName = "RunAsUserName";

		/// <summary>
		/// The RunAs user password field name
		/// </summary>
		public const string RunAsUserPasswordFieldName = "RunAsUserPassword";

		/// <summary>
		/// The service account field name
		/// </summary>
		public const string ServiceAccountFieldName = "ServiceAccount";

		private readonly ServiceProcessInstaller _serviceProcessInstaller = new ServiceProcessInstaller();
		private readonly ServiceInstaller _serviceInstaller = new ServiceInstaller();

		private string _userName;
		private string _password;
		private ServiceAccount _serviceAccount = ServiceAccount.LocalService;

		/// <summary>
		/// ProcessingServiceInstaller initialization with custom service description assembly
		/// </summary>
		/// <param name="serviceDescriptionAssembly">Assembly from which to get service information</param>
		public ServiceInstallerBase(Assembly serviceDescriptionAssembly = null)
		{
			var assemblyInfo = serviceDescriptionAssembly != null ? new AssemblyInfo(serviceDescriptionAssembly) : AssemblyInfo.Entry;

			TryToLoadRunAsUserSettings(serviceDescriptionAssembly);

			Initialize(assemblyInfo.Description, assemblyInfo.Description, assemblyInfo.Title,
				_serviceAccount, _userName, _password);
		}

		/// <summary>
		/// ProcessingServiceInstaller initialization with custom service description
		/// </summary>
		/// <param name="description">Service description</param>
		/// <param name="displayName">Service display name</param>
		/// <param name="serviceName">Service name</param>
		public ServiceInstallerBase(string description, string displayName, string serviceName)
		{
			TryToLoadRunAsUserSettings();

			Initialize(description, displayName, serviceName, _serviceAccount, null, null);
		}

		/// <summary>
		/// ProcessingServiceInstaller initialization with custom run as user and service description from specified assembly
		/// </summary>
		/// <param name="serviceDescriptionAssembly">Assembly from which to get service information</param>
		/// <param name="account">Account type under which to run this service</param>
		/// <param name="userName">User name under which to run this service</param>
		/// <param name="password">Password under which to run this service</param>
		public ServiceInstallerBase(ServiceAccount account, string userName = null, string password = null, Assembly serviceDescriptionAssembly = null)
		{
			var assemblyInfo = serviceDescriptionAssembly != null ? new AssemblyInfo(serviceDescriptionAssembly) : AssemblyInfo.Entry;

			Initialize(assemblyInfo.Description, assemblyInfo.Description, assemblyInfo.Title, account, userName,
					   password);
		}

		/// <summary>
		/// ProcessingServiceInstaller initialization with all custom settings
		/// </summary>
		/// <param name="description">Service description</param>
		/// <param name="displayName">Service display name</param>
		/// <param name="serviceName">Service name</param>
		/// <param name="account">Account type under which to run this service</param>
		/// <param name="userName">User name under which to run this service</param>
		/// <param name="password">Password under which to run this service</param>
		public ServiceInstallerBase(string description, string displayName, string serviceName, ServiceAccount account, string userName, string password)
		{
			Initialize(description, displayName, serviceName, account, userName, password);
		}

		/// <summary>
		/// Perform the installation of service (used by system)
		/// </summary>
		/// <param name="stateSaver"></param>
		public override void Install(IDictionary stateSaver)
		{
			if (IsRunAsUserSet())
			{
				Context.Parameters["USERNAME"] = _userName;
				Context.Parameters["PASSWORD"] = _password;
			}

			base.Install(stateSaver);
		}

		private static ServiceAccount TryParseServiceAccountFieldData(string data)
		{
			switch (data)
			{
				case "NetworkService":
					return ServiceAccount.NetworkService;

				case "LocalSystem":
					return ServiceAccount.LocalSystem;

				case "User":
					return ServiceAccount.User;

				default:
					return ServiceAccount.LocalService;
			}
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

		private void TryToLoadRunAsUserSettings(Assembly serviceAssembly = null)
		{
			var config = ConfigurationManager.OpenExeConfiguration(serviceAssembly?.Location ?? Assembly.GetEntryAssembly().Location);

			var configSection = config.GetSection(ServiceInstallerSettingsSectionName);

			if (configSection == null)
				return;

			string serviceAccount = null;

			var configSectionElement = XElement.Parse(configSection.SectionInformation.GetRawXml());

			foreach (var item in configSectionElement.XPathSelectElements("add").Where(item => item.Attribute(XName.Get("key")) != null))
			{
				if (item.Attribute(XName.Get("key"))?.Value == RunAsUserNameFieldName)
					_userName = item.Attribute(XName.Get("value"))?.Value;

				if (item.Attribute(XName.Get("key"))?.Value == RunAsUserPasswordFieldName)
					_password = item.Attribute(XName.Get("value"))?.Value;

				if (item.Attribute(XName.Get("key"))?.Value == ServiceAccountFieldName)
					serviceAccount = item.Attribute(XName.Get("value"))?.Value;
			}

			_serviceAccount = !IsRunAsUserSet() ? TryParseServiceAccountFieldData(serviceAccount) : ServiceAccount.User;
		}

		private bool IsRunAsUserSet()
		{
			return !string.IsNullOrEmpty(_userName) && !string.IsNullOrEmpty(_password);
		}
	}
}