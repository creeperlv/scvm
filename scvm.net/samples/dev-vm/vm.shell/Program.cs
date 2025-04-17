using Raylib_cs.BleedingEdge;
using System.Numerics;
using vm.core;
using vm.core.Controls;

namespace vm.shell;

class Program
{
	static void Main(string[] args)
	{
		bool ShowInspector = false;
		for (int i = 0; i < args.Length; i++)
		{
			string arg = args[i];
			switch (arg)
			{
				case "--inspector":
					ShowInspector = true;
					break;
				default:
					break;
			}
		}
		Raylib.SetConfigFlags(ConfigFlags.WindowResizable);
		Raylib.InitWindow(800, 600, "DEV VM [Develop]");
		DrawCore core = new DrawCore();
		core.NormalFont = Raylib.GetFontDefault();
		Console.WriteLine(Raylib.GetFontDefault().BaseSize);
		MainWindow mw = new MainWindow();
		mw.MenuBar.SubItems.Add(new MenuItem() { Header="File"});
		mw.MenuBar.SubItems.Add(new MenuItem() { Header="Edit"});
		mw.MenuBar.SubItems.Add(new MenuItem() { Header="Help"});
		while (!Raylib.WindowShouldClose())
		{
			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Black);
			core.Framestart();
			mw.Draw(core, new Vector4(0, 0, Raylib.GetRenderWidth(), Raylib.GetRenderHeight()));
			Raylib.EndDrawing();
		}
	}
}
