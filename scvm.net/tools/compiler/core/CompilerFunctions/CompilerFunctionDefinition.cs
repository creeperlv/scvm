using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using scvm.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace scvm.tools.compiler.core.CompilerFunctions
{
	public delegate bool FirstPassCompilerFunction(ISADefinition CurrentDefinition, ushort InstructionID, Segment HEAD, OperationResult<CompilationObject> result, IntermediateInstruction PointerToInstructionStruct, int PC);
	public delegate bool SecondPassCompilerFunction(ISADefinition CurrentDefinition, ushort InstructionID, OperationResult<CompilationObject> result, CompilationObjectPool pool, IntermediateInstruction intermediateInstruction, int PC);
	public class CompilerFunctionDefinition
	{
		public Dictionary<ushort, FirstPassCompilerFunction> FirstPassCompilerFunctions = new Dictionary<ushort, FirstPassCompilerFunction>();
		public Dictionary<ushort, SecondPassCompilerFunction> SecondPassCompilerFunctions = new Dictionary<ushort, SecondPassCompilerFunction>();
	}
	public class DefaultCompilerFunctionDefinition
	{
		public static CompilerFunctionDefinition GetDefault() => new CompilerFunctionDefinition()
		{
			FirstPassCompilerFunctions = new Dictionary<ushort, FirstPassCompilerFunction>()
			{
				{SCVMInst.ADD,FirstPassMathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.SUB,FirstPassMathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.MUL,FirstPassMathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.DIV,FirstPassMathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.OFC_ADD,FirstPassMathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.OFC_SUB,FirstPassMathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.OFC_MUL,FirstPassMathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.OFC_DIV,FirstPassMathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.JMP,FirstPassJumpCompilerFunctions.Assemble_JMP },
				{SCVMInst.JF,FirstPassJumpCompilerFunctions.Assemble_JF },
				{SCVMInst.LR,FirstPassMemoryCompilerFunctions.Compile_BasicLRSR},
				{SCVMInst.SR,FirstPassMemoryCompilerFunctions.Compile_BasicLRSR},
			}
		};
	}
}
