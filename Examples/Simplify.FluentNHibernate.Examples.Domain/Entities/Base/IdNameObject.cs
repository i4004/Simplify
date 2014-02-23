namespace Simplify.FluentNHibernate.Examples.Domain.Entities.Base
{
	public class IdNameObject
	{
		public virtual int ID { get; set; }
		public virtual string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
