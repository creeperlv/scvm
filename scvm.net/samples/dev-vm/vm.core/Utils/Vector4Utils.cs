using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace vm.core.Utils
{
	public static class Vector4Utils
	{
		public static Vector2 GetLT(this Vector4 v) => new(v.X, v.Y);
		public static Vector2 GetRB(this Vector4 v) => new(v.X + v.Z, v.Y + v.W);
		public static Vector2 GetSize(this Vector4 v) => new(v.Z, v.W);
	}
}
