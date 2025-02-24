using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using scvm.core;
using scvm.tools.compiler.core.CompilerFunctions.FirstPass;
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
				{SCVMInst.ADD,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.SUB,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.MUL,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.DIV,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.OFC_ADD,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.OFC_SUB,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.OFC_MUL,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.OFC_DIV,MathCompilerFunctions.Compile_BasicMath },
				{SCVMInst.JMP,JumpCompilerFunctions.Assemble_JMP },
				{SCVMInst.JF,JumpCompilerFunctions.Assemble_JF },
				{SCVMInst.JFF,JumpCompilerFunctions.Assemble_JFF },
				{SCVMInst.LR,MemoryCompilerFunctions.Compile_BasicLRSR},
				{SCVMInst.SR,MemoryCompilerFunctions.Compile_BasicLRSR},
				{SCVMInst.SYSCALL,SysCompilerFunctions.Assemble_SYSCALL},
				{SCVMInst.SYSREGR,SysCompilerFunctions.Assemble_SYSREGRW},
				{SCVMInst.SYSREGW,SysCompilerFunctions.Assemble_SYSREGRW},
				{SCVMInst.RF,SysCompilerFunctions.Assemble_RF},
				{SCVMInst.CP,MemoryCompilerFunctions.Compile_BasicCP},
			}
		};
	}
}
