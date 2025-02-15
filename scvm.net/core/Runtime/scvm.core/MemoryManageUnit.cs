using scvm.core.libNative;
using scvm.core.utilities;
using System.Collections.Generic;

namespace scvm.core
{
	public unsafe class MemoryManageUnit : IMemoryManagementUnit
	{
		public List<Memory> Memories;
		public SCVMMachine machine;
		ulong PageTableOffset;
		ulong PageTableSize;
		public byte* GetPtr(ulong ptr, ulong PageTable, int CallerProcessor, int AssumedAccessSize)
		{
			var table = GetTable(CallerProcessor, PageTable);
			bool IsHit = false;
			if (table->IsDirectAccess == true)
			{
				var address = GetFromPhysicalAddress(ptr, &IsHit);
				if (!IsHit)
				{
					machine.SegFault(CallerProcessor, ptr);
					return null;
				}
				return address;
			}
			else
			{
				var __ptr = table->Translate(ptr, &IsHit);
				if (!IsHit)
				{
					machine.SegFault(CallerProcessor, ptr);
					return null;
				}
				var address = GetFromPhysicalAddress(__ptr, &IsHit);
				if (!IsHit)
				{
					machine.SegFault(CallerProcessor, __ptr);
					return null;
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
		public PageTable* GetTable(int CallerProcessor, ulong PageTablePtr)
		{
			if (PageTablePtr > PageTableSize)
			{
				machine.InvPage(CallerProcessor, PageTablePtr);
				return null;
			}
			byte* ptr = Memories[0].Ptr + PageTableOffset + PageTablePtr;
			return (PageTable*)ptr;
		}
		public byte GetIndex(ulong ptr)
		{
			return ptr.CastAs<ulong, byte>(0);
		}

		public void Dispose()
		{
			foreach (var item in Memories)
			{
				if (item.Size > 0)
					stdlib.free(item.Ptr);
			}
		}

		public void SetPageTableStart(ulong offset)
		{
			this.PageTableOffset = offset;
		}

		public void SetPageTableSize(ulong size)
		{
			this.PageTableSize = size;
		}
	}
}
