using scvm.core.data;
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
		public ulong PC;
		public ulong PT;
		public bool IsPrivileged()
		{
			return (Flags & MStatFlags.Privilege) == MStatFlags.Privilege;
		}
	}
	public class MStatFlags
	{
		public const ulong Privilege = 0x1;
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
			var inst = ParentCPU.Machine.MMU.GetPtr((ulong)state.MStat.PC, state.MStat.PT, ThisProcessorID, sizeof(Instruction));

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
				case SCVMInst.OFC_ADD:
					{
						SCVMMathFunctions.MathAdd(state.Register, instruction);
					}
					break;
				case SCVMInst.OFC_SUB:
					{
						SCVMMathFunctions.MathSub(state.Register, instruction);
					}
					break;
				case SCVMInst.OFC_MUL:
					{
						SCVMMathFunctions.MathMul(state.Register, instruction);
					}
					break;
				case SCVMInst.OFC_DIV:
					{
						SCVMMathFunctions.MathDiv(state.Register, instruction);
					}
					break;
				case SCVMInst.CVT:
					{
						Instruction_OpSeparated_ByteSegmented altInst = instruction.CastAs<Instruction, Instruction_OpSeparated_ByteSegmented>();
						var L = altInst.D0;
						var R = altInst.D1;
						var LType = (NativeType)altInst.D2;
						var RType = (NativeType)altInst.D3;
						switch (LType)
						{
							case NativeType.BS:
								{
									var value = this.state.Register.GetData<CompactSByte>(L);
									switch (RType)
									{
										case NativeType.BS:
											this.state.Register.WriteData(R, value.Cast_SByte());
											break;
										case NativeType.BU:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										case NativeType.S:
											this.state.Register.WriteData(R, value.Cast_Short());
											break;
										case NativeType.SU:
											this.state.Register.WriteData(R, value.Cast_UShort());
											break;
										case NativeType.I:
											this.state.Register.WriteData(R, value.Cast_Int());
											break;
										case NativeType.IU:
											this.state.Register.WriteData(R, value.Cast_UInt());
											break;
										case NativeType.L:
											this.state.Register.WriteData(R, value.Cast_Long());
											break;
										case NativeType.LU:
											this.state.Register.WriteData(R, value.Cast_ULong());
											break;
										case NativeType.F:
											this.state.Register.WriteData(R, value.Cast_Float());
											break;
										case NativeType.D:
											this.state.Register.WriteData(R, value.Cast_Double());
											break;
										case NativeType.R:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										default:
											break;
									}
								}
								break;
							case NativeType.R:
							case NativeType.BU:
								{
									var value = this.state.Register.GetData<CompactByte>(L);
									switch (RType)
									{
										case NativeType.BS:
											this.state.Register.WriteData(R, value.Cast_SByte());
											break;
										case NativeType.BU:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										case NativeType.S:
											this.state.Register.WriteData(R, value.Cast_Short());
											break;
										case NativeType.SU:
											this.state.Register.WriteData(R, value.Cast_UShort());
											break;
										case NativeType.I:
											this.state.Register.WriteData(R, value.Cast_Int());
											break;
										case NativeType.IU:
											this.state.Register.WriteData(R, value.Cast_UInt());
											break;
										case NativeType.L:
											this.state.Register.WriteData(R, value.Cast_Long());
											break;
										case NativeType.LU:
											this.state.Register.WriteData(R, value.Cast_ULong());
											break;
										case NativeType.F:
											this.state.Register.WriteData(R, value.Cast_Float());
											break;
										case NativeType.D:
											this.state.Register.WriteData(R, value.Cast_Double());
											break;
										case NativeType.R:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										default:
											break;
									}
								}
								break;
							case NativeType.S:
								{
									var value = this.state.Register.GetData<CompactShort>(L);
									switch (RType)
									{
										case NativeType.BS:
											this.state.Register.WriteData(R, value.Cast_SByte());
											break;
										case NativeType.BU:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										case NativeType.S:
											this.state.Register.WriteData(R, value.Cast_Short());
											break;
										case NativeType.SU:
											this.state.Register.WriteData(R, value.Cast_UShort());
											break;
										case NativeType.I:
											this.state.Register.WriteData(R, value.Cast_Int());
											break;
										case NativeType.IU:
											this.state.Register.WriteData(R, value.Cast_UInt());
											break;
										case NativeType.L:
											this.state.Register.WriteData(R, value.Cast_Long());
											break;
										case NativeType.LU:
											this.state.Register.WriteData(R, value.Cast_ULong());
											break;
										case NativeType.F:
											this.state.Register.WriteData(R, value.Cast_Float());
											break;
										case NativeType.D:
											this.state.Register.WriteData(R, value.Cast_Double());
											break;
										case NativeType.R:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										default:
											break;
									}
								}
								break;
							case NativeType.SU:
								{
									var value = this.state.Register.GetData<CompactUShort>(L);
									switch (RType)
									{
										case NativeType.BS:
											this.state.Register.WriteData(R, value.Cast_SByte());
											break;
										case NativeType.BU:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										case NativeType.S:
											this.state.Register.WriteData(R, value.Cast_Short());
											break;
										case NativeType.SU:
											this.state.Register.WriteData(R, value.Cast_UShort());
											break;
										case NativeType.I:
											this.state.Register.WriteData(R, value.Cast_Int());
											break;
										case NativeType.IU:
											this.state.Register.WriteData(R, value.Cast_UInt());
											break;
										case NativeType.L:
											this.state.Register.WriteData(R, value.Cast_Long());
											break;
										case NativeType.LU:
											this.state.Register.WriteData(R, value.Cast_ULong());
											break;
										case NativeType.F:
											this.state.Register.WriteData(R, value.Cast_Float());
											break;
										case NativeType.D:
											this.state.Register.WriteData(R, value.Cast_Double());
											break;
										case NativeType.R:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										default:
											break;
									}
								}
								break;
							case NativeType.I:
								{
									var value = this.state.Register.GetData<CompactInt>(L);
									switch (RType)
									{
										case NativeType.BS:
											this.state.Register.WriteData(R, value.Cast_SByte());
											break;
										case NativeType.BU:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										case NativeType.S:
											this.state.Register.WriteData(R, value.Cast_Short());
											break;
										case NativeType.SU:
											this.state.Register.WriteData(R, value.Cast_UShort());
											break;
										case NativeType.I:
											this.state.Register.WriteData(R, value.Cast_Int());
											break;
										case NativeType.IU:
											this.state.Register.WriteData(R, value.Cast_UInt());
											break;
										case NativeType.L:
											this.state.Register.WriteData(R, value.Cast_Long());
											break;
										case NativeType.LU:
											this.state.Register.WriteData(R, value.Cast_ULong());
											break;
										case NativeType.F:
											this.state.Register.WriteData(R, value.Cast_Float());
											break;
										case NativeType.D:
											this.state.Register.WriteData(R, value.Cast_Double());
											break;
										case NativeType.R:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										default:
											break;
									}
								}
								break;
							case NativeType.IU:
								{
									var value = this.state.Register.GetData<CompactUInt>(L);
									switch (RType)
									{
										case NativeType.BS:
											this.state.Register.WriteData(R, value.Cast_SByte());
											break;
										case NativeType.BU:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										case NativeType.S:
											this.state.Register.WriteData(R, value.Cast_Short());
											break;
										case NativeType.SU:
											this.state.Register.WriteData(R, value.Cast_UShort());
											break;
										case NativeType.I:
											this.state.Register.WriteData(R, value.Cast_Int());
											break;
										case NativeType.IU:
											this.state.Register.WriteData(R, value.Cast_UInt());
											break;
										case NativeType.L:
											this.state.Register.WriteData(R, value.Cast_Long());
											break;
										case NativeType.LU:
											this.state.Register.WriteData(R, value.Cast_ULong());
											break;
										case NativeType.F:
											this.state.Register.WriteData(R, value.Cast_Float());
											break;
										case NativeType.D:
											this.state.Register.WriteData(R, value.Cast_Double());
											break;
										case NativeType.R:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										default:
											break;
									}
								}
								break;
							case NativeType.L:
								{
									var value = this.state.Register.GetData<CompactLong>(L);
									switch (RType)
									{
										case NativeType.BS:
											this.state.Register.WriteData(R, value.Cast_SByte());
											break;
										case NativeType.BU:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										case NativeType.S:
											this.state.Register.WriteData(R, value.Cast_Short());
											break;
										case NativeType.SU:
											this.state.Register.WriteData(R, value.Cast_UShort());
											break;
										case NativeType.I:
											this.state.Register.WriteData(R, value.Cast_Int());
											break;
										case NativeType.IU:
											this.state.Register.WriteData(R, value.Cast_UInt());
											break;
										case NativeType.L:
											this.state.Register.WriteData(R, value.Cast_Long());
											break;
										case NativeType.LU:
											this.state.Register.WriteData(R, value.Cast_ULong());
											break;
										case NativeType.F:
											this.state.Register.WriteData(R, value.Cast_Float());
											break;
										case NativeType.D:
											this.state.Register.WriteData(R, value.Cast_Double());
											break;
										case NativeType.R:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										default:
											break;
									}
								}
								break;
							case NativeType.LU:
								{
									var value = this.state.Register.GetData<CompactULong>(L);
									switch (RType)
									{
										case NativeType.BS:
											this.state.Register.WriteData(R, value.Cast_SByte());
											break;
										case NativeType.BU:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										case NativeType.S:
											this.state.Register.WriteData(R, value.Cast_Short());
											break;
										case NativeType.SU:
											this.state.Register.WriteData(R, value.Cast_UShort());
											break;
										case NativeType.I:
											this.state.Register.WriteData(R, value.Cast_Int());
											break;
										case NativeType.IU:
											this.state.Register.WriteData(R, value.Cast_UInt());
											break;
										case NativeType.L:
											this.state.Register.WriteData(R, value.Cast_Long());
											break;
										case NativeType.LU:
											this.state.Register.WriteData(R, value.Cast_ULong());
											break;
										case NativeType.F:
											this.state.Register.WriteData(R, value.Cast_Float());
											break;
										case NativeType.D:
											this.state.Register.WriteData(R, value.Cast_Double());
											break;
										case NativeType.R:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										default:
											break;
									}
								}
								break;
							case NativeType.F:
								{
									var value = this.state.Register.GetData<CompactSingle>(L);
									switch (RType)
									{
										case NativeType.BS:
											this.state.Register.WriteData(R, value.Cast_SByte());
											break;
										case NativeType.BU:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										case NativeType.S:
											this.state.Register.WriteData(R, value.Cast_Short());
											break;
										case NativeType.SU:
											this.state.Register.WriteData(R, value.Cast_UShort());
											break;
										case NativeType.I:
											this.state.Register.WriteData(R, value.Cast_Int());
											break;
										case NativeType.IU:
											this.state.Register.WriteData(R, value.Cast_UInt());
											break;
										case NativeType.L:
											this.state.Register.WriteData(R, value.Cast_Long());
											break;
										case NativeType.LU:
											this.state.Register.WriteData(R, value.Cast_ULong());
											break;
										case NativeType.F:
											this.state.Register.WriteData(R, value.Cast_Float());
											break;
										case NativeType.D:
											this.state.Register.WriteData(R, value.Cast_Double());
											break;
										case NativeType.R:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										default:
											break;
									}
								}
								break;
							case NativeType.D:
								{
									var value = this.state.Register.GetData<CompactDouble>(L);
									switch (RType)
									{
										case NativeType.BS:
											this.state.Register.WriteData(R, value.Cast_SByte());
											break;
										case NativeType.BU:
											this.state.Register.WriteData(R, value.Cast_Byte());
											break;
										case NativeType.S:
											this.state.Register.WriteData(R, value.Cast_Short());
											break;
										case NativeType.SU:
											this.state.Register.WriteData(R, value.Cast_UShort());
											break;
										case NativeType.I:
											this.state.Register.WriteData(R, value.Cast_Int());
											break;
										case NativeType.IU:
											this.state.Register.WriteData(R, value.Cast_UInt());
											break;
										case NativeType.L:
											this.state.Register.WriteData(R, value.Cast_Long());
											break;
										case NativeType.LU:
											this.state.Register.WriteData(R, value.Cast_ULong());
											break;
										case NativeType.F:
											this.state.Register.WriteData(R, value.Cast_Float());
											break;
										case NativeType.D:
											this.state.Register.WriteData(R, value.Cast_Double());
											break;
										case NativeType.R:
											this.state.Register.WriteData(R, value.Cast_Byte());
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
					break;
				case SCVMInst.JMP:
					{
						var InstAlt = instruction.CastAs<Instruction, Instruction_OpSeparated>(0);
						if (InstAlt.D1 == 1)
						{
							var offset = state.Register.GetData<int>(InstAlt.D2.CastAs<uint, int>(0));
							if (offset > 0)
								state.MStat.PC += (uint)offset;
							else
							{
								state.MStat.PC -= (uint)(-offset);
							}
						}
						else
						{
							var offset = InstAlt.D2.CastAs<uint, int>(0);
							if (offset > 0)
								state.MStat.PC += (uint)offset;
							else
							{
								state.MStat.PC -= (uint)(-offset);
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
									state.MStat.PC += (uint)offset;
								else
								{
									state.MStat.PC -= (uint)(-offset);
								}
							}
							else
							{
								var offset = InstAlt2.D1;
								if (offset > 0)
									state.MStat.PC += (uint)offset;
								else
								{
									state.MStat.PC -= (uint)(-offset);
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
						var src = this.ParentCPU.Machine.MMU.GetPtr(sPtr, this.state.MStat.PT, this.ThisProcessorID, len);
						if (src == null)
						{
							return;
						}
						var ptr = this.ParentCPU.Machine.MMU.GetPtr(sPtr, this.state.MStat.PT, this.ThisProcessorID, len);
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
						var ptr = this.ParentCPU.Machine.MMU.GetPtr(Ptr, this.state.MStat.PT, this.ThisProcessorID, Len);
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
						var ptr = this.ParentCPU.Machine.MMU.GetPtr(Ptr, this.state.MStat.PT, this.ThisProcessorID, Len);
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
						if (this.state.MStat.IsPrivileged())
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
								case SCVMSysRegIDs.TimerConfig:
									{
										this.state.TimerCfg = state.Register.GetData<SCVMTimerCfg>(InstAlt.D1);
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
						else
						{
							ControlProtect();
						}
					}
					break;
				case SCVMInst.SYSREGR:
					{
						if (this.state.MStat.IsPrivileged())
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
								case SCVMSysRegIDs.TimerConfig:
									{
										state.Register.SetData(InstAlt.D1, this.state.TimerCfg);
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
						else
						{
							ControlProtect();
						}
					}
					break;
				case SCVMInst.NOP:
					break;
				case SCVMInst.HALT:
					OnHaltCalled?.Invoke();
					break;
				case SCVMInst.SCALC:
					{
						var InstAlt = instruction.CastAs<Instruction, Instruction_OpSeparated>(0);
						SCVMAdvMathFunctions.Scalc(this.state.Register, InstAlt);
					}
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
							this.state.MStat.PC = config.PC;
							this.state.MStat.PT = config.PT;
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
							this.state.MStat.PC = config.PC;
							this.state.MStat.PT = config.PT;
						}
					}
					break;
				default:
					break;
			}
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ControlProtect()
		{
			this.TryHWInterrupt(SCVMBasicHardwareInterruptTable.ControlProtect);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AdvancePC()
		{
			this.state.MStat.PC += 1;
			if (this.state.TimerCfg.IsSet != 0)
			{
				this.state.RelativePC += 1;
				if (this.state.RelativePC >= this.state.TimerCfg.TargetIC)
				{
					this.TryHWInterrupt(SCVMBasicHardwareInterruptTable.ICTimer);
					this.state.RelativePC = 0;
				}
			}
			else
			{
				this.state.RelativePC = 0;
			}
		}
	}
}
