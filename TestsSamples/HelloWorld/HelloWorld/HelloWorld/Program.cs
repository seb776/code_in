using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public sealed class Program
{
	public static void Main ()
	{
		if (System.Diagnostics.Debugger.IsAttached)
			System.Diagnostics.Debugger.Break ();
		Console.WriteLine ("Toto");
	}
}
