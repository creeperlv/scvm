using scvm.core;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace scvm.tools.compiler.core
{
	public class Constants
	{
		public readonly static Version CompilationObjectVersion = new Version(1, 0, 0, 0);
	}
	[Serializable]
	public class CompilationObject
	{
		public Version ObjectFormatVersion = Constants.CompilationObjectVersion;
		public List<string> sourceFiles = new List<string>();
		public List<IntermediateInstruction> instructions = new List<IntermediateInstruction>();
		public Dictionary<string, Label> Labels = new Dictionary<string, Label>();
		public Dictionary<string, byte[]> Data = new Dictionary<string, byte[]>();
		public Dictionary<string, string> Definitions = new Dictionary<string, string>();
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
		public List<UnsolvedSymbol> UnsolvedSymbols = new List<UnsolvedSymbol>();

		public SourcePosition sourcePosition;
		public Instruction Instruction;
	}
	[Serializable]
	public class UnsolvedSymbol
	{
		public string? Symbol;
		public int TargetArumentPos;

		public UnsolvedSymbol()
		{

		}
		public UnsolvedSymbol(string symbol, int targetArumentPos)
		{
			Symbol = symbol;
			TargetArumentPos = targetArumentPos;
		}
	}
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct SourcePosition
	{
		public int FileID;
		public int Line;
	}
}
