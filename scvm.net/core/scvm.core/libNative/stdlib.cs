using System;
using System.Runtime.InteropServices;

namespace scvm.core.libNative
{
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
