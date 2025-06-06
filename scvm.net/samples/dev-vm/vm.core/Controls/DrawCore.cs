﻿using Raylib_cs.BleedingEdge;
using System.Numerics;

namespace vm.core.Controls
{
	public unsafe class DrawCore
	{
		public float Scale = 1;
		public int W;
		public int H;
		public Vector2 PointerPos;
		public Font NormalFont;
		public int CurrentElementHandle = int.MinValue;
		bool IsLeftDown = false;
		bool IsLeftDownLastFrame = false;
		bool IsLeftPressed = false;
		int Selection = -1;
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
			HighlightedButtonBackground = new StatedColor()
			{
				Normal = new Color(0x22, 0x88, 0xEE),
				Highlight = new Color(0x33, 0xAA, 0xFF),
				Pressed = new Color(0x00, 0x66, 0xCC),
				Disabled = new Color(0x36, 0x46, 0xA0),
			},
			HighlightedButtonBorder = new StatedColor()
			{
				Normal = new Color(0x22, 0x88, 0xEE),
				Highlight = new Color(0x33, 0xAA, 0xFF),
				Pressed = new Color(0x00, 0x66, 0xCC),
				Disabled = new Color(0x36, 0x46, 0xA0),
			},
			HighlightedButtonForeground = new StatedColor()
			{
				Normal = Color.White,
				Highlight = Color.White,
				Pressed = Color.White,
				Disabled = Color.White,
			},
			TabControlTabStrip = new Color(0x33, 0x33, 0x33),
			ButtonBorderDistance = new Vector2(4, 2),
			ButtonBorderThickness = 1,
			GenericForeground = Color.White,
			MenuBarHeight = 14,
		};
		int id = 0;
		public bool IsClicked = false;
		public void Framestart()
		{
			//Scale = Raylib.GetWindowScaleDPI().X;
			Scale = 2;
			PointerPos = Raylib.GetMousePosition() / Scale;
			id = 0;
			IsLeftDown = Raylib.IsMouseButtonDown(MouseButton.Left);
			IsLeftPressed = Raylib.IsMouseButtonPressed(MouseButton.Left);
			IsClicked = !IsLeftDown && IsLeftDownLastFrame;
		}
		public void Frameend()
		{
			IsLeftDownLastFrame = IsLeftDown;
			if (!IsLeftDown) Selection = -1;
		}
		public bool Button(Vector2 Pos, Vector2 Size, string content, Alignment HorizontalAlignment = Alignment.Center, bool IsHighlighted = false)
		{
			Rectangle rect = new Rectangle(Pos, Size);
			var isInside = Raylib.CheckCollisionPointRec(PointerPos, rect);
			var btnCntSize = MeasureText(Size, content);
			var Offset = (Size - btnCntSize) / 2;
			switch (HorizontalAlignment)
			{
				case Alignment.Near:
					Offset.X = Math.Min(CurrentStyle.ButtonBorderDistance.X, Size.X - btnCntSize.X);
					break;
				case Alignment.Center:
					break;
				case Alignment.Far:
					Offset.X = Math.Min(Size.X - btnCntSize.X - CurrentStyle.ButtonBorderDistance.X, Size.X - btnCntSize.X);
					break;
				default:
					break;
			}
			bool WillInvoke = false;
			StatedColor Background = IsHighlighted ? CurrentStyle.HighlightedButtonBackground : CurrentStyle.ButtonBackground;
			StatedColor Border = IsHighlighted ? CurrentStyle.HighlightedButtonBorder : CurrentStyle.ButtonBorder;
			StatedColor Foreground = IsHighlighted ? CurrentStyle.HighlightedButtonForeground : CurrentStyle.ButtonForeground;
			if (isInside)
			{
				Color BG = Background.Highlight;
				Color BD = Border.Highlight;
				if ((IsLeftDown && !IsLeftDownLastFrame) || (IsLeftDown && Selection == id))
				{
					Selection = id;
					BG = Background.Pressed;
					BD = Border.Pressed;
				}
				else
				if (!IsLeftDown && Selection == id)
				{
					WillInvoke = true;
				}
				DrawRectangle(rect, BG);
				DrawRectangleOutline(rect, CurrentStyle.ButtonBorderThickness, BD);
				DrawText(Pos + Offset, Size, content, Foreground.Highlight);
			}
			else
			{
				DrawRectangle(rect, Background.Normal);
				DrawRectangleOutline(rect, CurrentStyle.ButtonBorderThickness, Border.Normal);
				DrawText(Pos + Offset, Size, content, Foreground.Normal);
			}
			id++;
			return WillInvoke;
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
		public void DrawInt(Vector2 Position, int data, Color color)
		{
			var c = new Color(color.R, color.G, color.B, color.A);
			Vector2 CurrentPos = Position;
			int H = MeasureInt(data);
			CurrentPos.X += H;
			while (true)
			{
				var d = data % 10;
				data = data / 10;
				char ch = (char)('0' + d);
				int point = ch;
				var info = Raylib.GetGlyphInfo(NormalFont, point);
				var rectangle = Raylib.GetGlyphAtlasRec(NormalFont, point);
				CurrentPos.X -= info.AdvanceX + info.Image.Width + 1;
				Raylib.DrawTextCodepoint(NormalFont, point, CurrentPos * Scale, NormalFont.BaseSize * Scale, color);
				if (data == 0)
				{
					break;
				}
			}
		}
		public int MeasureInt(int data)
		{
			int V = 0;
			while (true)
			{
				var d = data % 10;
				data = data / 10;
				char ch = (char)('0' + d);
				int point = ch;
				var info = Raylib.GetGlyphInfo(NormalFont, point);
				var rectangle = Raylib.GetGlyphAtlasRec(NormalFont, point);
				V += info.AdvanceX + info.Image.Width + 1;
				if (data == 0)
				{
					break;
				}
			}
			return V;
		}
		public int MeasureHex(byte data)
		{
			int W = 0;
			byte b0 = data;
			while (true)
			{
				var d = b0 % 0x10;
				b0 = (byte)(b0 / 0x10);
				char ch = (char)('0' + d);
				if (d >= 0xA)
				{
					ch = (char)('A' + d - 0xA);
				}
				int point = ch;
				var info = Raylib.GetGlyphInfo(NormalFont, point);
				W += info.AdvanceX + info.Image.Width + 1;
				if (b0 == 0)
				{
					break;
				}
			}
			if (data <= 0xF)
			{
				int point = '0';
				var info = Raylib.GetGlyphInfo(NormalFont, point);
				W += info.AdvanceX + info.Image.Width + 1;
			}
			return W;
		}
		public int DrawHex(Vector2 Position, byte data, Color color)
		{
			var c = new Color(color.R, color.G, color.B, color.A);
			Vector2 CurrentPos = Position;
			byte b0 = data;
			var w = MeasureHex(data);
			CurrentPos.X += w;
			while (true)
			{
				var d = b0 % 0x10;
				b0 = (byte)(b0 / 0x10);
				char ch = (char)('0' + d);
				if (d >= 0xA)
				{
					ch = (char)('A' + d - 0xA);
				}
				int point = ch;
				var info = Raylib.GetGlyphInfo(NormalFont, point);
				CurrentPos.X -= info.AdvanceX + info.Image.Width + 1;
				Raylib.DrawTextCodepoint(NormalFont, point, CurrentPos * Scale, NormalFont.BaseSize * Scale, color);
				if (b0 == 0)
				{
					break;
				}
			}
			if (data <= 0xF)
			{
				int point = '0';
				var info = Raylib.GetGlyphInfo(NormalFont, point);
				CurrentPos.X -= info.AdvanceX + info.Image.Width + 1;
				Raylib.DrawTextCodepoint(NormalFont, point, CurrentPos * Scale, NormalFont.BaseSize * Scale, color);
			}
			return w;
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
	public enum Alignment
	{
		Near, Center, Far
	}
}
