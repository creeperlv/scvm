using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace scvm.core.data
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactSingle : INumbericData<CompactSingle>
	{
		public float Value;
		public CompactSingle(float value) { Value = value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSingle Add(CompactSingle R)
		{
			return new CompactSingle(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSingle Sub(CompactSingle R)
		{
			return new CompactSingle(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSingle Mul(CompactSingle R)
		{
			return new CompactSingle(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSingle Div(CompactSingle R)
		{
			return new CompactSingle(Value / R.Value);
		}
		public SCVMSimpleResult<CompactSingle> AddOF(CompactSingle R)
		{
			CompactSingle result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSingle(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSingle(Value + R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactSingle>(IsOF, result);
		}

		public SCVMSimpleResult<CompactSingle> SubOF(CompactSingle R)
		{
			CompactSingle result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSingle(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSingle(Value - R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactSingle>(IsOF, result);
		}

		public SCVMSimpleResult<CompactSingle> DivOF(CompactSingle R)
		{
			CompactSingle result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSingle(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSingle(Value / R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactSingle>(IsOF, result);
		}

		public SCVMSimpleResult<CompactSingle> MulOF(CompactSingle R)
		{
			CompactSingle result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSingle(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSingle(Value * R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactSingle>(IsOF, result);
		}

		public bool LT(CompactSingle R)
		{
			return this.Value < R.Value;
		}

		public bool GT(CompactSingle R)
		{
			return this.Value > R.Value;
		}

		public bool LE(CompactSingle R)
		{
			return this.Value <= R.Value;
		}

		public bool GE(CompactSingle R)
		{
			return this.Value >= R.Value;
		}

		public bool EQ(CompactSingle R)
		{
			return this.Value == R.Value;
		}

		public bool NE(CompactSingle R)
		{
			return this.Value != R.Value;
		}
	}

}
