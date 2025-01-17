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
		public ProcessorState state;
		public SCVMCPU ParentCPU;
		public FullInterruptConfig* SWInterrupts;
		public FullInterruptConfig* HWInterrupts;
		public Dictionary<int, InterruptHandler> HWInterruptHandlers = new Dictionary<int, InterruptHandler>();
		public Dictionary<int, InterruptHandler> SWInterruptHandlers = new Dictionary<int, InterruptHandler>();
		public SCVMProcessor()
		{
			SWInterrupts = malloc<FullInterruptConfig>(sizeof(FullInterruptConfig) * InterruptMaxCount);
			HWInterrupts = malloc<FullInterruptConfig>(sizeof(FullInterruptConfig) * InterruptMaxCount);
			state.Register.InitRegisters();
		}

		public void Dispose()
		{
			state.Register.Dispose();
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
			var inst = ParentCPU.Machine.MMU.GetPtr((ulong)state.PC, state.PageTable, ThisProcessorID, sizeof(Instruction));
			Instruction instruction = ((Instruction*)inst)[0];
			var Op = instruction.CastAs<Instruction, ushort>();
			switch (Op)
			{
				case SCVMInst.ADD:
					{
						SCVMMathFunctions.MathAdd(state.Register, instruction);
					}
					break;
				case SCVMInst.SUB:
					{
						SCVMMathFunctions.MathSub(state.Register, instruction);
					}
					break;
				case SCVMInst.MUL:
					{
						SCVMMathFunctions.MathMul(state.Register, instruction);
					}
					break;
				case SCVMInst.DIV:
					{
						SCVMMathFunctions.MathDiv(state.Register, instruction);
					}
					break;
				case SCVMInst.JMP:
					{
						var InstAlt = instruction.CastAs<Instruction, Instruction_OpSeparated>(0);
						if (InstAlt.D1 == 1)
						{
							var offset = state.Register.GetData<int>(InstAlt.D2.CastAs<uint, int>(0));
							if (offset > 0)
								state.PC += (uint)offset;
							else
							{
								state.PC -= (uint)(-offset);
							}
						}
						else
						{
							var offset = InstAlt.D2.CastAs<uint, int>(0);
							if (offset > 0)
								state.PC += (uint)offset;
							else
							{
								state.PC -= (uint)(-offset);
							}
						}
					}
					break;
				case SCVMInst.CP:
					{
						var InstAlt = instruction.CastAs<Instruction, Instruction_OpSeparated_ByteSegmented>(0);
						var sReg = InstAlt.D0;
						var tReg = InstAlt.D1;
						var lReg = InstAlt.D2;
						var sPtr = state.Register.GetData<ulong>(sReg);
						var tPtr = state.Register.GetData<ulong>(tReg);
						var len = state.Register.GetData<int>(lReg);
						var src = this.ParentCPU.Machine.MMU.GetPtr(sPtr, this.state.PageTable, this.ThisProcessorID, len);
						var ptr = this.ParentCPU.Machine.MMU.GetPtr(sPtr, this.state.PageTable, this.ThisProcessorID, len);
						Buffer.MemoryCopy(src,ptr,len,len);
					}
					break;
				case SCVMInst.LR:
					{
						var InstAlt = instruction.CastAs<Instruction, Instruction_OpSeparated_ByteSegmented>(0);
						var IsReg = InstAlt.D0;
						var TargetReg = InstAlt.D1;
						var PointerRegister = InstAlt.D2;
						var Ptr = state.Register.GetData<ulong>(PointerRegister);
						byte Len = 0;
						var Length = InstAlt.D3;
						if (IsReg == 0)
						{
							Len = Length;
						}
						else
						{
							Len = state.Register.GetData<byte>(Length);
						}
						var ptr = this.ParentCPU.Machine.MMU.GetPtr(Ptr, this.state.PageTable, this.ThisProcessorID, Len);
						state.Register.CopyFrom(TargetReg, ptr, Len);


					}
					break;
				case SCVMInst.SR:
					{
						var InstAlt = instruction.CastAs<Instruction, Instruction_OpSeparated_ByteSegmented>(0);
						var IsReg = InstAlt.D0;
						var TargetReg = InstAlt.D1;
						var PointerRegister = InstAlt.D2;
						var Ptr = state.Register.GetData<ulong>(PointerRegister);
						byte Len = 0;
						var Length = InstAlt.D3;
						if (IsReg == 0)
						{
							Len = Length;
						}
						else
						{
							Len = state.Register.GetData<byte>(Length);
						}
						var ptr = this.ParentCPU.Machine.MMU.GetPtr(Ptr, this.state.PageTable, this.ThisProcessorID, Len);
						state.Register.CopyTo(TargetReg, ptr, Len);
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
