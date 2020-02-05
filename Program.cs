using System;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class Program : ConsoleGame
	{
		const int PIXEL_SIZE = 16;
		const int WINDOW_WIDTH = 36;
		const int WINDOW_HEIGHT = 33;

		static void Main(string[] args)
		{
			new Program().Construct(WINDOW_WIDTH, WINDOW_HEIGHT, PIXEL_SIZE, PIXEL_SIZE, FramerateMode.MaxFps);
		}

		public override void Create()
		{
			Engine.SetPalette(Palettes.Default);
			//Engine.Borderless();
			Console.Title = "Test";
			TargetFramerate = 100;
			//Engine.SetBackground(Palettes.DARK_CYAN);

			GameData.GAME = this;
			GameData.VConsole.gameHandle = this;
			GameData.VConsole.writeLine("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLGMOPeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
			
			GameData.currentLevel = new Level("rockworld.txt");
			
		}
		public override void Render()
		{
			Engine.ClearBuffer();

			GameData.currentLevel.render();

			//player.render();
			GameData.VConsole.render();
			Engine.Frame(new Point(20, 0), new Point(35, 20), Palettes.DARK_GRAY);

			Engine.Frame(new Point(0, 0), new Point(20, 20), Palettes.DARK_GRAY);

			Engine.Frame(new Point(0, 30), new Point(35, 32), Palettes.DARK_GRAY);

			Engine.DisplayBuffer();
		}


		public override void Update()
		{
			Console.Title = "FPS: " +Math.Floor(GetFramerate());

			//handleKeyDown();
			//player.update();
			GameData.currentLevel.update();
		}

	
	}
}
