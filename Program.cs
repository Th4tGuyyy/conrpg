using System;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class Program : ConsoleGame
	{
		const int PIXEL_SIZE = 16;
		const int WINDOW_WIDTH = 36;
		const int WINDOW_HEIGHT = 31;

		Player player;

		Level currentLevel;
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
			Engine.SetBackground(0);

			GameData.GAME = this;
			GameData.VConsole.gameHandle = this;
			GameData.VConsole.writeLine("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLGMOPeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
			


			currentLevel = new Level("aidsworld.txt");

			player = new Player(currentLevel,"Bobby", new Glyph("@", Palettes.DARK_BLUE), new Point(4, 4), 10);//later remove player create here, and set in level


			currentLevel.player = player;

		}
		public override void Render()
		{
			Engine.ClearBuffer();

			currentLevel.render();

			//player.render();
			GameData.VConsole.render();
			Engine.Frame(new Point(20, 0), new Point(35, 20), Palettes.DARK_GRAY);

			Engine.Frame(new Point(0, 0), new Point(20, 20), Palettes.DARK_GRAY);

			Engine.DisplayBuffer();
		}


		public override void Update()
		{
			Console.Title = "FPS: " +Math.Floor(GetFramerate());

			//handleKeyDown();
			//player.update();
			currentLevel.update();
		}

	
	}
}
