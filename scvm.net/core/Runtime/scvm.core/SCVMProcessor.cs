using scvm.core.functions;
using scvm.core.libNative;
using scvm.core.utilities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
		public Dictionary<ushort, InterruptHandler> HWInterruptHandlers = new Dictionary<ushort, InterruptHandler>();
		public Dictionary<ushort, InterruptHandler> SWInterruptHandlers = new Dictionary<ushort, InterruptHandler>();
		public Action OnHaltCalled = null;
		bool isDisposed = false;
		public bool IsDisposed() => isDisposed;
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
			isDisposed = true;
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
		public void SetInterruptHandler(ushort ID, InterruptType Type, InterruptHandler handler)
		{
			switch (Type)
			{
				case InterruptType.HW:
					if (HWInterruptHandlers.ContainsKey(ID))
					{
						HWInterruptHandlers[ID] = handler;
					}
					else HWInterruptHandlers.Add(ID, handler);
					break;
				case InterruptType.SW:
					if (SWInterruptHandlers.ContainsKey(ID))
					{
						SWInterruptHandlers[ID] = handler;
					}
					else HWInterruptHandlers.Add(ID, handler);
					break;
				default:
					break;
			}
		}
		bool WillHWInterrupt = false;
		InterruptType InterruptType = InterruptType.HW;
		ushort TargetInterrupt = 0;
		public unsafe void Execute(bool willAdvancePC = true)
		{
			if (WillHWInterrupt)
			{
				WillHWInterrupt = false;

				switch (InterruptType)
				{
					case InterruptType.HW:
						ExecuteInterrupt(InterruptType.HW, TargetInterrupt);
						break;
					case InterruptType.SW:
						ExecuteInterrupt(InterruptType.SW, TargetInterrupt);
						break;
					default:
						break;
				}

			}
			var inst = ParentCPU.Machine.MMU.GetPtr((ulong)state.PC, state.PageTable, ThisProcessorID, sizeof(Instruction));

			if (inst == null)
			{
				return;
			}
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
				case SCVMInst.JF:
					{
						var InstAlt = instruction.CastAs<Instruction, Instruction_OpSeparated>(0);
						var InstAlt2 = instruction.CastAs<Instruction, Instruction_JFOp>(0);
						var FlagReg = instruction.CastAsWOffsetBytes<Instruction, byte>(7);
						var FlagValue = this.state.Register.GetData<byte>(FlagReg);
						if (FlagValue == 1)
						{

							if (InstAlt.D0 == 1)
							{
								var PCOffsetReg = InstAlt.D1;
								var offset = this.state.Register.GetData<int>(PCOffsetReg);
								if (offset > 0)
									state.PC += (uint)offset;
								else
								{
									state.PC -= (uint)(-offset);
								}
							}
							else
							{
								var offset = InstAlt2.D1;
								if (offset > 0)
									state.PC += (uint)offset;
								else
								{
									state.PC -= (uint)(-offset);
								}
							}
						}
					}
					break;
				case SCVMInst.CMP:
					{
						var InstAlt = instruction.CastAs<Instruction, Instruction_OpSeparated_ByteSegmented>(0);
						var OP = (SCVMCmpOps)InstAlt.D0;
						switch (OP)
						{
							case SCVMCmpOps.LT:
								SCVMCompareFunctions.LT(state.Register, InstAlt);
								break;
							case SCVMCmpOps.GT:
								SCVMCompareFunctions.GT(state.Register, InstAlt);
								break;
							case SCVMCmpOps.GE:
								SCVMCompareFunctions.GE(state.Register, InstAlt);
								break;
							case SCVMCmpOps.EQ:
								SCVMCompareFunctions.EQ(state.Register, InstAlt);
								break;
							case SCVMCmpOps.LE:
								SCVMCompareFunctions.LE(state.Register, InstAlt);
								break;
							case SCVMCmpOps.NE:
								SCVMCompareFunctions.NE(state.Register, InstAlt);
								break;
							default:
								break;
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
						if (src == null)
						{
							return;
						}
						var ptr = this.ParentCPU.Machine.MMU.GetPtr(sPtr, this.state.PageTable, this.ThisProcessorID, len);
						if (ptr == null)
						{
							return;
						}
						Buffer.MemoryCopy(src, ptr, len, len);
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
						if (ptr == null)
						{
							return;
						}
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
						if (ptr == null)
						{
							return;
						}
						state.Register.CopyTo(TargetReg, ptr, Len);
					}
					break;
				case SCVMInst.SYSCALL:
					{
						var T = instruction.CastAsWOffsetBytes<Instruction, InterruptType>(2);
						var id = instruction.CastAsWOffsetBytes<Instruction, ushort>(3);
						ExecuteInterrupt(T, id);
					}
					break;
				case SCVMInst.SYSREGW:
					{
						var InstAlt = instruction.CastAs<Instruction, Instruction_OpSysReg>(0);
						switch (InstAlt.D0)
						{
							case SCVMSysRegIDs.MachineState:
								{
									state.MStat.Flags = (this.state.Register.GetData<ulong>(InstAlt.D1));
								}
								break;
							case SCVMSysRegIDs.PageTableOffset:
								{
									ParentCPU.Machine.MMU.SetPageTableStart(this.state.Register.GetData<ulong>(InstAlt.D1));
								}
								break;
							case SCVMSysRegIDs.PageTableSize:
								{
									ParentCPU.Machine.MMU.SetPageTableSize(this.state.Register.GetData<ulong>(InstAlt.D1));
								}
								break;
							default:
								if (ParentCPU.Machine.SysRegisters.TryGetValue(InstAlt.D0, out var Reg))
								{
									Reg.SYSREGW(InstAlt.D0, ThisProcessorID, this.state.Register.Registers + InstAlt.D1);
								}
								break;

						}
					}
					break;
				case SCVMInst.SYSREGR:
					{
						var InstAlt = instruction.CastAs<Instruction, Instruction_OpSysReg>(0);
						switch (InstAlt.D0)
						{
							case SCVMSysRegIDs.MachineState:
								{
									state.Register.SetData(InstAlt.D1, state.MStat);
								}
								break;
							case SCVMSysRegIDs.PageTableOffset:
								{
									state.Register.SetData(InstAlt.D1, ParentCPU.Machine.MMU.GetPageTableStart());
								}
								break;
							case SCVMSysRegIDs.PageTableSize:
								{
									state.Register.SetData(InstAlt.D1, ParentCPU.Machine.MMU.GetPageTableSize());
								}
								break;
							default:
								if (ParentCPU.Machine.SysRegisters.TryGetValue(InstAlt.D0, out var Reg))
								{
									Reg.SYSREGR(InstAlt.D0, ThisProcessorID, this.state.Register.Registers + InstAlt.D1);
								}
								break;
						}
					}
					break;
				case SCVMInst.NOP:
					break;
				case SCVMInst.HALT:
					OnHaltCalled?.Invoke();
					break;
				default:
					break;
			}
			if (willAdvancePC) AdvancePC();
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void TryHWInterrupt(ushort id)
		{
			WillHWInterrupt = true;
			InterruptType = InterruptType.HW;
			this.TargetInterrupt = id;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void TrySWInterrupt(ushort id)
		{
			WillHWInterrupt = true;
			InterruptType = InterruptType.SW;
			this.TargetInterrupt = id;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ExecuteInterrupt(InterruptType T, ushort id)
		{
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
						else
						{
							var config = HWInterrupts[id].config;
							{
								var ptr = this.ParentCPU.Machine.MMU.GetPtr(config.RegisterStore, config.PT, this.ThisProcessorID, SCVMRegister.RegisterSize);
								if (ptr == null)
								{
									return;
								}
								posix_string.memcpy(ptr, state.Register.Registers, SCVMRegister.RegisterSize);
							}
							{
								var ptr = this.ParentCPU.Machine.MMU.GetPtr(config.MStat, config.PT, this.ThisProcessorID, sizeof(SCVMMachineStat));
								if (ptr == null)
								{
									return;
								}
								((SCVMMachineStat*)ptr)[0] = this.state.MStat;
							}
							this.state.IsInterrupt = true;
							this.state.InterruptType = T;
							this.state.InterruptID = id;
							this.state.PC = config.PC;
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
						else
						{
							var config = SWInterrupts[id].config;
							{
								var ptr = this.ParentCPU.Machine.MMU.GetPtr(config.RegisterStore, config.PT, this.ThisProcessorID, SCVMRegister.RegisterSize);
								if (ptr == null)
								{
									return;
								}
								posix_string.memcpy(ptr, state.Register.Registers, SCVMRegister.RegisterSize);
							}
							{
								var ptr = this.ParentCPU.Machine.MMU.GetPtr(config.MStat, config.PT, this.ThisProcessorID, sizeof(SCVMMachineStat));
								if (ptr == null)
								{
									return;
								}
								((SCVMMachineStat*)ptr)[0] = this.state.MStat;
							}
							this.state.IsInterrupt = true;
							this.state.InterruptType = T;
							this.state.InterruptID = id;
							this.state.PC = config.PC;
						}
					}
					break;
				default:
					break;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AdvancePC()
		{
			this.state.PC += 1;
		}
	}
}
