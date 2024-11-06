using scvm.core;
using System;
using System.Collections.Generic;

namespace scvm.tools.compiler.core
{
	[Serializable]
	public class CompilationObject
	{
		public List<string> sourceFiles = new List<string>();
		public List<IntermediateInstruction> instructions = new List<IntermediateInstruction>();
		public Dictionary<string, Label> Labels = new Dictionary<string, Label>();
		public Dictionary<string, string> Data = new Dictionary<string, string>();
	}
	[Serializable]
	public class Label
	{
		public LabelVisibility visibility = LabelVisibility.Default;
		public string Content = "";
		public int Position;
	}
	[Serializable]
	public class IntermediateInstruction
	{
		public bool IsIntermediate;
		public List<string?> UnsolvedSymbols = new List<string?>();

		public SourcePosition sourcePosition;
		public Instruction Instruction;
	}
	public struct SourcePosition
	{
		public int FileID;
		public int Line;
	}
}
