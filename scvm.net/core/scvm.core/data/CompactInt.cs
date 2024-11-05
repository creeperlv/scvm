using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace scvm.core.data
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactInt : INumbericData<CompactInt>
	{
		public int Value;
		public CompactInt(int value) { Value = value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactInt Add(CompactInt R)
		{
			return new CompactInt(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactInt Sub(CompactInt R)
		{
			return new CompactInt(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactInt Mul(CompactInt R)
		{
			return new CompactInt(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactInt Div(CompactInt R)
		{
			return new CompactInt(Value / R.Value);
		}

		public SCVMSimpleResult<CompactInt> AddOF(CompactInt R)
		{
			CompactInt result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactInt(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactInt(Value + R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactInt>(IsOF, result);
		}

		public SCVMSimpleResult<CompactInt> SubOF(CompactInt R)
		{
			CompactInt result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactInt(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactInt(Value - R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactInt>(IsOF, result);
		}

		public SCVMSimpleResult<CompactInt> DivOF(CompactInt R)
		{
			CompactInt result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactInt(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactInt(Value / R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactInt>(IsOF, result);
		}

		public SCVMSimpleResult<CompactInt> MulOF(CompactInt R)
		{
			CompactInt result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactInt(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactInt(Value * R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactInt>(IsOF, result);
		}
	}

}
