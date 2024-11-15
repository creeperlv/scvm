using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using scvm.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace scvm.tools.compiler.core.CompilerFunctions
{
	public delegate bool CompilerFunction(ISADefinition CurrentDefinition, ushort InstructionID, Segment HEAD, OperationResult<CompilationObject> result, IntermediateInstruction PointerToInstructionStruct, int PC);
	public class CompilerFunctionDefinition
	{
		public Dictionary<ushort, CompilerFunction> CompilerFunctions = new Dictionary<ushort, CompilerFunction>();
	}
	public class DefaultCompilerFunctionDefinition
	{
		public static CompilerFunctionDefinition GetDefault() => new CompilerFunctionDefinition()
		{
			CompilerFunctions = new Dictionary<ushort, CompilerFunction>()
			{
				{SCVMInst.ADD,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.SUB,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.MUL,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.DIV,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.OFC_ADD,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.OFC_SUB,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.OFC_MUL,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.OFC_DIV,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.JMP,JumpCompilerFunctions.Assemble_JMP },
			}
		};
	}
}
