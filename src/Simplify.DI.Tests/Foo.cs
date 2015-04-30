namespace Simplify.DI.Tests
{
	public class Foo
	{
		public Foo(Bar1 bar1, Bar2 bar2)
		{
			Bar1 = bar1;
			Bar2 = bar2;
		}

		public Bar1 Bar1 { get; private set; }
		public Bar2 Bar2 { get; private set; }
	}
}