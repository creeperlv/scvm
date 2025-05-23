﻿using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using scvm.core;
using scvm.core.utilities;
using scvm.tools.compiler.core.Errors;
using scvm.tools.compiler.core.utilities;
using System;

namespace scvm.tools.compiler.core.CompilerFunctions.FirstPass
{
	public static class MemoryCompilerFunctions
	{
		public unsafe static bool Compile_BasicLRSR(ISADefinition CurrentDefinition, ushort instID, Segment s, OperationResult<CompilationObject> result, IntermediateInstruction IInstruction, int PC)
		{
			switch (instID)
			{
				case SCVMInst.LR:
				case SCVMInst.SR:
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
			var Register = current;
			if (!st.GoNext())
			{
				result.AddError(new IncompleteInstructionError(st.Current, sourcePosition));
				return false;
			}
			current = st.Current;
			var Ptr = current;
			if (!st.GoNext())
			{
				result.AddError(new IncompleteInstructionError(st.Current, sourcePosition));
				return false;
			}
			current = st.Current;
			var Length = current;

			byte IsRegister = 0;
			if (DataConversion.TryParseRegister(CurrentDefinition, Register.content, result, out var IRegister))
			{

				InstPtr.Set(IRegister, 3);
			}
			else
			{
				IInstruction.UnsolvedSymbols.Add(new UnsolvedSymbol(Register.content, 0));
				IInstruction.IsIntermediate = true;
			}
			if (DataConversion.TryParseRegister(CurrentDefinition, Ptr.content, result, out var IPtr))
			{
				InstPtr.Set(IPtr, 4);
			}
			else
			{
				IInstruction.UnsolvedSymbols.Add(new UnsolvedSymbol(Ptr.content, 1));
				IInstruction.IsIntermediate = true;
			}
			if (DataConversion.TryParseRegister(CurrentDefinition, Length.content, result, out var RLen))
			{
				IsRegister = 1;
				InstPtr.Set(RLen, 5);
			}
			else if (!DataConversion.TryParseUByte(CurrentDefinition, Length.content, out var ILen))
			{
				InstPtr.Set(ILen, 5);
			}
			else
			{

				IInstruction.UnsolvedSymbols.Add(new UnsolvedSymbol(Ptr.content, 1));
				IInstruction.IsIntermediate = true;
			}
			IInstruction.Instruction=instruction;
			InstPtr.Set(IsRegister, 2);
			return true;
		}
		public unsafe static bool Compile_BasicCP(ISADefinition CurrentDefinition, ushort instID, Segment s, OperationResult<CompilationObject> result, IntermediateInstruction IInstruction, int PC)
		{
			switch (instID)
			{
				case SCVMInst.CP:
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
			byte _R;
			if (!DataConversion.TryParseRegister(CurrentDefinition, T.content, result, out _T))
			{
				//result.AddError(new TypeMismatchError(T, sourcePosition, CurrentDefinition.NativeTypes.ReverseQuery(NativeType.R)));
				//return false;

				IInstruction.UnsolvedSymbols.Add(new UnsolvedSymbol(T.content, 0));
				IInstruction.IsIntermediate = true;
			}
			if (!DataConversion.TryParseRegister(CurrentDefinition, L.content, result, out _L))
			{
				IInstruction.UnsolvedSymbols.Add(new UnsolvedSymbol(L.content, 1));
				IInstruction.IsIntermediate = true;
			}

			if (!DataConversion.TryParseRegister(CurrentDefinition, R.content, result, out _R))
			{
				IInstruction.UnsolvedSymbols.Add(new UnsolvedSymbol(R.content, 2));
				IInstruction.IsIntermediate = true;
			}
			InstPtr.Set(_T, 2);
			InstPtr.Set(_L, 3);
			InstPtr.Set(_R, 4);
			IInstruction.Instruction = instruction;
			return true;
		}
	}
}
