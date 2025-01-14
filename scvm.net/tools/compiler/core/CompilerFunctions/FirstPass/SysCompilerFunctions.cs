using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using scvm.core;
using scvm.core.utilities;
using scvm.tools.compiler.core.Errors;
using scvm.tools.compiler.core.utilities;
using System;

namespace scvm.tools.compiler.core.CompilerFunctions.FirstPass
{
	public static class SysCompilerFunctions
	{
		public unsafe static bool Assemble_NOP(ISADefinition CurrentDefinition, ushort instID, Segment s, OperationResult<CompilationObject> result, IntermediateInstruction IInstruction, int PC)
		{
			if (instID != SCVMInst.NOP)
			{
				return false;
			}
			Instruction inst = default;
			IntPtr InstPtr = (IntPtr)(&inst);
			InstPtr.Set(instID, 0);
			return true;
		}
		public unsafe static bool Assemble_SYSCALL(ISADefinition CurrentDefinition, ushort instID, Segment s, OperationResult<CompilationObject> result, IntermediateInstruction IInstruction, int PC)
		{
			if (instID != SCVMInst.SYSCALL)
			{
				return false;
			}
			SegmentTraveler st = new SegmentTraveler(s);
			var sPos = IInstruction.sourcePosition;
			Instruction inst = default;
			IntPtr InstPtr = (IntPtr)(&inst);
			if (!st.GoNext())
			{
				result.AddError(new IncompleteInstructionError(st.Current, sPos));
				return false;
			}
			var ValueSeg = st.Current;

			InstPtr.Set(instID, 0);

			if (DataConversion.TryParseRegister(CurrentDefinition, ValueSeg.content, result, out var _T))
			{
				InstPtr.Set(1, 2);
				InstPtr.Set(_T, 3);
			}
			else
			{
				InstPtr.Set(0, 2);
				if (result.Result.Labels.TryGetValue(ValueSeg.content, out var lbl))
				{
					if (lbl.Position >= 0)
					{
						InstPtr.Set(lbl.Position - PC, 4);
						return true;
					}
				}
				else
				if (DataConversion.TryParseInt(CurrentDefinition, ValueSeg.content, result, out var V))
				{
					InstPtr.Set(0, 2);
					InstPtr.Set(V, 3);
				}
				else
				{
					IInstruction.UnsolvedSymbols.Add(new UnsolvedSymbol(ValueSeg.content, 0));
					IInstruction.IsIntermediate = true;
				}
			}
			return true;
		}
		public unsafe static bool Assemble_RF(ISADefinition CurrentDefinition, ushort instID, Segment s, OperationResult<CompilationObject> result, IntermediateInstruction IInstruction, int PC)
		{
			if (instID != SCVMInst.RF)
			{
				return false;
			}
			SegmentTraveler st = new SegmentTraveler(s);
			var sPos = IInstruction.sourcePosition;
			Instruction inst = default;
			IInstruction.Instruction=inst;
			IntPtr InstPtr = (IntPtr)(&inst);
			if (!st.GoNext())
			{
				result.AddError(new IncompleteInstructionError(st.Current, sPos));
				return false;
			}
			var ValueSeg = st.Current;

			InstPtr.Set(instID, 0);

			if (DataConversion.TryParseRegister(CurrentDefinition, ValueSeg.content, result, out var _T))
			{
				InstPtr.Set(1, 2);
				InstPtr.Set(_T, 3);
			}
			else
			{
				InstPtr.Set(0, 2);
				if (result.Result.Labels.TryGetValue(ValueSeg.content, out var lbl))
				{
					if (lbl.Position >= 0)
					{
						InstPtr.Set(lbl.Position - PC, 4);
						return true;
					}
				}
				else
				if (DataConversion.TryParseInt(CurrentDefinition, ValueSeg.content, result, out var V))
				{
					InstPtr.Set(0, 2);
					InstPtr.Set(V, 3);
				}
				else
				{
					IInstruction.UnsolvedSymbols.Add(new UnsolvedSymbol(ValueSeg.content, 0));
					IInstruction.IsIntermediate = true;
				}
			}
			return true;
		}

	}
}
