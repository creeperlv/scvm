
using scvm.tools.core;

namespace DirectVM.linker.shell;

class Program
{
	static void Main(string[] args)
	{
		Arguments arguments = new Arguments();
		arguments.Definitions.Add(new ArgumentDefinition("input", ["-i", "--input"]) { HasValues = true });
		arguments.Definitions.Add(new ArgumentDefinition("output", ["-o", "--output"]) { HasValues = true });
		arguments.Resolve(args);
	}
}
