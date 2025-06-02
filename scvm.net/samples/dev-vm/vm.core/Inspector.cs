using scvm.core;
using System.Numerics;
using vm.core.Controls;
using vm.core.Utils;

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
		var LT = ParentSizeConstraint.GetLT();
		draw.DrawText(LT + new Vector2(0, 0), new Vector2(100, 100), "ASD", Raylib_cs.BleedingEdge.Color.White);
	}

	public string GetTitle()
	{
		return "Inspector";
	}
}
