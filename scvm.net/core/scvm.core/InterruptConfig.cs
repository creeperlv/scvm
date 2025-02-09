using System.Runtime.InteropServices;

namespace scvm.core
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct InterruptConfig
	{
		public byte ReturnValueRegister;
		public byte ReturnValueLength;
		public ulong PC;
		//Where to store register.
		public ulong RegisterStore;
		//Where to store machine state
		public ulong MStat;
		public ulong PT;
	}
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct FullInterruptConfig
	{
		public bool IsConfigured;
		public InterruptConfig config;
		public bool IsInjected;
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
