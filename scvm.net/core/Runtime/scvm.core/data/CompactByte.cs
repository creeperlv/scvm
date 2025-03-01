using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace scvm.core.data
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactByte : INumbericData<CompactByte>
	{
		public byte Value;
		public CompactByte(byte value) { Value = value; }
		public CompactByte(int value) { Value = (byte)value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactByte Add(CompactByte R)
		{
			return new CompactByte(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactByte Sub(CompactByte R)
		{
			return new CompactByte(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactByte Mul(CompactByte R)
		{
			return new CompactByte(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactByte Div(CompactByte R)
		{
			return new CompactByte(Value / R.Value);
		}
		public SCVMSimpleResult<CompactByte> AddOF(CompactByte R)
		{
			CompactByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactByte(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactByte(Value + R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactByte>(IsOF, result);
		}

		public SCVMSimpleResult<CompactByte> SubOF(CompactByte R)
		{
			CompactByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactByte(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactByte(Value - R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactByte>(IsOF, result);
		}

		public SCVMSimpleResult<CompactByte> DivOF(CompactByte R)
		{
			CompactByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactByte(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactByte(Value / R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactByte>(IsOF, result);
		}

		public SCVMSimpleResult<CompactByte> MulOF(CompactByte R)
		{
			CompactByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactByte(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactByte(Value * R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactByte>(IsOF, result);
		}

		public bool LT(CompactByte R)
		{
			return this.Value < R.Value;
		}

		public bool GT(CompactByte R)
		{
			return this.Value > R.Value;
		}

		public bool LE(CompactByte R)
		{
			return this.Value <= R.Value;
		}

		public bool GE(CompactByte R)
		{
			return this.Value >= R.Value;
		}

		public bool EQ(CompactByte R)
		{
			return this.Value == R.Value;
		}

		public bool NE(CompactByte R)
		{
			return this.Value != R.Value;
		}

		public INumbericData<CompactByte> Cast_Byte()
		{
			return new CompactByte(Value);
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
