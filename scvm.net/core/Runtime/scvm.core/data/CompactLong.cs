﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace scvm.core.data
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactLong : INumbericData<CompactLong>
	{
		public long Value;
		public CompactLong(long value) { Value = value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactLong Add(CompactLong R)
		{
			return new CompactLong(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactLong Sub(CompactLong R)
		{
			return new CompactLong(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactLong Mul(CompactLong R)
		{
			return new CompactLong(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactLong Div(CompactLong R)
		{
			return new CompactLong(Value / R.Value);
		}
		public SCVMSimpleResult<CompactLong> AddOF(CompactLong R)
		{
			CompactLong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactLong(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactLong(Value + R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactLong>(IsOF, result);
		}

		public SCVMSimpleResult<CompactLong> SubOF(CompactLong R)
		{
			CompactLong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactLong(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactLong(Value - R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactLong>(IsOF, result);
		}

		public SCVMSimpleResult<CompactLong> DivOF(CompactLong R)
		{
			CompactLong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactLong(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactLong(Value / R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactLong>(IsOF, result);
		}

		public SCVMSimpleResult<CompactLong> MulOF(CompactLong R)
		{
			CompactLong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactLong(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactLong(Value * R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactLong>(IsOF, result);
		}

		public bool LT(CompactLong R)
		{
			return this.Value < R.Value;
		}

		public bool GT(CompactLong R)
		{
			return this.Value > R.Value;
		}

		public bool LE(CompactLong R)
		{
			return this.Value <= R.Value;
		}

		public bool GE(CompactLong R)
		{
			return this.Value >= R.Value;
		}

		public bool EQ(CompactLong R)
		{
			return this.Value == R.Value;
		}

		public bool NE(CompactLong R)
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
	}

}
