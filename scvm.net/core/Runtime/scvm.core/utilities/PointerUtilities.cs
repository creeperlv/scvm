using System;
using System.Text;

namespace scvm.core.utilities
{
	public unsafe static class PointerUtilities
	{
		public static unsafe void Set<V>(this IntPtr t, V v, int OffsetInBytes = 0) where V : unmanaged
		{
			byte* ptr = ((byte*)t) + OffsetInBytes;
			((V*)ptr)[0] = v;
		}
		public static unsafe void SetOr(this IntPtr t, byte v, int OffsetInBytes = 0)
		{
			byte* ptr = ((byte*)t) + OffsetInBytes;
			(ptr)[0] = (byte)(v | (ptr)[0]);
		}
		public static unsafe void SetOr(this IntPtr t, int v, int OffsetInBytes = 0)
		{
			int* ptr = (int*)(((byte*)t) + OffsetInBytes);
			(ptr)[0] = (byte)(v | (ptr)[0]);
		}
		public static unsafe void SetData<V>(this IntPtr t, V v, int OffsetInBytes = 0) where V : unmanaged
		{
			byte* ptr = ((byte*)t) + OffsetInBytes;
			((V*)ptr)[0] = v;
		}
		public static V CastAs<T, V>(this T t, ushort offset = 0) where T : unmanaged where V : unmanaged
		{
			return ((V*)&t)[offset];
		}
		public static V CastAsWOffsetBytes<T, V>(this T t, ushort offset = 0) where T : unmanaged where V : unmanaged
		{
			var ptr = (byte*)&t;
			ptr += offset;
			return ((V*)ptr)[0];
		}
		public static T Combine<T, V>(V d0, V d1) where T : unmanaged where V : unmanaged
		{
			T t = default;
			T* t_ptr = &t;
			V* v_ptr = (V*)t_ptr;
			v_ptr[0] = d0;
			v_ptr[1] = d1;
			return t;
		}
	}
}
