using DryIoc;
using NUnit.Framework;
using Simplify.DI.Provider.DryIoc;
using Simplify.DI.TestsTypes.New;

namespace Simplify.DI.Tests
{
	[TestFixture]
	public class NewDIContainerProviderTests
	{
		private IDIContainerProvider _provider;

		[SetUp]
		public void Initialize()
		{
			_provider = new DryIocDIProvider();
		}

		#region Non-dependant

		[Test]
		public void Resolve_NonDependencyType_NoRegistration_ContainerException()
		{
			// Act
			using (var scope = _provider.BeginLifetimeScope())
				Assert.Throws<ContainerException>(() => scope.Resolver.Resolve<NonDepFoo>());
		}

		[Test]
		public void Resolve_NonDependencyType_Registered_Resolved()
		{
			// Assign

			_provider.Register<NonDepFoo>();

			NonDepFoo foo;

			// Act
			using (var scope = _provider.BeginLifetimeScope())
				foo = scope.Resolver.Resolve<NonDepFoo>();

			// Assert
			Assert.IsNotNull(foo);
		}

		[Test]
		public void Resolve_InterfaceWithImplementationType_Resolved()
		{
			// Assign

			_provider.Register<IBar, Bar>();

			IBar bar;

			// Act
			using (var scope = _provider.BeginLifetimeScope())
				bar = scope.Resolver.Resolve<IBar>();

			// Assert
			Assert.IsNotNull(bar);
		}

		#endregion Non-dependant

		#region Single dependency

		[Test]
		public void Resolve_SingleDependencyScoped_Resolved_Reused()
		{
			// Assign

			_provider.Register<IBar, Bar>();
			_provider.Register<IFoo, Foo>();

			IFoo foo;

			// Act
			using (var scope = _provider.BeginLifetimeScope())
				foo = scope.Resolver.Resolve<IFoo>();

			// Assert

			Assert.IsNotNull(foo);
			Assert.IsNotNull(foo.Bar);
		}

		#endregion Single dependency
	}
}