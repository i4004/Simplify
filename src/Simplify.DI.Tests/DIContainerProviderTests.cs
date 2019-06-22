using DryIoc;
using NUnit.Framework;
using Simplify.DI.Provider.DryIoc;
using Simplify.DI.TestsTypes;

namespace Simplify.DI.Tests
{
	[TestFixture]
	public class DIContainerProviderTests
	{
		private IDIContainerProvider _provider;

		[SetUp]
		public void Initialize()
		{
			_provider = new DryIocDIProvider();
		}

		[Test]
		public void Resolve_InterfaceWithImplementationType_Resolved()
		{
			// Assign

			_provider.Register<IBar1, Bar1>();

			IBar1 bar;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
				bar = scope.Resolver.Resolve<IBar1>();

			// Assert

			Assert.IsNotNull(bar);
		}

		[Test]
		public void Resolve_AllSingletones_EqualObjects()
		{
			// Assign

			_provider.Register<Foo>(LifetimeType.Singleton);
			_provider.Register<Bar1>(LifetimeType.Singleton);
			_provider.Register<Bar2>(LifetimeType.Singleton);

			// Act

			var foo = _provider.Resolve<Foo>();
			var foo2 = _provider.Resolve<Foo>();
			Foo foo3;
			Foo foo4;

			using (var scope = _provider.BeginLifetimeScope())
				foo3 = scope.Resolver.Resolve<Foo>();

			using (var scope = _provider.BeginLifetimeScope())
				foo4 = scope.Resolver.Resolve<Foo>();

			// Assert

			Assert.AreEqual(foo, foo2);
			Assert.AreEqual(foo, foo3);
			Assert.AreEqual(foo, foo4);
		}

		[Test]
		public void Resolve_AllTransients_NotEqualObjects()
		{
			// Assign

			_provider.Register<Foo>(LifetimeType.Transient);
			_provider.Register<Bar1>(LifetimeType.Transient);
			_provider.Register<Bar2>(LifetimeType.Transient);

			// Act

			var foo = _provider.Resolve<Foo>();
			var foo2 = _provider.Resolve<Foo>();
			Foo foo3;
			Foo foo4;

			using (var scope = _provider.BeginLifetimeScope())
				foo3 = scope.Resolver.Resolve<Foo>();

			using (var scope = _provider.BeginLifetimeScope())
				foo4 = scope.Resolver.Resolve<Foo>();

			// Assert

			Assert.AreNotEqual(foo, foo2);
			Assert.AreNotEqual(foo, foo3);
			Assert.AreNotEqual(foo, foo4);

			Assert.AreNotEqual(foo.Bar1, foo2.Bar1);
			Assert.AreNotEqual(foo.Bar2, foo2.Bar2);
			Assert.AreNotEqual(foo.Bar1, foo3.Bar1);
			Assert.AreNotEqual(foo.Bar2, foo3.Bar2);
			Assert.AreNotEqual(foo3.Bar1, foo4.Bar1);
			Assert.AreNotEqual(foo3.Bar2, foo4.Bar2);
		}

		[Test]
		public void Resolve_SingletonInsideTransientAndScoped_SingletonEqualsAndTransientNotEquals()
		{
			// Assign

			_provider.Register<Foo>(LifetimeType.Transient);
			_provider.Register<Bar1>(LifetimeType.Singleton);
			_provider.Register<Bar2>(LifetimeType.Transient);

			// Act

			var foo = _provider.Resolve<Foo>();
			var foo2 = _provider.Resolve<Foo>();
			Foo foo3;
			Foo foo4;

			using (var scope = _provider.BeginLifetimeScope())
				foo3 = scope.Resolver.Resolve<Foo>();

			using (var scope = _provider.BeginLifetimeScope())
				foo4 = scope.Resolver.Resolve<Foo>();

			// Assert

			Assert.AreNotEqual(foo, foo2);
			Assert.AreNotEqual(foo, foo3);
			Assert.AreNotEqual(foo, foo4);

			Assert.AreEqual(foo.Bar1, foo2.Bar1);
			Assert.AreNotEqual(foo.Bar2, foo2.Bar2);
			Assert.AreEqual(foo.Bar1, foo3.Bar1);
			Assert.AreNotEqual(foo.Bar1, foo3.Bar2);
			Assert.AreEqual(foo3.Bar1, foo4.Bar1);
			Assert.AreNotEqual(foo3.Bar2, foo4.Bar2);
		}

