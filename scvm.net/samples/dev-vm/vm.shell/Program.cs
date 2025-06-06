﻿using Raylib_cs.BleedingEdge;
using scvm.core;
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
		mw.MenuBar.SubItems.Add(new MenuItem()
		{
			Header = "File",
			SubItems ={
				new MenuItem(){ Header="Open Executable" },
				new MenuItem(){ Header="Exit", OnClick=()=>{
						Environment.Exit(0);
					}
				}
			}
		});
		mw.MenuBar.SubItems.Add(new MenuItem() { Header = "Edit" });
		mw.MenuBar.SubItems.Add(new MenuItem() { Header = "Help",
			SubItems =
			{
				new MenuItem(){ Header="About" }
			}	
		});
		mw.Init();
		//mw.TabControl.Titles.Add("Main VM");
		//mw.TabControl.Titles.Add("Inspector");
		SCVMMachine machine=new SCVMMachine();
		SCVMCPU cpu=new SCVMCPU(4);
		machine.CPU = cpu;
		mw.TabControl.AddPage(new Inspector(machine));
		while (!Raylib.WindowShouldClose())
		{
			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Black);
			core.Framestart();
			mw.Draw(core, new Vector4(0, 0, Raylib.GetRenderWidth()/core.Scale, Raylib.GetRenderHeight() / core.Scale));
			core.Frameend();
			Raylib.EndDrawing();
		}
	}
}
