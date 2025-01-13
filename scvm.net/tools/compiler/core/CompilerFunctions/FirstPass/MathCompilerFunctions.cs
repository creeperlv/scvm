using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using scvm.core;
using scvm.core.utilities;
using scvm.tools.compiler.core.Errors;
using scvm.tools.compiler.core.utilities;
using System;

namespace scvm.tools.compiler.core.CompilerFunctions.FirstPass
{
	public static class MathCompilerFunctions
	{
		public unsafe static bool Compile_BasicMath(ISADefinition CurrentDefinition, ushort instID, Segment s, OperationResult<CompilationObject> result, IntermediateInstruction IInstruction, int PC)
		{
			switch (instID)
			{
				case SCVMInst.ADD:
				case SCVMInst.SUB:
				case SCVMInst.MUL:
				case SCVMInst.DIV:
				case SCVMInst.OFC_ADD:
				case SCVMInst.OFC_SUB:
				case SCVMInst.OFC_MUL:
				case SCVMInst.OFC_DIV:
					break;
				default:
					return false;
			}
			Instruction instruction = default;
			SourcePosition sourcePosition = IInstruction.sourcePosition;
			IntPtr InstPtr = (IntPtr)(&instruction);
			InstPtr.Set(instID);
			SegmentTraveler st = new SegmentTraveler(s);
			if (!st.GoNext())
			{
				result.AddError(new IncompleteInstructionError(st.Current, sourcePosition));
				return false;
			}
			var current = st.Current;
			if (CurrentDefinition.NativeTypes.TryGetValue(current.content.ToLower(), out var type))
			{
				InstPtr.Set(type, 2);
			}
			else
			{
				//result.AddError(new UnknownBaseTypeError(current, sourcePosition));
				//return false;

				IInstruction.UnsolvedSymbols.Add(new UnsolvedSymbol(current.content, 0));
				IInstruction.IsIntermediate = true;
			}
			if (!st.GoNext())
			{
				result.AddError(new IncompleteInstructionError(st.Current, sourcePosition));
				return false;
			}
			current = st.Current;
			var T = current;
			if (!st.GoNext())
			{
				result.AddError(new IncompleteInstructionError(st.Current, sourcePosition));
				return false;
			}
			current = st.Current;
			var L = current;
			if (!st.GoNext())
			{
				result.AddError(new IncompleteInstructionError(st.Current, sourcePosition));
				return false;
			}
			current = st.Current;
			var R = current;
			byte _T;
			byte _L;
			if (!DataConversion.TryParseRegister(CurrentDefinition, T.content, result, out _T))
			{
				//result.AddError(new TypeMismatchError(T, sourcePosition, CurrentDefinition.NativeTypes.ReverseQuery(NativeType.R)));
				//return false;

				IInstruction.UnsolvedSymbols.Add(new UnsolvedSymbol(T.content, 1));
				IInstruction.IsIntermediate = true;
			}
			if (!DataConversion.TryParseRegister(CurrentDefinition, L.content, result, out _L))
			{
				IInstruction.UnsolvedSymbols.Add(new UnsolvedSymbol(L.content, 2));
				IInstruction.IsIntermediate = true;
			}
			InstPtr.Set(_T, 3);
			InstPtr.Set(_L, 4);
			{

				byte _R;

				if (!DataConversion.TryParseRegister(CurrentDefinition, R.content, result, out _R))
				{
					IInstruction.UnsolvedSymbols.Add(new UnsolvedSymbol(R.content, 3));
					IInstruction.IsIntermediate = true;
				}
				InstPtr.Set(_R, 5);
			}
			IInstruction.Instruction = instruction;
			return true;
		}

	}
}
