using scvm.core.functions;
using scvm.core.utilities;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static scvm.core.libNative.stdlib;
namespace scvm.core
{
	[StructLayout(LayoutKind.Sequential)]
	public struct SCVMMachineStat
	{
		public ulong Flags;
	}
	public delegate void InterruptHandler(SCVMProcessor processor);
	public unsafe class SCVMProcessor : IDisposable
	{
		public const int InterruptMaxCount = 0xFFFF;
		public int ThisCPUID;
		public SCVMMachineStat MStat;
		public ulong PageTable;
		public ulong PC;
		public uint Timer;
		public SCVMCPU ParentCPU;
		public SCVMRegister Register;
		public FullInterruptConfig* SWInterrupts;
		public FullInterruptConfig* HWInterrupts;
		public Dictionary<int, InterruptHandler> InterruptHandlers = new Dictionary<int, InterruptHandler>();
		public SCVMProcessor()
		{
			SWInterrupts = malloc<FullInterruptConfig>(sizeof(FullInterruptConfig) * InterruptMaxCount);
			HWInterrupts = malloc<FullInterruptConfig>(sizeof(FullInterruptConfig) * InterruptMaxCount);
			Register.InitRegisters();
		}

		public void Dispose()
		{
			Register.Dispose();
			free(SWInterrupts);
			free(HWInterrupts);
		}

		public unsafe void Execute()
		{
			var inst = ParentCPU.Machine.MMU.GetPtr(PC, PageTable, ThisCPUID, sizeof(Instruction));
			Instruction instruction = ((Instruction*)inst)[0];
			var Op = instruction.CastAs<Instruction, ushort>();
			switch (Op)
			{
				case SCVMInst.ADD:
					{
						SCVMMathFunctions.MathAdd(Register, instruction);
					}
					break;
				case SCVMInst.SUB:
					{
						SCVMMathFunctions.MathSub(Register, instruction);
					}
					break;
				case SCVMInst.MUL:
					{
						SCVMMathFunctions.MathMul(Register, instruction);
					}
					break;
				case SCVMInst.DIV:
					{
						SCVMMathFunctions.MathDiv(Register, instruction);
					}
					break;
				default:
					break;
			}
		}
	}
}
