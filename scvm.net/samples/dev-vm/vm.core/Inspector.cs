using scvm.core;
using System.Numerics;
using vm.core.Controls;

namespace vm.core;

public class Inspector : IDrawable
{
	public SCVMMachine Machine;

	public Inspector(SCVMMachine machine)
	{
		Machine = machine;
	}

	public void Draw(DrawCore draw, Vector4 ParentSizeConstraint)
	{
	}
}
