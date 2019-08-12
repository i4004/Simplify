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

		#region Type without dependency

		[Test]
		public void Resolve_TypeWithoutDependencyAndNotRegistered_ContainerException()
		{
			// Act & Assert
			Assert.Throws<ContainerException>(() => _provider.Resolve<NonDepFoo>());
		}

		[Test]
		public void ScopedResolve_TypeWithoutDependencyAndNotRegistered_ContainerException()
		{
			// Act & Assert
			using (var scope = _provider.BeginLifetimeScope())
				Assert.Throws<ContainerException>(() => scope.Resolver.Resolve<NonDepFoo>());
		}

		[Test]
		public void Resolve_TypeWithoutDependencyAndScopeRegistered_ContainerException()
		{
			// Assign
			_provider.Register<NonDepFoo>();

			// Act & Assert
			Assert.Throws<ContainerException>(() => _provider.Resolve<NonDepFoo>());
		}

		[Test]
		public void ScopedResolve_TypeWithoutDependencyAndScopeRegistered_Resolved()
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
		public void Resolve_TypeWithoutDependencyAndSingletonRegistered_Resolved()
		{
			// Assign
			_provider.Register<NonDepFoo>(LifetimeType.Singleton);

			// Act
			var foo = _provider.Resolve<NonDepFoo>();

			// Assert
			Assert.IsNotNull(foo);
		}

		[Test]
		public void ScopedResolve_TypeWithoutDependencyAndSingletonRegistered_Resolved()
		{
			// Assign

			_provider.Register<NonDepFoo>(LifetimeType.Singleton);

			NonDepFoo foo;

			// Act
			using (var scope = _provider.BeginLifetimeScope())
				foo = scope.Resolver.Resolve<NonDepFoo>();

			// Assert
			Assert.IsNotNull(foo);
		}

		[Test]
		public void Resolve_TypeWithoutDependencyAndTransientRegistered_Resolved()
		{
			// Assign
			_provider.Register<NonDepFoo>(LifetimeType.Transient);

			// Act
			var foo = _provider.Resolve<NonDepFoo>();

			// Assert
			Assert.IsNotNull(foo);
		}

		[Test]
		public void ScopedResolve_TypeWithoutDependencyAndTransientRegistered_Resolved()
		{
			// Assign

			_provider.Register<NonDepFoo>(LifetimeType.Transient);

			NonDepFoo foo;

			// Act
			using (var scope = _provider.BeginLifetimeScope())
				foo = scope.Resolver.Resolve<NonDepFoo>();

			// Assert
			Assert.IsNotNull(foo);
		}

		[Test]
		public void Resolve_InterfaceWithImplementationTypeAndTransient_Resolved()
		{
			// Assign
			_provider.Register<IBar, Bar>(LifetimeType.Transient);

			// Act
			var bar = _provider.Resolve<IBar>();

			// Assert
			Assert.IsNotNull(bar);
		}

		[Test]
		public void ScopedResolve_InterfaceWithImplementationTypeAndTransient_Resolved()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Transient);

			IBar bar;

			// Act
			using (var scope = _provider.BeginLifetimeScope())
				bar = scope.Resolver.Resolve<IBar>();

			// Assert
			Assert.IsNotNull(bar);
		}

		[Test]
		public void Resolve_InterfaceWithImplementationTypeAndTransientAndDelegateRegistration_Resolved()
		{
			// Assign
			_provider.Register<IBar>(r => new Bar(), LifetimeType.Transient);

			// Act
			var bar = _provider.Resolve<IBar>();

			// Assert
			Assert.IsNotNull(bar);
		}

		[Test]
		public void ScopedResolve_InterfaceWithImplementationTypeAndTransientAndDelegateRegistration_Resolved()
		{
			// Assign

			_provider.Register<IBar>(r => new Bar(), LifetimeType.Transient);

			IBar bar;

			// Act
			using (var scope = _provider.BeginLifetimeScope())
				bar = scope.Resolver.Resolve<IBar>();

			// Assert
			Assert.IsNotNull(bar);
		}

		#endregion Type without dependency

		#region Single dependency

		[Test]
		public void ScopedResolve_SingleDependencyAllScoped_ResolvedAndReused()
		{
			// Assign

			_provider.Register<IBar, Bar>();
			_provider.Register<IFoo, Foo>();

			IFoo foo;
			IFoo fooReused;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
			{
				foo = scope.Resolver.Resolve<IFoo>();
				fooReused = scope.Resolver.Resolve<IFoo>();
			}

			// Assert

			Assert.IsNotNull(foo);
			Assert.IsNotNull(foo.Bar);
			Assert.AreEqual(foo, fooReused);
			Assert.AreEqual(foo.Bar, fooReused.Bar);
		}

		#endregion Single dependency

		#region Verification

		[Test]
		public void Verify_MissingRegistration_ContainerException()
		{
			// Assign
			_provider.Register<Foo>();

			// Act && Assert
			Assert.Throws<ContainerException>(() => _provider.Verify());
		}

		[Test]
		public void Verify_SingletonDependsOnTransient_ContainerException()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Transient);
			_provider.Register<IFoo, Foo>(LifetimeType.Singleton);

			// Act && Assert
			Assert.Throws<ContainerException>(() => _provider.Verify());
		}

		[Test]
		public void Verify_ScopedDependsOnTransient_ContainerException()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Transient);
			_provider.Register<IFoo, Foo>();

			// Act && Assert
			Assert.Throws<ContainerException>(() => _provider.Verify());
		}

		[Test]
		public void Verify_TransientDependsOnSingleton_NoExceptions()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Singleton);
			_provider.Register<IFoo, Foo>(LifetimeType.Transient);

			// Act && Assert
			Assert.DoesNotThrow(() => _provider.Verify());
		}

		[Test]
		public void Verify_ScopedDependsOnSingleton_NoExceptions()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Singleton);
			_provider.Register<IFoo, Foo>();

			// Act && Assert
			Assert.DoesNotThrow(() => _provider.Verify());
		}

		[Test]
		public void Verify_ScopedDependsOnScoped_NoExceptions()
		{
			// Assign

			_provider.Register<IBar, Bar>();
			_provider.Register<IFoo, Foo>();

			// Act && Assert
			Assert.DoesNotThrow(() => _provider.Verify());
		}

		#endregion Verification
	}
}