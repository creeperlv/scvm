using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace scvm.tools.compiler.core.Errors
{
	public class CompilerError : Error
	{
		public Segment ErrorSegment;

		public CompilerError(Segment errorSegment)
		{
			ErrorSegment = errorSegment;
		}
	}
	public class TypeMismatchError : CompilerError
	{
		private string targetType;
		public TypeMismatchError(Segment errorSegment, string TargetType) : base(errorSegment) => targetType = TargetType;
		public override string ToString()
		{
			return $"Expect {targetType}!";
		}
	}
	public class UnknownBaseTypeError : CompilerError
	{
		public UnknownBaseTypeError(Segment errorSegment) : base(errorSegment)
		{

		}
		public override string ToString()
		{
			return "Unknown Base Type!";
		}
	}
	public class IncompletInstructionError : CompilerError
	{
		public IncompletInstructionError(Segment errorSegment) : base(errorSegment)
		{
		}

		public override string ToString()
		{
			return "Incomplete Instruction!";
		}
	}
}
