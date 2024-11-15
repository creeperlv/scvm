using LibCLCC.NET.TextProcessing;
using scvm.tools.compiler.core.utilities;

namespace scvm.tools.compiler.core.Errors
{
	public class UnimplementedInstructionError : CompilerError
	{
		public string InstructionName;

		public UnimplementedInstructionError(string instructionName, Segment segment,SourcePosition position) : base(segment, position)
		{
			InstructionName = instructionName;
		}

		public override string ToString()
		{
			return $"Unimplemented Instruction: {InstructionName}. At:"+ErrorSegment.GetFullPosition(SourcePosition);
		}
	}
}
