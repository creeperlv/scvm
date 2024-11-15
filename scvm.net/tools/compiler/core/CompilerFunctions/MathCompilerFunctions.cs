﻿using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using scvm.core;
using scvm.core.utilities;
using scvm.tools.compiler.core.Errors;
using scvm.tools.compiler.core.utilities;
using System;

namespace scvm.tools.compiler.core.CompilerFunctions
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
			IntPtr InstPtr = (IntPtr)(&instruction);
			InstPtr.Set(instID);
			SegmentTraveler st = new SegmentTraveler(s);
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var current = st.Current;
			if (CurrentDefinition.NativeTypes.TryGetValue(current.content.ToLower(), out var type))
			{
				InstPtr.Set(type, 2);
			}
			else
			{
				result.AddError(new UnknownBaseTypeError(current));
				return false;
			}
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			current = st.Current;
			var T = current;
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			current = st.Current;
			var L = current;
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			current = st.Current;
			var R = current;
			byte _T;
			byte _L;
			if (!DataConversion.TryParseRegister(CurrentDefinition, T.content, result, out _T))
			{
				result.AddError(new TypeMismatchError(T, CurrentDefinition.NativeTypes.ReverseQuery(NativeType.R)));
				return false;
			}
			if (!DataConversion.TryParseRegister(CurrentDefinition, L.content, result, out _L))
			{
				result.AddError(new TypeMismatchError(T, CurrentDefinition.NativeTypes.ReverseQuery(NativeType.R)));
				return false;
			}
			InstPtr.Set(_T, 3);
			InstPtr.Set(_L, 4);
			{

				byte _R;

				if (!DataConversion.TryParseRegister(CurrentDefinition, R.content, result, out _R))
				{
					result.AddError(new TypeMismatchError(T, CurrentDefinition.NativeTypes.ReverseQuery(NativeType.R)));
					return false;
				}
				InstPtr.Set(_R, 5);
			}
			IInstruction.Instruction = instruction;
			return true;
		}

	}
}
