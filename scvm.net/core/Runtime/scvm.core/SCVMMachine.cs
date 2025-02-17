using scvm.core.dispatchers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace scvm.core
{
	public class SCVMCallingConvention
	{
		public const int Function_Parameter64_0 = 10;
		public const int Function_Parameter64_1 = 11;
		public const int Function_Parameter64_2 = 12;
	}
	public unsafe class SCVMMachine : IDisposable
	{
		public IMemoryManagementUnit MMU;
		public SCVMCPU CPU;
		public MachineWrokMode WrokMode = MachineWrokMode.DisposeOnDoneExecution;
		public IDispatcher Dispatcher;
		public Dictionary<ushort, ISCVMSysRegHandler> SysRegisters = new Dictionary<ushort, ISCVMSysRegHandler>();
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RegisterSysRegister(ushort id, ISCVMSysRegHandler handler)
		{
			if (SysRegisters.ContainsKey(id))
				SysRegisters[id] = handler;
			else
				SysRegisters.Add(id, handler);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void UnregisterSysRegister(ushort id, ISCVMSysRegHandler handler)
		{
			if (SysRegisters.ContainsKey(id))
			{
				SysRegisters.Remove(id);
			}
		}
		public byte* UnknownInterrupt(int Processor, InterruptType InterruptType, ushort InterruptID)
		{
			return null;
		}
		public void SegFault(int CPU, ulong Address)
		{
			((ulong*)this.CPU.Processors[CPU].state.Register.Registers)[SCVMCallingConvention.Function_Parameter64_2] = Address;
			this.CPU.Processors[CPU].TryHWInterrupt(SCVMBasicHardwareInterruptTable.PageFault);
		}
		public void InvPage(int CPU, ulong PageTablePtr)
		{
			((ulong*)this.CPU.Processors[CPU].state.Register.Registers)[SCVMCallingConvention.Function_Parameter64_2] = PageTablePtr;
			this.CPU.Processors[CPU].TryHWInterrupt(SCVMBasicHardwareInterruptTable.InvalidPage);
		}
		public void InvalidInstruction(int CPU, Instruction instruction) { }

		public void Dispose()
		{
			CPU.Dispose();
			MMU.Dispose();
		}
	}
	public enum MachineWrokMode
	{
		ContinuousExistence,
		DisposeOnDoneExecution,
	}
	public unsafe interface ISCVMSysRegHandler
	{
		void SYSREGR(ushort ID, int CPUID, byte* startRegPtr);
		void SYSREGW(ushort ID, int CPUID, byte* startRegPtr);
	}
}
