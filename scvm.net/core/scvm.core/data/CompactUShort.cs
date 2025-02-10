using System.Runtime.CompilerServices;
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
	}

}
