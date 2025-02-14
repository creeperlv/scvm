using scvm.core.dispatchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace scvm.core
{
	public class SCVMCallingConvention
	{
		public const int Function_Parameter64_0=10;
		public const int Function_Parameter64_1=11;
		public const int Function_Parameter64_2=12;
	}
	public unsafe class SCVMMachine : IDisposable
	{
		public IMemoryManagementUnit MMU;
		public SCVMCPU CPU;
		public MachineWrokMode WrokMode = MachineWrokMode.DisposeOnDoneExecution;
		public IDispatcher Dispatcher;
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
	public unsafe class SCVMCPU : IDisposable
	{
		public SCVMMachine Machine;
		public List<SCVMProcessor> Processors;
		public int MaxCPUCount;
		private int doneCount = 0;
		public SCVMCPU()
		{
			Processors = new List<SCVMProcessor>
			{
				new SCVMProcessor()
			};
			MaxCPUCount = 1;
		}
		public SCVMCPU(int CPUCount)
		{
			Processors = new List<SCVMProcessor>();
			for (int i = 0; i < CPUCount; i++)
			{
				Processors.Add(new SCVMProcessor());
			}
			MaxCPUCount = CPUCount;
		}
		public void ReportDone(int ProcessorID)
		{
			doneCount++;
			if (doneCount >= Processors.Count)
			{
				if (Machine.WrokMode == MachineWrokMode.DisposeOnDoneExecution)
					Machine.Dispose();
			}
		}
		public void Dispose()
		{
			foreach (var processor in Processors)
			{
				processor.Dispose();
			}
		}
	}
}
