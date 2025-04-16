using scvm.core;
using System.Numerics;
using vm.core.Controls;

namespace vm.core;

public class Inspector : ITabPage
{
	public SCVMMachine Machine;

	public Inspector(SCVMMachine machine)
	{
		Machine = machine;
	}

	public void Draw(DrawCore draw, Vector4 ParentSizeConstraint)
	{
	}

	public string GetTitle()
	{
		return "Inspector";
	}
}
