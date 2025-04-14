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
	}
	public struct StatedColor
	{
		public Color Normal;
		public Color Highlight;
		public Color Pressed;
		public Color Disabled;
	}
	public unsafe class DrawCore
	{
		public float Scale = 1;
		public int W;
		public int H;
		public Font NormalFont;
		public int CurrentElementHandle = int.MinValue;
		public void DrawText(Vector2 Position, Vector2 Size, string Text, Color color)
		{
			var c = new Color(color.R, color.G, color.B, color.A);
			var codepoint = Raylib.LoadCodepoints(Text, out var l);
			Vector2 CurrentPos = Position;
			int H = 0;
			for (int i = 0; i < l; i++)
			{
				int point = codepoint[i];
				var info = Raylib.GetGlyphInfo(NormalFont, point);
				var rectangle = Raylib.GetGlyphAtlasRec(NormalFont, point);
				H = Math.Max(H, info.Image.Height);
				if (CurrentPos.Y - Position.Y + H > Size.Y)
				{
					break;
				}
				Raylib.DrawTextCodepoint(NormalFont, point, CurrentPos * Scale, NormalFont.BaseSize * Scale, color);
				CurrentPos.X += info.AdvanceX + info.Image.Width + 1;
				if (point == '\n')
				{
					CurrentPos.Y += H;
					H = 0;
					CurrentPos.X = Position.X;
					continue;
				}
				if (CurrentPos.X - Position.X > Size.X)
				{
					CurrentPos.Y += H;
					H = 0;
					CurrentPos.X = Position.X;
				}
			}
		}
		public void DrawRectangle(Vector4 vector, Color color)
		{
			var X = (int)(vector.X * Scale);
			var Y = (int)(vector.Y * Scale);
			var W = (int)(vector.Z * Scale);
			var H = (int)(vector.W * Scale);
			Raylib_cs.BleedingEdge.Raylib.DrawRectangle(X, Y, W, H, color);
		}
	}
	internal interface IDrawable
	{
		void Draw(DrawCore draw, Vector4 ParentSizeConstraint);
	}
	public class MenuItem
	{
		public MenuItem? Parent = null;
		public string? Header;
		public Action? OnClick;
		public List<MenuItem> SubItems = new List<MenuItem>();
	}
}
