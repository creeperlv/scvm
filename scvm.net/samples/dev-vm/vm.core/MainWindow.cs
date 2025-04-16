using Raylib_cs.BleedingEdge;
using System.Numerics;
using vm.core.Controls;

namespace vm.core;

public class MainWindow : IDrawable
{
	public MenuItem MenuBar = new MenuItem();
	public void Draw(DrawCore draw, Vector4 ParentSizeConstraint)
	{
		draw.DrawText(new Vector2(10, 10), new Vector2(50, 24), "A fox jumps over the lazy dog", Color.White);
	}
}