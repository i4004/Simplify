namespace Simplify.DI.TestsTypes
{
	public class Foo : IFoo
	{
		public Foo(IBar bar)
		{
			Bar = bar;
		}

		public IBar Bar { get; }
	}
}