using LibCLCC.NET.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace scvm.tools.compiler.core
{
	public class Compiler
	{
		public unsafe OperationResult<CompilationObject> Assemble(Stream stream, string? FileFolder, string FileName, OperationResult<CompilationObject>? previousCompile)
		{
			OperationResult<CompilationObject> result = previousCompile ?? new CompilationObject();

			StreamReader streamReader = new StreamReader(stream);
			SCVMCompilerScanner scanner = new SCVMCompilerScanner();

			return result;	
		}
	}
}
