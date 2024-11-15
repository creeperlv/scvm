namespace scvm.core
{
	public interface IMemoryManagementUnit
	{
		unsafe byte* GetPtr(ulong ptr, ulong PageTable, int CallerProcessor, int AssumedAccessSize);
	}
}