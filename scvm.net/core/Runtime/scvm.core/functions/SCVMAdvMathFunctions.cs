using System;
using System.Runtime.CompilerServices;

namespace scvm.core.functions
{
	public static class SCVMAdvMathFunctions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Scalc(SCVMRegister context, Instruction_OpSeparated inst)
		{
			SCVMScalcFunction scalc = (SCVMScalcFunction)inst.D2;
			double d = context.GetData<double>(inst.D0);
			switch (scalc)
			{
				case SCVMScalcFunction.sin:
					d = Math.Sin(d);
					break;
				case SCVMScalcFunction.cos:
					d = Math.Cos(d);
					break;
				case SCVMScalcFunction.tan:
					d = Math.Tan(d);
					break;
				case SCVMScalcFunction.asin:
					d = Math.Asin(d);
					break;
				case SCVMScalcFunction.acos:
					d = Math.Acos(d);
					break;
				case SCVMScalcFunction.atan:
					d = Math.Atan(d);
					break;
				case SCVMScalcFunction.sinh:
					d = Math.Sinh(d);
					break;
				case SCVMScalcFunction.cosh:
					d = Math.Cosh(d);
					break;
				case SCVMScalcFunction.log:
					d = Math.Log(d);
					break;
				case SCVMScalcFunction.log10:
					d = Math.Log10(d);
					break;
				case SCVMScalcFunction.exp:
					d = Math.Exp(d);
					break;
				case SCVMScalcFunction.round:
					d = Math.Round(d);
					break;
				default:
					break;
			}
			context.SetData(inst.D1, d);
		}
	}
	public enum SCVMScalcFunction : uint
	{
		sin,
		cos,
		tan,
		asin,
		acos,
		atan,
		sinh,
		cosh,
		log,
		log10,
		exp,
		round,
	}
}
