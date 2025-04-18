using Raylib_cs.BleedingEdge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace vm.core.Controls
{
	public class Style
	{
		public StatedColor ButtonBackground;
		public StatedColor ButtonBorder;
		public StatedColor ButtonForeground;
		public float ButtonBorderThickness;
		public Color GenericForeground;
		public Color GenericBackground;
		public Vector2 ButtonBorderDistance;
		public int MenuBarHeight;
	}
	public struct StatedColor
	{
		public Color Normal;
		public Color Highlight;
		public Color Pressed;
		public Color Disabled;
	}
	public interface IDrawable
	{
		void Draw(DrawCore draw, Vector4 ParentSizeConstraint);
	}
	public class MenuItem
	{
		public MenuItem? Parent = null;
		public string? Header;
		public bool IsOpen = false;
		public Action? OnClick = null;
		public List<MenuItem> SubItems = new List<MenuItem>();
		public void FinalizeParent()
		{
			foreach (var item in SubItems)
			{
				item.Parent = this;
				item.FinalizeParent();
			}
		}
		public void AddItem(MenuItem item)
		{
			item.Parent = this;
			SubItems.Add(item);
		}
	}
}
