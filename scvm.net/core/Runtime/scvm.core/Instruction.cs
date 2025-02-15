using System.Runtime.InteropServices;

namespace scvm.core
{
	public enum NativeType : byte
	{
		BS = 0, BU, S, SU, I, IU, L, LU, F, D, R
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct Instruction
	{
		public ulong data;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct Instruction_OpSeparated
	{
		public ushort op;
		public byte D0;
		public byte D1;
		public uint D2;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct Instruction_OpSysReg
	{
		public ushort op;
		public ushort D0;
		public byte D1;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct Instruction_JFOp
	{
		public ushort op;
		public byte D0;
		public int D1;
		public byte D2;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct Instruction_OpSeparated_ByteSegmented
	{
		public ushort op;
		public byte D0;
		public byte D1;
		public byte D2;
		public byte D3;
		public byte D4;
		public byte D5;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct Instruction_OpSeparated_ByteSegmented2
	{
		public ushort op;
		public byte D0;
		public byte D1;
		public byte D2;
		public byte D3;
		public byte D4;
		public byte D5;
	}
}
