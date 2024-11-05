using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
	}

}
