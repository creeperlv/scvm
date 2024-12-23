#include "cobj.h"
#include <stdio.h>
#include <stdlib.h>

void SCVMInitCompileObject(SCVMCompileObject *object)
{
	object->Symbols = malloc(sizeof(SCVMObjSymbol) * SCOBJ_SYMBOL_ALLOC_BLOCK);
	object->SymbolLength = SCOBJ_SYMBOL_ALLOC_BLOCK;
	object->SymbolCount = 0;
	object->InstructionPtr = malloc(sizeof(Instruction) * SCOBJ_INST_ALLOC_BLOCK);
	object->InstructionCount = 0;
	object->InstructionLength = SCOBJ_INST_ALLOC_BLOCK;
}
void SCVMAppendInstruction(SCVMCompileObject *object, Instruction inst)
{
	if (object->InstructionCount >= object->InstructionLength)
	{
		object->InstructionPtr =
			realloc(object->InstructionPtr, sizeof(Instruction) * (object->InstructionLength + SCOBJ_INST_ALLOC_BLOCK));
		object->InstructionLength += SCOBJ_INST_ALLOC_BLOCK;
	}
	object->InstructionPtr[object->InstructionCount] = inst;
	object->InstructionCount++;
}
