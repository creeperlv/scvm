using System;

namespace scvm.core
{
	public interface IMemoryManagementUnit : IDisposable
	{
		unsafe byte* GetPtr(ulong ptr, ulong PageTable, int CallerProcessor, int AssumedAccessSize);
		void SetPageTableStart(ulong offset);
		void SetPageTableSize(ulong size);
		ulong GetPageTableStart();
		ulong GetPageTableSize();
	}
}