		[Test]
		public void Resolve_PerLifetimeScope_EqualInsideScope()
		{
			// Assign

			_provider.Register<Foo>();
			_provider.Register<Bar1>(LifetimeType.Singleton);
			_provider.Register<Bar2>(LifetimeType.Transient);

			// Act

			Foo foo3;
			Foo foo4;
			Foo foo5;

			using (var scope = _provider.BeginLifetimeScope())
				foo3 = scope.Resolver.Resolve<Foo>();

			using (var scope = _provider.BeginLifetimeScope())
			{
				foo4 = scope.Resolver.Resolve<Foo>();
				foo5 = scope.Resolver.Resolve<Foo>();
			}

			// Assert

			Assert.AreNotEqual(foo3, foo4);
			Assert.AreEqual(foo4, foo5);

			Assert.AreEqual(foo3.Bar1, foo4.Bar1);
			Assert.AreEqual(foo4.Bar1, foo5.Bar1);
			Assert.AreNotEqual(foo3.Bar2, foo4.Bar2);
		}

		[Test]
		public void Resolve_DependencyInPerLifetimeScope_EqualInsideScope()
		{
			// Assign

			_provider.Register<Foo>(LifetimeType.Transient);
			_provider.Register<Bar1>();
			_provider.Register<Bar2>(LifetimeType.Transient);

			// Act

			Foo foo3;
			Foo foo4;
			Foo foo5;

			using (var scope = _provider.BeginLifetimeScope())
				foo3 = scope.Resolver.Resolve<Foo>();

			using (var scope = _provider.BeginLifetimeScope())
			{
				foo4 = scope.Resolver.Resolve<Foo>();
				foo5 = scope.Resolver.Resolve<Foo>();
			}

			// Assert

			Assert.AreNotEqual(foo3, foo4);
			Assert.AreNotEqual(foo3, foo5);
			Assert.AreNotEqual(foo4, foo5);

			Assert.AreNotEqual(foo3.Bar1, foo4.Bar1);
			Assert.AreEqual(foo4.Bar1, foo5.Bar1);
		}

		[Test]
		public void Resolve_DelegateWithResolve_ScopeVersionReturned()
		{
			// Assign

			_provider.Register<Bar1>();
			_provider.Register<Bar2>();
			_provider.Register(p => new Foo(p.Resolve<Bar1>(), p.Resolve<Bar2>()));

			Foo foo1;
			Foo foo2;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
				foo1 = scope.Resolver.Resolve<Foo>();

			using (var scope = _provider.BeginLifetimeScope())
				foo2 = scope.Resolver.Resolve<Foo>();

			// Assert

			Assert.AreNotEqual(foo1, foo2);
			Assert.AreNotEqual(foo1.Bar1, foo2.Bar1);
		}

		[Test]
		public void Verify_CorrectGraph_NoException()
		{
			// Assign

			_provider.Register<Bar1>();
			_provider.Register<Bar2>();
			_provider.Register<Foo>();

			// Act && Assert
			Assert.DoesNotThrow(() => _provider.Verify());
		}

		[Test]
		public void Verify_LifetimeScopeMismatch_ExceptionThrown()
		{
			// Assign

			_provider.Register<Bar1>(LifetimeType.Transient);
			_provider.Register<Bar2>();
			_provider.Register<Foo>(LifetimeType.Singleton);

			// Act && Assert
			Assert.Throws<ContainerException>(() => _provider.Verify());
		}

		[Test]
		public void Verify_MissingRegistrations_ExceptionThrown()
		{
			// Assign
			_provider.Register<Foo>();

			// Act && Assert
			Assert.Throws<ContainerException>(() => _provider.Verify());
		}
	}
}