using System.Runtime.InteropServices;

namespace DirectVM.core;
public class SyscallNames
{
	public const int exit = 1;
	public const int read = 3;
	public const int write = 4;
	public const int open = 5;
	public const int close = 6;
	public const int link = 9;
	public const int unlink = 10;
	public const int chdir = 12;
	public const int getpid = 20;
}
[StructLayout(LayoutKind.Sequential)]
public unsafe struct MemoryBlock
{
	public bool IsAllocated;
	public byte* ptr;
	public uint size;
}
