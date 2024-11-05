using System;
using System.Collections.Generic;
using System.Text;

namespace scvm.core
{
	public unsafe class SCVMMachine
	{
		public IMemoryManagementUnit MMU;
		public SCVMCPU CPU;
		public byte* SegFault(int CPU, ulong Address)
		{
			return null;
		}
		public void InvalidInstruction(int CPU, Instruction instruction) { }
	}
	public unsafe class SCVMCPU
	{
		public SCVMMachine Machine;
		public List<SCVMProcessor> Processors;
	}
}
