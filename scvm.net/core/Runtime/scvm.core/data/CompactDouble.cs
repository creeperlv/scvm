﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace scvm.core.data
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactDouble : INumbericData<CompactDouble>
	{
		public double Value;
		public CompactDouble(double value) { Value = value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactDouble Add(CompactDouble R)
		{
			return new CompactDouble(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactDouble Sub(CompactDouble R)
		{
			return new CompactDouble(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactDouble Mul(CompactDouble R)
		{
			return new CompactDouble(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactDouble Div(CompactDouble R)
		{
			return new CompactDouble(Value / R.Value);
		}

		public SCVMSimpleResult<CompactDouble> AddOF(CompactDouble R)
		{
			CompactDouble result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactDouble(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactDouble(Value + R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactDouble>(IsOF, result);
		}

		public SCVMSimpleResult<CompactDouble> SubOF(CompactDouble R)
		{
			CompactDouble result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactDouble(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactDouble(Value - R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactDouble>(IsOF, result);
		}

		public SCVMSimpleResult<CompactDouble> DivOF(CompactDouble R)
		{
			CompactDouble result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactDouble(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactDouble(Value / R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactDouble>(IsOF, result);
		}

		public SCVMSimpleResult<CompactDouble> MulOF(CompactDouble R)
		{
			CompactDouble result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactDouble(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactDouble(Value * R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactDouble>(IsOF, result);
		}

		public bool LT(CompactDouble R)
		{
			return this.Value < R.Value;
		}

		public bool GT(CompactDouble R)
		{
			return this.Value > R.Value;
		}

		public bool LE(CompactDouble R)
		{
			return this.Value <= R.Value;
		}

		public bool GE(CompactDouble R)
		{
			return this.Value >= R.Value;
		}

		public bool EQ(CompactDouble R)
		{
			return this.Value == R.Value;
		}

		public bool NE(CompactDouble R)
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
			((double*)targetPtr)[0] = this.Value;
		}
		public int SizeOf()
		{
			return sizeof(double);
		}
	}

}
