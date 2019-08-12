namespace Simplify.DI.TestsTypes.New
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