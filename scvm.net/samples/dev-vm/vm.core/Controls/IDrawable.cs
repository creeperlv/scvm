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
		public int MenuBarHeight;
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
		public Vector2 PointerPos;
		public Font NormalFont;
		public int CurrentElementHandle = int.MinValue;
		public Style CurrentStyle = new Style()
		{
			ButtonBackground = new StatedColor()
			{
				Normal = new Color(0x33, 0x33, 0x33),
				Highlight = new Color(0x55, 0x55, 0x55),
				Pressed = new Color(0x44, 0x44, 0x44),
				Disabled = new Color(0x66, 0x66, 0x66),
			},
			ButtonBorder = new StatedColor()
			{
				Normal = new Color(0x33, 0x33, 0x33),
				Highlight = new Color(0x80, 0x80, 0x80),
				Pressed = new Color(0x50, 0x50, 0x50),
				Disabled = new Color(0x66, 0x66, 0x66),
			},
			ButtonForeground = new StatedColor()
			{
				Normal = Color.White,
				Highlight = Color.White,
				Pressed = Color.White,
				Disabled = Color.White,
			},
			ButtonBorderThickness = 1,
			GenericForeground = Color.White,
			MenuBarHeight = 14,
		};
		int id = 0;
		public void Framestart()
		{
			//Scale = Raylib.GetWindowScaleDPI().X;
			Scale = 2;
			PointerPos = Raylib.GetMousePosition() / Scale;
			id = 0;
		}
		public bool Button(Vector2 Pos, Vector2 Size, string content)
		{
			Rectangle rect = new Rectangle(Pos, Size);
			var isInside = Raylib.CheckCollisionPointRec(PointerPos, rect);
			var btnCntSize = MeasureText(Size, content);
			var center = (Size - btnCntSize) / 2;
			if (isInside)
			{
				DrawRectangle(rect, CurrentStyle.ButtonBackground.Highlight);
				DrawRectangleOutline(rect, CurrentStyle.ButtonBorderThickness, CurrentStyle.ButtonBorder.Highlight);
				DrawText(Pos + center, Size, content, CurrentStyle.ButtonForeground.Highlight);
			}
			else
			{
				DrawRectangle(rect, CurrentStyle.ButtonBackground.Normal);
				DrawRectangleOutline(rect, CurrentStyle.ButtonBorderThickness, CurrentStyle.ButtonBorder.Normal);
				DrawText(Pos + center, Size, content, CurrentStyle.ButtonForeground.Normal);
			}
			id++;
			return false;
		}
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
		public Vector2 MeasureText(Vector2 Size, string Text)
		{
			var Position = Vector2.Zero;
			var codepoint = Raylib.LoadCodepoints(Text, out var l);
			Vector2 CurrentPos = Position;
			int H = 0;
			float MaxW = 0;
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
				MaxW = Math.Max(MaxW, CurrentPos.X);
			}
			return new Vector2(MaxW, H);
		}
		public void DrawRectangle(Vector4 vector, Color color)
		{
			var X = (int)(vector.X * Scale);
			var Y = (int)(vector.Y * Scale);
			var W = (int)(vector.Z * Scale);
			var H = (int)(vector.W * Scale);
			Raylib.DrawRectangle(X, Y, W, H, color);
		}
		public void DrawRectangle(Rectangle vector, Color color)
		{
			var X = (int)(vector.X * Scale);
			var Y = (int)(vector.Y * Scale);
			var W = (int)(vector.Width * Scale);
			var H = (int)(vector.Height * Scale);
			Raylib.DrawRectangle(X, Y, W, H, color);
		}
		public void DrawRectangleOutline(Rectangle rectangle, float Thickness, Color color)
		{
			Rectangle rect = rectangle;
			rect.X = rectangle.X * Scale;
			rect.Y = rectangle.Y * Scale;
			rect.Width = rectangle.Width * Scale;
			rect.Height = rectangle.Height * Scale;
			Raylib.DrawRectangleLinesEx(rect, Thickness * Scale, color);
		}
		public void DrawRectangleOutline(Vector4 vector, float Thickness, Color color)
		{
			var X = (int)(vector.X * Scale);
			var Y = (int)(vector.Y * Scale);
			var W = (int)(vector.Z * Scale);
			var H = (int)(vector.W * Scale);
			Rectangle rect = new Rectangle(X, Y, W, H);
			Raylib.DrawRectangleLinesEx(rect, Thickness * Scale, color);
		}
	}
	public interface IDrawable
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
