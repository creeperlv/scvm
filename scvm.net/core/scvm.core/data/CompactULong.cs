using System.Runtime.CompilerServices;
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
	}

}
