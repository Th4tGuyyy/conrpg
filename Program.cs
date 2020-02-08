using ConsoleGameEngine;
using System;
using System.Collections.Generic;
using System.IO;

namespace rpgtest2020
{
	internal class Program : ConsoleGame
	{
		private const int PIXEL_SIZE = 16;
		private const int WINDOW_WIDTH = 36;
		private const int WINDOW_HEIGHT = 33;

		private KeyboardHandler gameKeys;

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

			gameKeys = new KeyboardHandler();

			gameKeys.add(ConsoleKey.Enter, () => GameData.VConsole.switchState(), 0.5f);

			TargetFramerate = 60;


			GameData.GAME = this;
			//GameData.VConsole.gameHandle = this;

			GameData.VConsole.writeLine("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLGMOPeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");

			//loops thru maps folder and adds the maps if valid
			String[] maps = Directory.GetFiles(GameData.MAPSLOCATION);
			for(int i =0; i< maps.Length; i++) {
				String fileName = Path.GetFileName(maps[i]);

				if(fileName.Substring(fileName.Length-4,4) != ".txt")
					throw new Exception(fileName.Substring(fileName.Length - 4, 4) + "is not a valid file type");

				GameData.allLevels.Add(fileName, new Level(fileName));
			}


			GameData.player.level = GameData.allLevels["openworld.txt"];

			foreach(KeyValuePair<string, Level> obj in GameData.allLevels)
				obj.Value.loadLevel();


		}

		public override void Render()
		{
			Engine.ClearBuffer();

			if(GameData.currentGameState != GameData.GameState.STOP)
				GameData.player.level.render();

			//player.render();
			GameData.VConsole.render();
			Engine.Frame(new Point(20, 0), new Point(35, 20), Palettes.DARK_GRAY);//temp menu frame

			Engine.Frame(new Point(0, 0), new Point(20, 20), Palettes.DARK_GRAY);//frame around game

			//Engine.Frame(new Point(0, 30), new Point(35, 32), Palettes.DARK_GRAY);//temp input frame

			Engine.DisplayBuffer();
		}

		public override void Update()
		{
			Console.Title = "FPS: " + Math.Floor(GetFramerate());

			
			gameKeys.handle();

			if(!GameData.VConsole.READING)
				GameData.player.level.update();
			else
				GameData.VConsole.update();

		}
	}
}