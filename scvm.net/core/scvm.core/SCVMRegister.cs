using System;
using System.Runtime.CompilerServices;
using static scvm.core.libNative.stdlib;
namespace scvm.core
{
	public unsafe struct SCVMRegister : IDisposable
	{
		public const int RegisterCount = 0xFF;
		public const int RegisterSize = 0x200;
		public SCVMProcessorFlag Flags;
		public byte* Registers;
		public void InitRegisters()
		{
			Registers = malloc<byte>(RegisterSize / 8);
		}

		public uint RegisterLimit;
		public void Dispose()
		{
			free(Registers);
		}

		public T GetData<T>(int offset) where T : unmanaged
		{
			if (offset + sizeof(T) > RegisterLimit) return default;
			var ptr = Registers + offset;
			return ((T*)ptr)[0];
		}
		public void SetData<T>(int offset, T d) where T : unmanaged
		{
			if (offset + sizeof(T) > RegisterLimit) return;
			((T*)(Registers + offset))[0] = d;
		}
	}
	public unsafe struct SCVMProcessorFlag
	{
		public uint value;
		public static readonly uint OFFlag = 1;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsOverflow()
		{
			return (value & OFFlag) == OFFlag;
		}
		public void SetOverflow(bool overflow)
		{
			if (overflow)
			{
				value &= (uint.MaxValue ^ OFFlag);
			}
			else
			{
				value |= OFFlag;
			}

		}
	}
}
