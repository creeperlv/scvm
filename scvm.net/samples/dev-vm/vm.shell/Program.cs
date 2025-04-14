using Raylib_cs.BleedingEdge;
using System.Numerics;
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
		while (!Raylib.WindowShouldClose())
		{
			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Black);
			core.DrawText(new System.Numerics.Vector2(10, 10), new System.Numerics.Vector2(50, 8), "A fox jumps over the lazy dog", Color.White);
			Raylib.DrawText("A fox jumps over the lazy dog", 10, 50, 10, Color.White);
			Raylib.EndDrawing();
		}
	}
}
