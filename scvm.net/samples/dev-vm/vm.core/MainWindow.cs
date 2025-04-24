using Raylib_cs.BleedingEdge;
using System.Numerics;
using vm.core.Controls;

namespace vm.core;

public class MainWindow : IDrawable
{
	public MenuItem MenuBar = new MenuItem();
	public TabControl TabControl = new TabControl();
	public List<int> MenuSelection = new List<int>();
	public void Init()
	{
		MenuBar.FinalizeParent();
	}
	void CloseMenu(MenuItem item)
	{
		item.IsOpen = false;
		foreach (var mitem in item.SubItems)
		{
			CloseMenu(mitem);
		}
	}
	bool TryOpenMenu = false;
	void OpenMenu(MenuItem item)
	{
		TryOpenMenu = true;
		item.IsOpen = true;
		if (item.Parent is not null)
		{
			OpenMenu(item.Parent);
		}
	}
	void DrawMenu(DrawCore core, MenuItem MenuBar, Vector2 BasePos, bool IsHorizontal = false)
	{
		if (IsHorizontal)
		{
			int posX = 0;
			for (int i = 0; i < MenuBar.SubItems.Count; i++)
			{
				MenuItem? item = MenuBar.SubItems[i];
				var w = Raylib.MeasureText(item.Header ?? "", 10) + 10;
				if (core.Button(BasePos + new Vector2(posX, 0), new Vector2(w, core.CurrentStyle.MenuBarHeight), item.Header ?? "", Alignment.Near))
				{
					if (item.OnClick is not null)
					{
						item.OnClick();
					}
					else
					{
						CloseMenu(this.MenuBar);
						OpenMenu(item);
					}
				}
				if (item.IsOpen)
				{
					DrawMenu(core, item, BasePos + new Vector2(posX, core.CurrentStyle.MenuBarHeight));

				}
				posX += w;
			}
		}
		else
		{
			int posY = 0;
			int MaxW = 0;
			for (int i = 0; i < MenuBar.SubItems.Count; i++)
			{
				MenuItem? item = MenuBar.SubItems[i];
				var w = Raylib.MeasureText(item.Header ?? "", 10) + 10;
				MaxW = Math.Max(MaxW, w);
			}
			for (int i = 0; i < MenuBar.SubItems.Count; i++)
			{
				MenuItem? item = MenuBar.SubItems[i];
				if (core.Button(BasePos + new Vector2(0, posY), new Vector2(MaxW, core.CurrentStyle.MenuBarHeight), item.Header ?? "", Alignment.Near))
				{
					if (item.OnClick is not null)
					{
						item.OnClick();
					}
					else
					{
						CloseMenu(this.MenuBar);
						OpenMenu(item);
					}
				}
				if (item.IsOpen)
				{
					DrawMenu(core, item, BasePos + new Vector2(MaxW, posY));
				}
				posY += core.CurrentStyle.MenuBarHeight;
			}

		}
	}
	public void Draw(DrawCore draw, Vector4 ParentSizeConstraint)
	{
		draw.DrawRectangle(new Vector4(0, 0, ParentSizeConstraint.Z, draw.CurrentStyle.MenuBarHeight), draw.CurrentStyle.ButtonBackground.Normal);
		var OX = ParentSizeConstraint.X;
		var OY = ParentSizeConstraint.Y;
		TryOpenMenu = false;
		{
			ParentSizeConstraint.W -= draw.CurrentStyle.MenuBarHeight;
			ParentSizeConstraint.Y += draw.CurrentStyle.MenuBarHeight;
			TabControl.Draw(draw, ParentSizeConstraint);
		}
		DrawMenu(draw, MenuBar, new Vector2(OX, OY), true);
		if (draw.IsClicked && !TryOpenMenu)
		{
			CloseMenu(MenuBar);
		}
	}
}