using System.Diagnostics;

namespace Simplify.WindowsServices.MultitaskExample
{
	public class TaskProcessor1
	{
		public void Run()
		{
			Debug.WriteLine("TaskProcessor1 Run executed");
		}

		public void RunTask2()
		{
			Debug.WriteLine("TaskProcessor1 RunTask2 executed");
		}
	}
}