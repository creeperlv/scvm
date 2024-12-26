using scvm.core;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace scvm.tools.compiler.core
{
	public class ISADefinition
	{
		public Dictionary<string, ProgramSegment> SegmentNames = new Dictionary<string, ProgramSegment>();
		public Dictionary<string, LabelVisibility> LabelVisibilityNames = new Dictionary<string, LabelVisibility>();
		public Dictionary<string, ushort> InstructionIDs = new Dictionary<string, ushort>();
		public Dictionary<string, string> PredefinedSymbols = new Dictionary<string, string>();

		public Dictionary<string, NativeType> NativeTypes = new Dictionary<string, NativeType>();
		public Dictionary<string, byte> RegisterNames = new Dictionary<string, byte>();
		public string TranslateSymbol(string symbol)
		{
			if (PredefinedSymbols.ContainsKey(symbol)) return PredefinedSymbols[symbol];
			return symbol;
		}
	}
	public class DefaultISADefinition
	{
		public static ISADefinition Default = new ISADefinition()
		{
			SegmentNames = new Dictionary<string, ProgramSegment>()
			{
				{".code", ProgramSegment.Code },
				{".data", ProgramSegment.Data},
				{".def", ProgramSegment.Definition},
				{".definition", ProgramSegment.Definition},
			},
			LabelVisibilityNames = new Dictionary<string, LabelVisibility>()
			{
				{"default", LabelVisibility.Default},
				{"global", LabelVisibility.Global},
			},
			InstructionIDs = new Dictionary<string, ushort>()
			{
				{"add", SCVMInst.ADD },
				{"sub", SCVMInst.SUB },
				{"mul", SCVMInst.MUL },
				{"div", SCVMInst.DIV },
				{"ofc_add", SCVMInst.OFC_ADD },
				{"ofc_sub", SCVMInst.OFC_SUB },
				{"ofc_mul", SCVMInst.OFC_MUL },
				{"ofc_div", SCVMInst.OFC_DIV },
				{"ofc.add", SCVMInst.OFC_ADD },
				{"ofc.sub", SCVMInst.OFC_SUB },
				{"ofc.mul", SCVMInst.OFC_MUL },
				{"ofc.div", SCVMInst.OFC_DIV },
				{"add.ofc", SCVMInst.OFC_ADD },
				{"sub.ofc", SCVMInst.OFC_SUB },
				{"mul.ofc", SCVMInst.OFC_MUL },
				{"div.ofc", SCVMInst.OFC_DIV },

				{"set", SCVMInst.SET },
				{"cvt", SCVMInst.CVT },
				{"scalc", SCVMInst.SCALC },
				{"dcalc", SCVMInst.DCALC },
				{"sr", SCVMInst.SR },
				{"lr", SCVMInst.LR },
				{"sh", SCVMInst.SH },
				{"lg", SCVMInst.LG },
				{"jmp", SCVMInst.JMP },
				{"jf", SCVMInst.JF },
				{"jff", SCVMInst.JFF },
				{"cmp", SCVMInst.CMP },
				{"cp", SCVMInst.CP },
				{"syscall", SCVMInst.SYSCALL },
				{"int", SCVMInst.SYSCALL },
				{"rf", SCVMInst.RF },
				{"setmstat", SCVMInst.SYSREGW },
				{"set.mstat", SCVMInst.SYSREGW },
				{"mstat.set", SCVMInst.SYSREGW },
				{"mstat.get", SCVMInst.SYSREGR },
				{"getmstat", SCVMInst.SYSREGR },
				{"get.mstat", SCVMInst.SYSREGR },

			},

			NativeTypes = new Dictionary<string, NativeType>() {
				{"byte",NativeType.BU },
				{"sbyte",NativeType.BS},
				{"int16",NativeType.S},
				{"short",NativeType.S},
				{"uint16",NativeType.SU},
				{"ushort",NativeType.SU},
				{"int64",NativeType.L},
				{"long",NativeType.L},
				{"uint64",NativeType.LU},
				{"ulong",NativeType.LU},
				{"float",NativeType.F},
				{"single",NativeType.F},
				{"double",NativeType.D},
				{"int",NativeType.I},
				{"int32",NativeType.I},
				{"uint",NativeType.IU},
				{"uint32",NativeType.IU},
				{"register",NativeType.R},
			},
		};

	}
	public enum ProgramSegment
	{
		Code, Data, Definition
	}
	public enum LabelVisibility
	{
		Default, Global
	}
}
