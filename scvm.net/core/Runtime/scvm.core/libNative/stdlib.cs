using System;
using System.Runtime.InteropServices;

namespace scvm.core.libNative
{
	public unsafe static class posix_string
	{
		public static unsafe void* memcpy(void* dest_str, void* src_str, uint n)
		{
			Buffer.MemoryCopy(src_str, dest_str, n, n);
			return dest_str;
		}
	}
	public unsafe static class stdlib
	{
		public static void* malloc(uint size)
		{
			return (void*)Marshal.AllocHGlobal((int)size);
		}
		public static void* malloc(int size)
		{
			return (void*)Marshal.AllocHGlobal(size);
		}
		public static T* malloc<T>(uint size) where T : unmanaged
		{
			return (T*)Marshal.AllocHGlobal((int)size);
		}
		public static T* malloc<T>(int size) where T : unmanaged
		{
			return (T*)Marshal.AllocHGlobal(size);
		}
		public static T* calloc<T>(int size) where T : unmanaged
		{
			var ptr = (T*)Marshal.AllocHGlobal(size);
			for (int i = 0; i < size; i++)
			{
				ptr[i] = default(T);
			}
			return ptr;
		}
		public static void free(void* ptr)
		{
			Marshal.FreeHGlobal((IntPtr)ptr);
		}
		public static void free<T>(T* ptr) where T : unmanaged
		{
			Marshal.FreeHGlobal((IntPtr)ptr);
		}
		public static T* realloc<T>(T* ptr, uint size) where T : unmanaged
		{
			return (T*)Marshal.ReAllocHGlobal((IntPtr)ptr, (IntPtr)size);
		}
	}
}
