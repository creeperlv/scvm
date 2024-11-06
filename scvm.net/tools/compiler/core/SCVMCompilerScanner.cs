using LibCLCC.NET.TextProcessing;
using System.Collections.Generic;

namespace scvm.tools.compiler.core
{
	public class SCVMCompilerScanner : GeneralPurposeScanner
	{
		public SCVMCompilerScanner()
		{
			this.PredefinedSegmentCharacters = new List<char> { ':', ',' };
			this.lineCommentIdentifiers = new List<LineCommentIdentifier>() {
				new LineCommentIdentifier { StartSequence = ";" },
				new LineCommentIdentifier { StartSequence = "#" }
			};
		}
	}
}
