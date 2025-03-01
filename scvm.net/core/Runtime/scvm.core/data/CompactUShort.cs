﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace scvm.core.data
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactUShort : INumbericData<CompactUShort>
	{
		public ushort Value;
		public CompactUShort(ushort value) { Value = value; }
		public CompactUShort(int value) { Value = (ushort)value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUShort Add(CompactUShort R)
		{
			return new CompactUShort(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUShort Sub(CompactUShort R)
		{
			return new CompactUShort(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUShort Mul(CompactUShort R)
		{
			return new CompactUShort(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUShort Div(CompactUShort R)
		{
			return new CompactUShort(Value / R.Value);
		}
		public SCVMSimpleResult<CompactUShort> AddOF(CompactUShort R)
		{
			CompactUShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUShort(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUShort(Value + R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactUShort>(IsOF, result);
		}

		public SCVMSimpleResult<CompactUShort> SubOF(CompactUShort R)
		{
			CompactUShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUShort(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUShort(Value - R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactUShort>(IsOF, result);
		}

		public SCVMSimpleResult<CompactUShort> DivOF(CompactUShort R)
		{
			CompactUShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUShort(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUShort(Value / R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactUShort>(IsOF, result);
		}

		public SCVMSimpleResult<CompactUShort> MulOF(CompactUShort R)
		{
			CompactUShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUShort(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUShort(Value * R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactUShort>(IsOF, result);
		}

		public bool LT(CompactUShort R)
		{
			return this.Value < R.Value;
		}

		public bool GT(CompactUShort R)
		{
			return this.Value > R.Value;
		}

		public bool LE(CompactUShort R)
		{
			return this.Value <= R.Value;
		}

		public bool GE(CompactUShort R)
		{
			return this.Value >= R.Value;
		}

		public bool EQ(CompactUShort R)
		{
			return this.Value == R.Value;
		}

		public bool NE(CompactUShort R)
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
