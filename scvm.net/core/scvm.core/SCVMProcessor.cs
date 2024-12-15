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
	public enum InterruptType : byte
	{
		HW = 0, SW = 1
	}
	public delegate void InterruptHandler(SCVMProcessor processor);
	public unsafe class SCVMProcessor : IDisposable
	{
		public const int InterruptMaxCount = 0xFFFF;
		public int ThisProcessorID;
		public SCVMMachineStat MStat;
		public ulong PageTable;
		public ulong PC;
		public uint Timer;
		public SCVMCPU ParentCPU;
		public SCVMRegister Register;
		public FullInterruptConfig* SWInterrupts;
		public FullInterruptConfig* HWInterrupts;
		public Dictionary<int, InterruptHandler> HWInterruptHandlers = new Dictionary<int, InterruptHandler>();
		public Dictionary<int, InterruptHandler> SWInterruptHandlers = new Dictionary<int, InterruptHandler>();
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

		void InternalSyscall()
		{

		}
		public void SetupSyscall(FullInterruptConfig config, ushort ID, InterruptType Type)
		{
			switch (Type)
			{
				case InterruptType.HW:
					HWInterrupts[ID] = config;
					break;
				case InterruptType.SW:
					SWInterrupts[ID] = config;
					break;
				default:
					break;
			}
		}
		public unsafe void Execute()
		{
			var inst = ParentCPU.Machine.MMU.GetPtr((ulong)PC, PageTable, ThisProcessorID, sizeof(Instruction));
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
				case SCVMInst.JMP:
					{
						var InstAlt = instruction.CastAs<Instruction, Instruction_OpSeparated>(0);
						if (InstAlt.D1 == 1)
						{
							var offset = Register.GetData<int>(InstAlt.D2.CastAs<uint, int>(0));
							if (offset > 0)
								PC += (uint)offset;
							else
							{
								PC -= (uint)(-offset);
							}
						}
						else
						{
							var offset = InstAlt.D2.CastAs<uint, int>(0);
							if (offset > 0)
								PC += (uint)offset;
							else
							{
								PC -= (uint)(-offset);
							}
						}
					}
					break;
				case SCVMInst.SYSCALL:
					{
						var T = instruction.CastAsWOffsetBytes<Instruction, InterruptType>(2);
						var id = instruction.CastAsWOffsetBytes<Instruction, ushort>(3);
						switch (T)
						{
							case InterruptType.HW:
								if (HWInterrupts[id].IsConfigured == false)
								{
									this.ParentCPU.Machine.UnknownInterrupt(ThisProcessorID, T, id);
								}
								else
								{
									if (HWInterrupts[id].IsInjected)
									{
										if (HWInterruptHandlers.TryGetValue(id, out var handler))
										{
											handler(this);
										}
									}
								}
								break;
							case InterruptType.SW:
								if (SWInterrupts[id].IsConfigured == false)
								{
									this.ParentCPU.Machine.UnknownInterrupt(ThisProcessorID, T, id);
								}
								else
								{
									if (SWInterrupts[id].IsInjected)
									{
										if (SWInterruptHandlers.TryGetValue(id, out var handler))
										{
											handler(this);
										}
									}
								}
								break;
							default:
								break;
						}
					}
					break;
				default:
					break;
			}
		}
	}
}
