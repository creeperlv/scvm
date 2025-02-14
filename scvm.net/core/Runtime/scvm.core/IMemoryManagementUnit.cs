﻿using System;

namespace scvm.core
{
	public interface IMemoryManagementUnit:IDisposable
	{
		unsafe byte* GetPtr(ulong ptr, ulong PageTable, int CallerProcessor, int AssumedAccessSize);
		void SetPageTableCount(ulong offset,ulong size);
	}
}