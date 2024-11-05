using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DirectVM.core.libc
{
	public unsafe partial class StdLib
	{
		public static void init()
		{
			NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), DllImportResolver);
		}
		[LibraryImport("libc", SetLastError = true)]
		public static partial void* malloc(uint size);
		[LibraryImport("libc", SetLastError = true)]
		public static partial void free(IntPtr ptr);
		[LibraryImport("libc", SetLastError = true)]
		public static partial nint realloc(IntPtr ptr, uint size);

		private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
		{
			if (libraryName == "libc")
			{
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					return NativeLibrary.Load("msvcrt.dll", assembly, searchPath);
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
				{
					return NativeLibrary.Load("libc", assembly, searchPath);
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
				{
					return NativeLibrary.Load("libSystem.dylib", assembly, searchPath);
				}
			}
			return IntPtr.Zero;
		}
	}
}
