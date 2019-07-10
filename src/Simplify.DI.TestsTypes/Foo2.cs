namespace Simplify.DI.TestsTypes
{
	public class Foo2
	{
		public Foo2(IBar1 bar1)
		{
			Bar1 = bar1;
		}

		public IBar1 Bar1 { get; }
	}
}