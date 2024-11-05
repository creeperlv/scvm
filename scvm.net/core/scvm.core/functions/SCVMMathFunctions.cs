using scvm.core.data;
using scvm.core.utilities;
using System.Runtime.CompilerServices;

namespace scvm.core.functions
{
	public unsafe static class SCVMMathFunctions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void MathAdd(SCVMRegister context, Instruction inst)
		{
			var type = inst.CastAs<Instruction, NativeType>(2);
			var ofCheck = inst.CastAs<Instruction, byte>(3);
			bool Checkof = ofCheck == 1;
			var L = inst.CastAs<Instruction, byte>(4);
			var R = inst.CastAs<Instruction, byte>(5);
			var T = inst.CastAs<Instruction, byte>(6);

			switch (type)
			{
				case NativeType.BU:
					GenericAdd<CompactByte>(context, L, R, T, Checkof);
					break;
				case NativeType.BS:
					GenericAdd<CompactSByte>(context, L, R, T, Checkof);
					break;
				case NativeType.S:
					GenericAdd<CompactShort>(context, L, R, T, Checkof);
					break;
				case NativeType.SU:
					GenericAdd<CompactUShort>(context, L, R, T, Checkof);
					break;
				case NativeType.I:
					GenericAdd<CompactInt>(context, L, R, T, Checkof);
					break;
				case NativeType.IU:
					GenericAdd<CompactUInt>(context, L, R, T, Checkof);
					break;
				case NativeType.L:
					GenericAdd<CompactLong>(context, L, R, T, Checkof);
					break;
				case NativeType.LU:
					GenericAdd<CompactULong>(context, L, R, T, Checkof);
					break;
				case NativeType.F:
					GenericAdd<CompactSingle>(context, L, R, T, Checkof);
					break;
				case NativeType.D:
					GenericAdd<CompactDouble>(context, L, R, T, Checkof);
					break;
			}
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void MathSub(SCVMRegister context, Instruction inst)
		{
			var type = inst.CastAs<Instruction, NativeType>(2);
			var ofCheck = inst.CastAs<Instruction, byte>(3);
			bool Checkof = ofCheck == 1;
			var L = inst.CastAs<Instruction, byte>(4);
			var R = inst.CastAs<Instruction, byte>(5);
			var T = inst.CastAs<Instruction, byte>(6);

			switch (type)
			{
				case NativeType.BU:
					GenericSub<CompactByte>(context, L, R, T, Checkof);
					break;
				case NativeType.BS:
					GenericSub<CompactSByte>(context, L, R, T, Checkof);
					break;
				case NativeType.S:
					GenericSub<CompactShort>(context, L, R, T, Checkof);
					break;
				case NativeType.SU:
					GenericSub<CompactUShort>(context, L, R, T, Checkof);
					break;
				case NativeType.I:
					GenericSub<CompactInt>(context, L, R, T, Checkof);
					break;
				case NativeType.IU:
					GenericSub<CompactUInt>(context, L, R, T, Checkof);
					break;
				case NativeType.L:
					GenericSub<CompactLong>(context, L, R, T, Checkof);
					break;
				case NativeType.LU:
					GenericSub<CompactULong>(context, L, R, T, Checkof);
					break;
				case NativeType.F:
					GenericSub<CompactSingle>(context, L, R, T, Checkof);
					break;
				case NativeType.D:
					GenericSub<CompactDouble>(context, L, R, T, Checkof);
					break;
			}
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void MathMul(SCVMRegister context, Instruction inst)
		{
			var type = inst.CastAs<Instruction, NativeType>(2);
			var ofCheck = inst.CastAs<Instruction, byte>(3);
			bool Checkof = ofCheck == 1;
			var L = inst.CastAs<Instruction, byte>(4);
			var R = inst.CastAs<Instruction, byte>(5);
			var T = inst.CastAs<Instruction, byte>(6);

			switch (type)
			{
				case NativeType.BU:
					GenericMul<CompactByte>(context, L, R, T, Checkof);
					break;
				case NativeType.BS:
					GenericMul<CompactSByte>(context, L, R, T, Checkof);
					break;
				case NativeType.S:
					GenericMul<CompactShort>(context, L, R, T, Checkof);
					break;
				case NativeType.SU:
					GenericMul<CompactUShort>(context, L, R, T, Checkof);
					break;
				case NativeType.I:
					GenericMul<CompactInt>(context, L, R, T, Checkof);
					break;
				case NativeType.IU:
					GenericMul<CompactUInt>(context, L, R, T, Checkof);
					break;
				case NativeType.L:
					GenericMul<CompactLong>(context, L, R, T, Checkof);
					break;
				case NativeType.LU:
					GenericMul<CompactULong>(context, L, R, T, Checkof);
					break;
				case NativeType.F:
					GenericMul<CompactSingle>(context, L, R, T, Checkof);
					break;
				case NativeType.D:
					GenericMul<CompactDouble>(context, L, R, T, Checkof);
					break;
			}
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void MathDiv(SCVMRegister context, Instruction inst)
		{
			var type = inst.CastAs<Instruction, NativeType>(2);
			var ofCheck = inst.CastAs<Instruction, byte>(3);
			bool Checkof = ofCheck == 1;
			var L = inst.CastAs<Instruction, byte>(4);
			var R = inst.CastAs<Instruction, byte>(5);
			var T = inst.CastAs<Instruction, byte>(6);

			switch (type)
			{
				case NativeType.BU:
					GenericDiv<CompactByte>(context, L, R, T, Checkof);
					break;
				case NativeType.BS:
					GenericDiv<CompactSByte>(context, L, R, T, Checkof);
					break;
				case NativeType.S:
					GenericDiv<CompactShort>(context, L, R, T, Checkof);
					break;
				case NativeType.SU:
					GenericDiv<CompactUShort>(context, L, R, T, Checkof);
					break;
				case NativeType.I:
					GenericDiv<CompactInt>(context, L, R, T, Checkof);
					break;
				case NativeType.IU:
					GenericDiv<CompactUInt>(context, L, R, T, Checkof);
					break;
				case NativeType.L:
					GenericDiv<CompactLong>(context, L, R, T, Checkof);
					break;
				case NativeType.LU:
					GenericDiv<CompactULong>(context, L, R, T, Checkof);
					break;
				case NativeType.F:
					GenericDiv<CompactSingle>(context, L, R, T, Checkof);
					break;
				case NativeType.D:
					GenericDiv<CompactDouble>(context, L, R, T, Checkof);
					break;
			}
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void GenericAdd<N>(SCVMRegister Register, int L, int R, int T, bool CheckOF) where N : unmanaged, INumbericData<N>
		{
			var LN = Register.GetData<N>(L);
			var RN = Register.GetData<N>(R);
			N TN = default;
			if (CheckOF)
			{
				var result = LN.AddOF(RN);
				TN = result.Value;
				Register.Flags.SetOverflow(result.IsSuccess);
			}
			else
			{
				TN = LN.Add(RN);
			}
			Register.SetData(T, TN);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void GenericSub<N>(SCVMRegister Register, int L, int R, int T, bool CheckOF) where N : unmanaged, INumbericData<N>
		{
			var LN = Register.GetData<N>(L);
			var RN = Register.GetData<N>(R);
			N TN = default;
			if (CheckOF)
			{
				var result = LN.SubOF(RN);
				TN = result.Value;
				Register.Flags.SetOverflow(result.IsSuccess);
			}
			else
			{
				TN = LN.Sub(RN);
			}
			Register.SetData<N>(T, TN);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void GenericMul<N>(SCVMRegister Register, int L, int R, int T, bool CheckOF) where N : unmanaged, INumbericData<N>
		{
			var LN = Register.GetData<N>(L);
			var RN = Register.GetData<N>(R);
			N TN = default;
			if (CheckOF)
			{
				var result = LN.MulOF(RN);
				TN = result.Value;
				Register.Flags.SetOverflow(result.IsSuccess);
			}
			else
			{
				TN = LN.Mul(RN);
			}
			Register.SetData<N>(T, TN);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void GenericDiv<N>(SCVMRegister Register, int L, int R, int T, bool CheckOF) where N : unmanaged, INumbericData<N>
		{
			var LN = Register.GetData<N>(L);
			var RN = Register.GetData<N>(R);
			N TN = default;
			if (CheckOF)
			{
				var result = LN.DivOF(RN);
				TN = result.Value;
				Register.Flags.SetOverflow(result.IsSuccess);
			}
			else
			{
				TN = LN.Div(RN);
			}
			Register.SetData<N>(T, TN);
		}
	}
}
