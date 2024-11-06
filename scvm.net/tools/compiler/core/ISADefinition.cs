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
			},
			LabelVisibilityNames = new Dictionary<string, LabelVisibility>()
			{
				{"default", LabelVisibility.Default},
				{"global", LabelVisibility.Global},
			}
		};
	}
	public enum ProgramSegment
	{
		Code, Data
	}
	public enum LabelVisibility
	{
		Default, Global
	}
}
