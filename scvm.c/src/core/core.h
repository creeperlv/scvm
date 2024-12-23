#ifndef __SCVM_CORE_CORE__
#define __SCVM_CORE_CORE__
#include "Inst.h"
#include <stdint.h>

typedef int64_t Instruction;
typedef uint8_t byte;
typedef uint8_t *(*MMUGetPtr)(uint64_t ptr, uint64_t PageTable, int CallerProcessor, int AssumedSize);
typedef struct __mmu
{
	MMUGetPtr GetPtr;
} SCVMMMU;
typedef struct __machine
{
	struct __cpu *CPU;
	SCVMMMU MMU;
} SCVMMachine;

typedef struct __processor
{
	struct __cpu *ParentCPU;
} SCVMProcessor;

typedef struct __cpu
{
	SCVMProcessor *Processors;
	int ProcessorCount;
} SCVMCPU;
void InitCPU(SCVMCPU *cpu, int ProcessorCount);
#endif
