using System.Runtime.CompilerServices;
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
	}

}
