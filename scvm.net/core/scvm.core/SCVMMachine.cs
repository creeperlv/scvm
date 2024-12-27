using System;
using System.Collections.Generic;
using System.Text;

namespace scvm.core
{
	public unsafe class SCVMMachine : IDisposable
	{
		public IMemoryManagementUnit MMU;
		public SCVMCPU CPU;
		public MachineWrokMode WrokMode = MachineWrokMode.DisposeOnDoneExecution;
		public byte* UnknownInterrupt(int Processor, InterruptType InterruptType, ushort InterruptID)
		{
			return null;
		}
		public byte* SegFault(int CPU, ulong Address)
		{
			return null;
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
		private int doneCount = 0;
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
