using System.Numerics;

namespace vm.core.Controls
{
	public class TabControl : IDrawable
	{
		public void Draw(DrawCore draw, Vector4 ParentSizeConstraint)
		{
			throw new NotImplementedException();
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
