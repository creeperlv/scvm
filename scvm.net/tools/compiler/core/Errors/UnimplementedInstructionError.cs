using LibCLCC.NET.TextProcessing;

namespace scvm.tools.compiler.core.Errors
{
	public class UnimplementedInstructionError : CompilerError
	{
		public string InstructionName;

		public UnimplementedInstructionError(string instructionName, Segment segment) : base(segment)
		{
			InstructionName = instructionName;
		}

		public override string ToString()
		{
			return $"Unimplemented Instruction: {InstructionName}";
		}
	}
}
