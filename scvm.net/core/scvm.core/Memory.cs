using scvm.core.utilities;

namespace scvm.core
{
	public unsafe class Memory
	{
		public byte* Ptr;
		public ulong Size;
	}
	public unsafe struct PageTable
	{
		public bool IsDirectAccess;
		public uint EntryCount;
		public PageTableEntry* Entries;
		public ulong Translate(ulong ptr, bool* IsSuccess)
		{
			var index = ptr.CastAs<ulong, uint>();
			if (index > EntryCount)
			{
				IsSuccess[0] = false;
				return 0;
			}
			IsSuccess[0] = true;
			return PointerUtilities.Combine<ulong, uint>(Entries[index].Mapped, ptr.CastAs<ulong, uint>(1));
		}
	}
	public struct PageTableEntry
	{
		public uint Mapped;
	}
}
