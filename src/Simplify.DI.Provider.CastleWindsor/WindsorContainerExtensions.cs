using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Handlers;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;
using Castle.Windsor.Diagnostics.Helpers;

namespace Simplify.DI.Provider.CastleWindsor
{
	/// <summary>
	/// Provides IWindsorContainer extensions
	/// </summary>
	public static class WindsorContainerExtensions
	{
		/// <summary>
		/// Checks for potentially misconfigured components registered in container.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <exception cref="MisconfiguredComponentException"></exception>
		public static void CheckForPotentiallyMisconfiguredComponents(this IWindsorContainer container)
		{
			var host = (IDiagnosticsHost)container.Kernel.GetSubSystem(SubSystemConstants.DiagnosticsKey);

			CheckForPotentiallyMisconfiguredComponents(host);
			CheckForMisconfiguredLifetimesForComponents(host);
		}

		private static void CheckForPotentiallyMisconfiguredComponents(IDiagnosticsHost host)
		{
			var diagnostics = host.GetDiagnostic<IPotentiallyMisconfiguredComponentsDiagnostic>();

			var handlers = diagnostics.Inspect();

			if (!handlers.Any())
				return;

			var message = new StringBuilder();
			var inspector = new DependencyInspector(message);

			foreach (var handler in handlers)
				((IExposeDependencyInfo)handler).ObtainDependencyDetails(inspector);

			throw new MisconfiguredComponentException(message.ToString());
		}

		private static void CheckForMisconfiguredLifetimesForComponents(IDiagnosticsHost host)
		{
			var diagnostics = host.GetDiagnostic<IPotentialLifestyleMismatchesDiagnostic>();

			var handlers = diagnostics.Inspect();

			if (!handlers.Any())
				return;

			var messages = handlers.ConvertAll(GetMismatchMessage);

			if (messages.Length == 0)
				return;

			var message = string.Join(Environment.NewLine, messages);

			throw new MisconfiguredComponentException(message);
		}

		private static string GetMismatchMessage(IHandler[] handlers)
		{
			var message = new StringBuilder();

			Debug.Assert(handlers.Length > 1, "handlers.Length > 1");

			var root = handlers.First();
			var last = handlers.Last();

			message.AppendFormat("Component '{0}' with lifestyle {1} ", GetNameDescription(root.ComponentModel),
				root.ComponentModel.GetLifestyleDescription());

			message.AppendFormat("depends on '{0}' with lifestyle {1}", GetNameDescription(last.ComponentModel),
				last.ComponentModel.GetLifestyleDescription());

			for (var i = 1; i < handlers.Length - 1; i++)
			{
				var via = handlers[i];

				message.AppendLine();
				message.AppendFormat("\tvia '{0}' with lifestyle {1}", GetNameDescription(via.ComponentModel),
					via.ComponentModel.GetLifestyleDescription());
			}

			message.AppendLine();
			message.AppendFormat("This kind of dependency is usually not desired and may lead to various kinds of bugs.");

			return message.ToString();
		}

		private static string GetNameDescription(ComponentModel componentModel)
		{
			return componentModel.ComponentName.SetByUser ? componentModel.ComponentName.Name : componentModel.ToString();
		}

		private static TResult[] ConvertAll<T, TResult>(this T[] items, Func<T, TResult> converter)
		{
			var count = items.Length;
			var results = new TResult[count];

			for (var i = 0; i < count; i++)
			{
				results[i] = converter(items[i]);
			}

			return results;
		}
	}
}