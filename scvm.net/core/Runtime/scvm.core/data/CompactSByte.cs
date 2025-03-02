﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace scvm.core.data
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactSByte : INumbericData<CompactSByte>
	{
		public sbyte Value;
		public CompactSByte(sbyte value) { Value = value; }
		public CompactSByte(int value) { Value = (sbyte)value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Add(CompactSByte R)
		{
			return new CompactSByte(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Sub(CompactSByte R)
		{
			return new CompactSByte(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Mul(CompactSByte R)
		{
			return new CompactSByte(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Div(CompactSByte R)
		{
			return new CompactSByte(Value / R.Value);
		}
		public SCVMSimpleResult<CompactSByte> AddOF(CompactSByte R)
		{
			CompactSByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSByte(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSByte(Value + R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactSByte>(IsOF, result);
		}

		public SCVMSimpleResult<CompactSByte> SubOF(CompactSByte R)
		{
			CompactSByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSByte(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSByte(Value - R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactSByte>(IsOF, result);
		}

		public SCVMSimpleResult<CompactSByte> DivOF(CompactSByte R)
		{
			CompactSByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSByte(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSByte(Value / R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactSByte>(IsOF, result);
		}

		public SCVMSimpleResult<CompactSByte> MulOF(CompactSByte R)
		{
			CompactSByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSByte(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSByte(Value * R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactSByte>(IsOF, result);
		}

		public bool LT(CompactSByte R)
		{
			return this.Value < R.Value;
		}

		public bool GT(CompactSByte R)
		{
			return this.Value > R.Value;
		}

		public bool LE(CompactSByte R)
		{
			return this.Value <= R.Value;
		}

		public bool GE(CompactSByte R)
		{
			return this.Value >= R.Value;
		}

		public bool EQ(CompactSByte R)
		{
			return this.Value == R.Value;
		}

		public bool NE(CompactSByte R)
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
			((sbyte*)targetPtr)[0] = this.Value;
		}
		public int SizeOf()
		{
			return sizeof(sbyte);
		}
	}

}
