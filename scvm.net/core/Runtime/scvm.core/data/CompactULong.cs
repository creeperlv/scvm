﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace scvm.core.data
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactULong : INumbericData<CompactULong>
	{
		public ulong Value;
		public CompactULong(ulong value) { Value = value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactULong Add(CompactULong R)
		{
			return new CompactULong(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactULong Sub(CompactULong R)
		{
			return new CompactULong(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactULong Mul(CompactULong R)
		{
			return new CompactULong(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactULong Div(CompactULong R)
		{
			return new CompactULong(Value / R.Value);
		}
		public SCVMSimpleResult<CompactULong> AddOF(CompactULong R)
		{
			CompactULong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactULong(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactULong(Value + R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactULong>(IsOF, result);
		}

		public SCVMSimpleResult<CompactULong> SubOF(CompactULong R)
		{
			CompactULong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactULong(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactULong(Value - R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactULong>(IsOF, result);
		}

		public SCVMSimpleResult<CompactULong> DivOF(CompactULong R)
		{
			CompactULong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactULong(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactULong(Value / R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactULong>(IsOF, result);
		}

		public SCVMSimpleResult<CompactULong> MulOF(CompactULong R)
		{
			CompactULong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactULong(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactULong(Value * R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactULong>(IsOF, result);
		}

		public bool LT(CompactULong R)
		{
			return this.Value < R.Value;
		}

		public bool GT(CompactULong R)
		{
			return this.Value > R.Value;
		}

		public bool LE(CompactULong R)
		{
			return this.Value <= R.Value;
		}

		public bool GE(CompactULong R)
		{
			return this.Value >= R.Value;
		}

		public bool EQ(CompactULong R)
		{
			return this.Value == R.Value;
		}

		public bool NE(CompactULong R)
		{
			return this.Value != R.Value;
		}
		public INumbericData<CompactByte> Cast_Byte()
		{
			return new CompactByte((byte)Value);
		}

		public INumbericData<CompactSByte> Cast_SByte()
		{
			return new CompactSByte((sbyte)Value);
		}

		public INumbericData<CompactShort> Cast_Short()
		{
			return new CompactShort((short)Value);
		}

		public INumbericData<CompactUShort> Cast_UShort()
		{
			return new CompactUShort((ushort)Value);
		}

		public INumbericData<CompactInt> Cast_Int()
		{
			return new CompactInt((int)Value);
		}

		public INumbericData<CompactUInt> Cast_UInt()
		{
			return new CompactUInt((uint)Value);
		}

		public INumbericData<CompactLong> Cast_Long()
		{
			return new CompactLong((long)Value);
		}

		public INumbericData<CompactULong> Cast_ULong()
		{
			return new CompactULong((ulong)Value);
		}

		public INumbericData<CompactDouble> Cast_Double()
		{
			return new CompactDouble((double)Value);
		}

		public INumbericData<CompactSingle> Cast_Float()
		{
			return new CompactSingle((float)Value);
		}
		public unsafe void Write(byte* targetPtr)
		{
			((ulong*)targetPtr)[0] = this.Value;
		}
		public int SizeOf()
		{
			return sizeof(ulong);
		}
	}

}
