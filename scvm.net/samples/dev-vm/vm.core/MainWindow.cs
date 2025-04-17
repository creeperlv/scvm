using Raylib_cs.BleedingEdge;
using System.Numerics;
using vm.core.Controls;

namespace vm.core;

public class MainWindow : IDrawable
{
	public MenuItem MenuBar = new MenuItem();
	public TabControl TabControl = new TabControl();
	public void Draw(DrawCore draw, Vector4 ParentSizeConstraint)
	{
		int posX = 0;
		foreach (var item in MenuBar.SubItems)
		{
			var w = Raylib.MeasureText(item.Header ?? "", 10) + 10;
			draw.Button(new Vector2(posX, 0), new Vector2(w, draw.CurrentStyle.MenuBarHeight), item.Header ?? "");
			posX += w;
		}
		ParentSizeConstraint.W -= draw.CurrentStyle.MenuBarHeight;
		ParentSizeConstraint.Y += draw.CurrentStyle.MenuBarHeight;
		draw.DrawRectangleOutline(new Rectangle(100,100,100,100),2,Color.White);
		TabControl.Draw(draw, ParentSizeConstraint);
	}
}