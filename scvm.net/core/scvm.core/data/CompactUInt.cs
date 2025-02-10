using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace scvm.core.data
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactUInt : INumbericData<CompactUInt>
	{
		public uint Value;
		public CompactUInt(uint value) { Value = value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUInt Add(CompactUInt R)
		{
			return new CompactUInt(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUInt Sub(CompactUInt R)
		{
			return new CompactUInt(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUInt Mul(CompactUInt R)
		{
			return new CompactUInt(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUInt Div(CompactUInt R)
		{
			return new CompactUInt(Value / R.Value);
		}
		public SCVMSimpleResult<CompactUInt> AddOF(CompactUInt R)
		{
			CompactUInt result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUInt(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUInt(Value + R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactUInt>(IsOF, result);
		}

		public SCVMSimpleResult<CompactUInt> SubOF(CompactUInt R)
		{
			CompactUInt result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUInt(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUInt(Value - R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactUInt>(IsOF, result);
		}

		public SCVMSimpleResult<CompactUInt> DivOF(CompactUInt R)
		{
			CompactUInt result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUInt(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUInt(Value / R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactUInt>(IsOF, result);
		}

		public SCVMSimpleResult<CompactUInt> MulOF(CompactUInt R)
		{
			CompactUInt result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUInt(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUInt(Value * R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactUInt>(IsOF, result);
		}

		public bool LT(CompactUInt R)
		{
			return this.Value < R.Value;
		}

		public bool GT(CompactUInt R)
		{
			return this.Value > R.Value;
		}

		public bool LE(CompactUInt R)
		{
			return this.Value <= R.Value;
		}

		public bool GE(CompactUInt R)
		{
			return this.Value >= R.Value;
		}

		public bool EQ(CompactUInt R)
		{
			return this.Value == R.Value;
		}

		public bool NE(CompactUInt R)
		{
			return this.Value != R.Value;
		}
	}

}
