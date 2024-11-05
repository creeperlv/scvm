using DirectVM.core.libc;

namespace DirectVM.shell;

class Program
{
	static void Main(string[] args)
	{
		StdLib.init();
		if (args.Length == 0) { }
		else
		{
			var filename = args[0];
		}
	}
}
