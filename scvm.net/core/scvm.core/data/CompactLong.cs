using System.Runtime.CompilerServices;
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
	}

}
