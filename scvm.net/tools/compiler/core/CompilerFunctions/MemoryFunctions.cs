﻿using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using scvm.core;
using scvm.core.utilities;
using scvm.tools.compiler.core.Errors;
using scvm.tools.compiler.core.utilities;
using System;

namespace scvm.tools.compiler.core.CompilerFunctions
{
	public static class MemoryFunctions
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
			if (!st.GoNext())
			{
				result.AddError(new IncompleteInstructionError(st.Current, sourcePosition));
				return false;
			}

			byte IsRegister = 0;
			if (!DataConversion.TryParseRegister(CurrentDefinition, Register.content, result, out var IRegister))
			{

			}
			else
			{

			}

			return true;
		}

	}
}
