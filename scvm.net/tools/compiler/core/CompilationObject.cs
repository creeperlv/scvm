using scvm.core;
using System;
using System.Collections.Generic;

namespace scvm.tools.compiler.core
{
	[Serializable]
	public class CompilationObject
	{
		public List<string> sourceFiles = new List<string>();
		public List<PartialInstruction> instructions = new List<PartialInstruction>();
	}
	[Serializable]
	public class PartialInstruction
	{
		public string? Label;
		public SourcePosition sourcePosition;
		public Instruction Instruction;
	}
	public struct SourcePosition
	{
		public int FileID;
		public int Line;
	}
}
