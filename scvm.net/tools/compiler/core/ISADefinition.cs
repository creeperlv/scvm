﻿using scvm.core;
using System.Collections.Generic;

namespace scvm.tools.compiler.core
{
	public class ISADefinition
	{
		public Dictionary<string, ProgramSegment> SegmentNames = new Dictionary<string, ProgramSegment>();
		public Dictionary<string, LabelVisibility> LabelVisibilityNames = new Dictionary<string, LabelVisibility>();
		public Dictionary<string, ushort> InstructionIDs = new Dictionary<string, ushort>();
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
				{"setmstat", SCVMInst.SETMSTAT },
				{"set.mstat", SCVMInst.SETMSTAT },
				{"mstat.set", SCVMInst.SETMSTAT },
				{"mstat.get", SCVMInst.GETMSTAT },
				{"getmstat", SCVMInst.GETMSTAT },
				{"get.mstat", SCVMInst.GETMSTAT },

			}
		};
	}
	public enum ProgramSegment
	{
		Code, Data,Definition
	}
	public enum LabelVisibility
	{
		Default, Global
	}
}
