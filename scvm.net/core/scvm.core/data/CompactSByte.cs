using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace scvm.core.data
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactSByte : INumbericData<CompactSByte>
	{
		public sbyte Value;
		public CompactSByte(sbyte value) { Value = value; }
		public CompactSByte(int value) { Value = (sbyte)value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Add(CompactSByte R)
		{
			return new CompactSByte(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Sub(CompactSByte R)
		{
			return new CompactSByte(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Mul(CompactSByte R)
		{
			return new CompactSByte(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Div(CompactSByte R)
		{
			return new CompactSByte(Value / R.Value);
		}
		public SCVMSimpleResult<CompactSByte> AddOF(CompactSByte R)
		{
			CompactSByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSByte(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSByte(Value + R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactSByte>(IsOF, result);
		}

		public SCVMSimpleResult<CompactSByte> SubOF(CompactSByte R)
		{
			CompactSByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSByte(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSByte(Value - R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactSByte>(IsOF, result);
		}

		public SCVMSimpleResult<CompactSByte> DivOF(CompactSByte R)
		{
			CompactSByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSByte(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSByte(Value / R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactSByte>(IsOF, result);
		}

		public SCVMSimpleResult<CompactSByte> MulOF(CompactSByte R)
		{
			CompactSByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSByte(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSByte(Value * R.Value);
					}
				}
			}
			return new SCVMSimpleResult<CompactSByte>(IsOF, result);
		}
	}

}
