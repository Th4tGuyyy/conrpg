using ConsoleGameEngine;
using System;
using System.Collections.Generic;
using System.IO;

namespace rpgtest2020
{
	internal class Program : ConsoleGame
	{
		private const int PIXEL_SIZE = 20;
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

			TargetFramerate = 30;


			GameData.GAME = this;
			GameData.VConsole.gameHandle = this;

			GameData.VConsole.writeLine("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLGMOPeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");

			//loops thru maps folder and adds the maps if valid
			String[] maps = Directory.GetFiles(GameData.MAPSLOCATION);
			for(int i =0; i< maps.Length; i++) {
				String fileName = Path.GetFileName(maps[i]);

				if(fileName.Substring(fileName.Length-4,4) != ".txt")
					throw new Exception(fileName.Substring(fileName.Length - 4, 4));

				GameData.allLevels.Add(fileName, new Level(fileName));
			}
			//GameData.allLevels.Add("outside.txt",new Level("outside.txt"));
			//GameData.allLevels.Add("inside.txt", new Level("inside.txt"));

			foreach(KeyValuePair<string, Level> obj in GameData.allLevels)
				obj.Value.loadLevel();


			GameData.player.level = GameData.allLevels["outside.txt"];
		}

		public override void Render()
		{
			Engine.ClearBuffer();

			if(GameData.currentGameState == GameData.GameState.RUNNING)
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
			if(GameData.currentGameState == GameData.GameState.RUNNING)
				GameData.player.level.update();
		}
	}
}