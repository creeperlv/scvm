using scvm.core.utilities;
using System.Collections.Generic;

namespace scvm.core
{
	public unsafe class MemoryManageUnit : IMemoryManagementUnit
	{
		public List<Memory> Memories;
		public SCVMMachine machine;
		public byte* GetPtr(ulong ptr, ulong PageTable, int CallerCPU, int AssumedAccessSize)
		{
			var table = GetTable(PageTable);
			bool IsHit = false;
			if (table->IsDirectAccess == true)
			{
				var address = GetFromPhysicalAddress(ptr, &IsHit);
				if (!IsHit)
				{
					machine.SegFault(CallerCPU, ptr);
				}
				return address;
			}
			else
			{
				var __ptr = table->Translate(ptr, &IsHit);
				if (!IsHit)
				{
					machine.SegFault(CallerCPU, ptr);
				}
				var address = GetFromPhysicalAddress(__ptr, &IsHit);
				if (!IsHit)
				{
					machine.SegFault(CallerCPU, __ptr);
				}
				return address;
			}
		}
		public byte* GetFromPhysicalAddress(ulong ptr, bool* IsSuccess)
		{
			ulong BaseAddr = 0;
			for (int i = 0; i < Memories.Count; i++)
			{
				var __size = BaseAddr + Memories[i].Size;
				if (__size > ptr)
				{
					IsSuccess[0] = true;
					return Memories[i].Ptr + ptr.CastAs<ulong, uint>(1);
				}
				else
				{
					BaseAddr += Memories[i].Size;
				}
			}
			IsSuccess[0] = false;
			return null;
		}
		public PageTable* GetTable(ulong PageTablePtr)
		{
			byte* ptr = Memories[0].Ptr + PageTablePtr;
			return (PageTable*)ptr;
		}
		public byte GetIndex(ulong ptr)
		{
			return ptr.CastAs<ulong, byte>(0);
		}
	}
}
