#include "core.h"
#include <stdlib.h>
void InitCPU(SCVMCPU *cpu, int ProcessorCount)
{
	cpu->ProcessorCount = ProcessorCount;
	cpu->Processors = (SCVMProcessor *)malloc(sizeof(SCVMProcessor) * ProcessorCount);
	for (int i = 0; i < ProcessorCount; i++)
	{
		cpu->Processors[i].ParentCPU = cpu;
	}
}
