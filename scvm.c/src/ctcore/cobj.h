#ifndef __SCVM_COMPILE_OBJECT__
#define __SCVM_COMPILE_OBJECT__
#include "../core/core.h"

#define SCOBJ_VERSION 1

#define SCOBJ_SYMBOL_ALLOC_BLOCK 4

#define SCOBJ_INST_ALLOC_BLOCK 4

typedef struct _Symbol
{
	int ID;
	char *Content;
	int ContentLen;
} SCVMObjSymbol;
typedef struct __cobj
{
	SCVMObjSymbol *Symbols;
	int SymbolCount;
	int SymbolLength;
	Instruction *InstructionPtr;
	int InstructionCount;
	int InstructionLength;
} SCVMCompileObject;

void SCVMInitCompileObject(SCVMCompileObject *object);
void SCVMAppendInstruction(SCVMCompileObject *object, Instruction inst);

#endif
