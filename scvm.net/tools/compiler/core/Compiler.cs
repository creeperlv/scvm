using LibCLCC.NET.Operations;
using scvm.core;
using scvm.tools.compiler.core.CompilerFunctions;
using scvm.tools.compiler.core.Errors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace scvm.tools.compiler.core
{
	public class Compiler
	{
		public unsafe OperationResult<CompilationObject> Assemble(Stream stream, string? FileFolder, string FileName, OperationResult<CompilationObject>? previousCompile, ISADefinition CurrentDefinition)
		{
			OperationResult<CompilationObject> result = previousCompile ?? new CompilationObject();

			using StreamReader streamReader = new StreamReader(stream);
			SCVMCompilerScanner scanner = new SCVMCompilerScanner();
			int PC = 0;
			ProgramSegment segment = ProgramSegment.Code;
			while (true)
			{

				var line = streamReader.ReadLine();
				if (line == null)
				{
					break;
				}
				var HEAD = scanner.Scan(line, false, FileName);
				var Next = HEAD.Next;
				var HEAD_NAME = HEAD.content;
				if (CurrentDefinition.SegmentNames.TryGetValue(HEAD_NAME, out var value))
				{
					segment = value;
					continue;
				}
				switch (segment)
				{
					case ProgramSegment.Code:
						{

							if (CurrentDefinition.InstructionIDs.TryGetValue(HEAD_NAME, out var instID))
							{
								if (DefaultCompilerFunctionDefinition.GetDefault().CompilerFunctions.TryGetValue(instID, out var assemble))
								{

									IntermediateInstruction instruction = new IntermediateInstruction();
									if (assemble(CurrentDefinition, instID, HEAD, result, instruction, PC))
									{
										result.Result.instructions.Add(instruction);
										PC++;
									}
								}
								else
								{
									if (Next != null)
									{
										if (Next.content == ":")
										{
											result.Result.Labels.Add(HEAD_NAME, new Label()
											{
												Content = HEAD_NAME,
												Position = PC,
												visibility = LabelVisibility.Default
											});
											continue;
										}
									}
									result.AddError(new UnimplementedInstructionError(HEAD_NAME, HEAD));
								}
							}
						}
						break;
					case ProgramSegment.Data:
						break;
					case ProgramSegment.Definition:
						break;
					default:
						break;
				}
			}
			return result;
		}
	}
}
