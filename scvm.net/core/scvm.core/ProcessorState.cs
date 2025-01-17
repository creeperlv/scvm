using System;
using System.Runtime.InteropServices;
namespace scvm.core
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ProcessorState:IDisposable
	{
		public SCVMMachineStat MStat;
		public ulong PageTable;
		public ulong PC;
		public uint Timer;
		public SCVMRegister Register;

		public void Dispose()
		{
			Register.Dispose();
		}
	}
}
