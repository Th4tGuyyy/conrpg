using ConsoleGameEngine;
using System;
using System.IO;

namespace rpgtest2020
{
	internal class Program : ConsoleGame
	{
		private const int PIXEL_SIZE = 16;
		private const int WINDOW_WIDTH = 36;
		private const int WINDOW_HEIGHT = 33;

		private static void Main(string[] args)
		{
			new Program().Construct(WINDOW_WIDTH, WINDOW_HEIGHT, PIXEL_SIZE, PIXEL_SIZE, FramerateMode.MaxFps);
		}

		public override void Create()
		{
			if(File.Exists(GameData.METALOCATION + "Error.txt"))
				File.Delete(GameData.METALOCATION + "Error.txt");

			Engine.SetPalette(Palettes.Default);
			//Engine.Borderless();
			Console.Title = "Test";
			TargetFramerate = 100;
			//Engine.SetBackground(Palettes.DARK_CYAN);

			GameData.GAME = this;
			GameData.VConsole.gameHandle = this;
			GameData.VConsole.writeLine("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLGMOPeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");

			//String k = "outside.text"
			GameData.allLevels.Add("outside.txt",new Level("outside.txt"));
			GameData.allLevels.Add("inside.txt", new Level("inside.txt"));

			GameData.allLevels["outside.txt"].loadLevel();
			GameData.allLevels["inside.txt"].loadLevel();

			GameData.player.level = GameData.allLevels["outside.txt"];
		}

		public override void Render()
		{
			Engine.ClearBuffer();

			//GameData.currentLevel.render();
			GameData.player.level.render();

			//player.render();
			GameData.VConsole.render();
			Engine.Frame(new Point(20, 0), new Point(35, 20), Palettes.DARK_GRAY);

			Engine.Frame(new Point(0, 0), new Point(20, 20), Palettes.DARK_GRAY);

			Engine.Frame(new Point(0, 30), new Point(35, 32), Palettes.DARK_GRAY);

			Engine.DisplayBuffer();
		}

		public override void Update()
		{
			Console.Title = "FPS: " + Math.Floor(GetFramerate());

			//handleKeyDown();
			//player.update();
			//GameData.currentLevel.update();
			GameData.player.level.update();
		}
	}
}