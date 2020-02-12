using ConsoleGameEngine;
using System;
using System.Collections.Generic;
using System.IO;

namespace rpgtest2020
{
	internal class Program : ConsoleGame
	{
		private KeyboardHandler gameKeys;

		private static void Main(string[] args)
		{
			new Program().Construct(GameData.WINDOW_WIDTH, GameData.WINDOW_HEIGHT, GameData.PIXEL_SIZE, GameData.PIXEL_SIZE, FramerateMode.MaxFps);
		}

		public override void Create()
		{
			if(File.Exists(GameData.METALOCATION + "Error.txt"))
				File.Delete(GameData.METALOCATION + "Error.txt");

			Engine.SetPalette(Palettes.Default);
			Engine.Borderless();
			GameData.GAME = this;

			gameKeys = new KeyboardHandler();

			gameKeys.add(ConsoleKey.Enter, () => GameData.VConsole.switchState(), 0.5f);
			gameKeys.add(0xBF, () => GameData.VConsole.switchStateAndSlash(), 0.5f);

			TargetFramerate = 100;


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


			GameData.VConsole.render();

			//Engine.Frame(new Point(20, 0), new Point(35, 20), Palettes.DARK_GRAY);//temp menu frame
			GameData.statsPanel.render();


			if(GameData.currentGameState != GameData.GameState.STOP)
				GameData.player.level.render();


			Engine.DisplayBuffer();
		}

		public override void Update()
		{
			Console.Title = "FPS: " + Math.Floor(GetFramerate());

			
			gameKeys.handle();

			if(!GameData.VConsole.READING)
				GameData.player.level.update();
			

			GameData.VConsole.update();

			GameData.statsPanel.update();

		}
	}
}