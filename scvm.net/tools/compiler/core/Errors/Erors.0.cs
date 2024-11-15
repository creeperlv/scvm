using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using scvm.tools.compiler.core.utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace scvm.tools.compiler.core.Errors
{
	public class CompilerError : Error
	{
		public Segment ErrorSegment;
		public SourcePosition SourcePosition;
		public CompilerError(Segment errorSegment,SourcePosition position)
		{
			ErrorSegment = errorSegment;
			SourcePosition = position;
		}
	}
	public class TypeMismatchError : CompilerError
	{
		private string targetType;
		public TypeMismatchError(Segment errorSegment,SourcePosition position, string TargetType) : base(errorSegment, position) => targetType = TargetType;
		public override string ToString()
		{
			return $"Expect {targetType}!At:" + ErrorSegment.GetFullPosition(SourcePosition);
		}
	}
	public class UnknownBaseTypeError : CompilerError
	{
		public UnknownBaseTypeError(Segment errorSegment, SourcePosition position) : base(errorSegment,position)
		{

		}
		public override string ToString()
		{
			return "Unknown Base Type! At:" + ErrorSegment.GetFullPosition(SourcePosition);
		}
	}
	public class IncompleteInstructionError : CompilerError
	{
		public IncompleteInstructionError(Segment errorSegment, SourcePosition position) : base(errorSegment, position)
		{
		}

		public override string ToString()
		{
			return "Incomplete Instruction!At:" + ErrorSegment.GetFullPosition(SourcePosition);
		}
	}
}
