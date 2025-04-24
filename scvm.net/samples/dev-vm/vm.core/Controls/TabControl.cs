using Raylib_cs.BleedingEdge;
using System.Numerics;

namespace vm.core.Controls
{
	public class TabControl : IDrawable
	{
		public List<ITabPage> TabPages = new List<ITabPage>();
		public List<string> Titles = new List<string>();
		public void AddPage(ITabPage tabPage)
		{

		}
		public void RemovePage(ITabPage page)
		{
		}
		int SelectedTabPage = -1;
		public void Draw(DrawCore draw, Vector4 ParentSizeConstraint)
		{
			//throw new NotImplementedException();

			int Height = draw.NormalFont.BaseSize + 4;
			var startPos = new Vector2(ParentSizeConstraint.X, ParentSizeConstraint.Y);
			int Index = 0;
			draw.DrawRectangle(new Rectangle(startPos, new Vector2(ParentSizeConstraint.W, Height)), draw.CurrentStyle.TabControlTabStrip);
			foreach (var item in Titles)
			{
				var size = draw.MeasureText(Vector2.PositiveInfinity, item);
				var width = size.X + 8;
				int id = Index;

				Vector2 RealSize = new(width, Height);
				if (draw.Button(startPos, RealSize, item, Alignment.Center))
				{
					SelectedTabPage = id;
				}
				if (id == SelectedTabPage)
				{
					draw.DrawRectangleOutline(new Rectangle(startPos, RealSize), 1, new Color(0x22, 0x88, 0xEE));
				}
				startPos.X += width;
				Index++;
			}
		}
	}
	public interface ITabPage : IDrawable
	{
		public string GetTitle();
	}
	public class TagPageConfig
	{
		public bool Closable;
		public Action? OnSave;
		public Action? OnClose;
	}
}
