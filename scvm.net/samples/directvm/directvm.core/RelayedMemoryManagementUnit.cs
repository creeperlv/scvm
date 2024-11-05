using DirectVM.core.libc;
using scvm.core;
using scvm.core.utilities;

namespace DirectVM.core;

public unsafe class RelayedMemoryManagementUnit : IMemoryManagementUnit
{
	public required SCVMMachine machine;
	public MemoryBlock* blocks;
	public int MemoryCount;
	public int AllocMemory(uint size)
	{
		for (int i = 0; i < MemoryCount; i++)
		{
			var block = blocks[i];
			if (!block.IsAllocated)
			{
				block.IsAllocated = true;
				block.ptr = (byte*)StdLib.malloc(size);
				block.size = size;
				return i;
			}
		}
		blocks = (MemoryBlock*)StdLib.realloc((nint)blocks, (uint)((MemoryCount + 1) * sizeof(MemoryBlock)));
		{
			var block = blocks[MemoryCount];
			block.ptr = (byte*)StdLib.malloc(size);
			block.IsAllocated = true;
			block.size=size;
		}
		MemoryCount++;
		return MemoryCount - 1;
	}
	public byte* GetPtr(ulong ptr, ulong PageTable, int CallerCPU, int AssumedAccessSize)
	{
		var BlockID = ptr.CastAs<ulong, uint>(0);
		var offset = ptr.CastAs<ulong, uint>(1);
		if (MemoryCount <= BlockID)
		{
			return machine.SegFault(CallerCPU, ptr);
		}
		var block = blocks[BlockID];
		if (!block.IsAllocated)
		{
			return machine.SegFault(CallerCPU, ptr);
		}
		if (block.size <= offset + AssumedAccessSize)
		{
			return machine.SegFault(CallerCPU, ptr + (uint)AssumedAccessSize - 1);
		}
		return block.ptr + offset;
	}
}