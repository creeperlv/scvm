using scvm.core;

namespace scvm.tools.compiler.shell;
public class Program
{
	public static void Main(string[] args)
	{
		unsafe
		{
			Console.WriteLine($"sizeof(Instruction)={sizeof(Instruction)}");
			Console.WriteLine($"sizeof(Instruction_OpSeparated)={sizeof(Instruction_OpSeparated)}");
			Console.WriteLine($"sizeof(Instruction_OpSeparated_ByteSegmented)={sizeof(Instruction_OpSeparated_ByteSegmented)}");
			Console.WriteLine($"Type.I={(byte)NativeType.I}");
		}
	}
}