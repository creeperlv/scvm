using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using scvm.core;
using scvm.core.utilities;
using scvm.tools.compiler.core.Errors;
using scvm.tools.compiler.core.utilities;
using System;

namespace scvm.tools.compiler.core.CompilerFunctions.FirstPass
{
	public static class JumpCompilerFunctions
	{
		public unsafe static bool Assemble_JMP(ISADefinition CurrentDefinition, ushort instID, Segment s, OperationResult<CompilationObject> result, IntermediateInstruction IInstruction, int PC)
		{
			if (instID != SCVMInst.JMP)
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

			InstPtr.Set(SCVMInst.JMP, 0);

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
		public unsafe static bool Assemble_JF(ISADefinition CurrentDefinition, ushort instID, Segment s, OperationResult<CompilationObject> result, IntermediateInstruction IInstruction, int PC)
		{
			if (instID != SCVMInst.JF)
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
			if (!st.GoNext())
			{
				result.AddError(new IncompleteInstructionError(st.Current, sPos));
				return false;
			}
			var TargetRegister = st.Current;
			InstPtr.Set(SCVMInst.JF, 0);

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

			if (DataConversion.TryParseRegister(CurrentDefinition, TargetRegister.content, result, out var _R))
			{
				InstPtr.Set(_R, 7);
			}
			else
			{
				IInstruction.UnsolvedSymbols.Add(new UnsolvedSymbol(ValueSeg.content, 0));
				IInstruction.IsIntermediate = true;
			}
			return true;
		}
		public unsafe static bool Assemble_JFF(ISADefinition CurrentDefinition, ushort instID, Segment s, OperationResult<CompilationObject> result, IntermediateInstruction IInstruction, int PC)
		{
			if (instID != SCVMInst.JFF)
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
			if (!st.GoNext())
			{
				result.AddError(new IncompleteInstructionError(st.Current, sPos));
				return false;
			}
			var TargetFlag = st.Current;
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
			{
				if (DataConversion.TryParseInt(CurrentDefinition, TargetFlag.content, result, out var V))
				{
					InstPtr.Set(V, 7);
				}
				else
				{
					IInstruction.UnsolvedSymbols.Add(new UnsolvedSymbol(ValueSeg.content, 1));
					IInstruction.IsIntermediate = true;
				}
			}
			return true;
		}

	}
}
