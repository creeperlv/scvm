﻿using System;
using System.Collections.Generic;

namespace scvm.core
{
	public unsafe class SCVMCPU : IDisposable
	{
		public SCVMMachine Machine;
		public List<SCVMProcessor> Processors;
		public int MaxCPUCount;
		private int doneCount = 0;
		public Dictionary<int, IOFunction> Ports;
		public SCVMCPU()
		{
			Processors = new List<SCVMProcessor>
			{
				new SCVMProcessor(0)
			};
			MaxCPUCount = 1;
		}
		public SCVMCPU(int CPUCount)
		{
			Processors = new List<SCVMProcessor>();
			for (int i = 0; i < CPUCount; i++)
			{
				Processors.Add(new SCVMProcessor(i));
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
	public enum IOFunctionType
	{
		In, Out, InMap, OutMap
	}
	public delegate void IOFunction(SCVMProcessor callee, IOFunctionType type, int port, IntPtr dataSrc, int DataLength);
}
