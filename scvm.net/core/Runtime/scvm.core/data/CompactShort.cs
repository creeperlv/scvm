﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace scvm.core.data
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactShort : INumbericData<CompactShort>
	{
		public short Value;
		public CompactShort(short value) { Value = value; }
		public CompactShort(int value) { Value = (short)value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactShort Add(CompactShort R)
		{
			return new CompactShort(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactShort Sub(CompactShort R)
		{
			return new CompactShort(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactShort Mul(CompactShort R)
		{
			return new CompactShort(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactShort Div(CompactShort R)
		{
			return new CompactShort(Value / R.Value);
		}
		public SCVMSimpleResult<CompactShort> AddOF(CompactShort R)
		{
			CompactShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactShort(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactShort(Value + R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactShort>(IsOF, result);
		}

		public SCVMSimpleResult<CompactShort> SubOF(CompactShort R)
		{
			CompactShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactShort(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactShort(Value - R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactShort>(IsOF, result);
		}

		public SCVMSimpleResult<CompactShort> DivOF(CompactShort R)
		{
			CompactShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactShort(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactShort(Value / R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactShort>(IsOF, result);
		}

		public SCVMSimpleResult<CompactShort> MulOF(CompactShort R)
		{
			CompactShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactShort(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactShort(Value * R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactShort>(IsOF, result);
		}

		public bool LT(CompactShort R)
		{
			return this.Value < R.Value;
		}

		public bool GT(CompactShort R)
		{
			return this.Value > R.Value;
		}

		public bool LE(CompactShort R)
		{
			return this.Value <= R.Value;
		}

		public bool GE(CompactShort R)
		{
			return this.Value >= R.Value;
		}

		public bool EQ(CompactShort R)
		{
			return this.Value == R.Value;
		}

		public bool NE(CompactShort R)
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
			((short*)targetPtr)[0] = this.Value;
		}
		public int SizeOf()
		{
			return sizeof(short);
		}
	}
}
