using System.Runtime.InteropServices;

namespace scvm.core
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct InterruptConfig
	{
		public byte ReturnValueRegister;
		public byte ReturnValueLength;
		public long PC;
		public long RegisterStore;
		public long MStat;
		public long PT;
	}
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct FullInterruptConfig
	{
		public bool IsConfigured;
		public InterruptConfig config;
		public bool IsInjected;
		public int FuncID;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct BoolByte
	{
		public byte Value;
		public bool this[int index]
		{
			get => ((Value >> index) & 1) == 1;
			set => Value = (byte)(value ? (Value | (1 << index)) : (((Value | (1 << index)) ^ (1 << index))));
		}
	}
}
