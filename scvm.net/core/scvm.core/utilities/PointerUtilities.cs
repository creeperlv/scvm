using System;
using System.Collections.Generic;
using System.Text;

namespace scvm.core.utilities
{
	public unsafe static class PointerUtilities
	{
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
