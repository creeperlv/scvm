using Raylib_cs.BleedingEdge;
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

	public unsafe void Draw(DrawCore draw, Vector4 ParentSizeConstraint)
	{
		var LT = ParentSizeConstraint.GetLT();
		int Y = 0;
		int LSize = 15;
		foreach (var p in Machine.CPU.Processors)
		{
			draw.DrawText(LT + new Vector2(0, Y), new Vector2(ParentSizeConstraint.Z, LSize), "CPU", Raylib_cs.BleedingEdge.Color.White);
			draw.DrawInt(LT + new Vector2(100, Y), p.ThisProcessorID, Raylib_cs.BleedingEdge.Color.White);
			draw.DrawRectangle(new Vector4(LT.X + 200, LT.Y + Y, 100, 12), Color.Green);
			Y += LSize;
			int x = 0;
			int mx = 0;
			for (int i = 0; i < p.state.Register.RegisterLimit; i++)
			{
				var b = p.state.Register.Registers[i];
				var w = draw.MeasureHex(b);
				if (x + w + 5 >= ParentSizeConstraint.Z)
				{
					x = 0;
					Y += LSize;
				}
				draw.DrawRectangle(new Vector4(LT.X + x, LT.Y + Y, w, 12), Color.DarkGreen);
				draw.DrawHex(LT + new Vector2(x, Y), b, Raylib_cs.BleedingEdge.Color.White);
				x += w;
				x += 5;
				mx = Math.Max(mx, x);
			}
			Y += LSize;
			draw.DrawInt(LT + new Vector2(100, Y), mx, Raylib_cs.BleedingEdge.Color.White);

			Y += LSize;
		}
	}

	public string GetTitle()
	{
		return "Inspector";
	}
}
