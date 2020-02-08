using ConsoleGameEngine;
using System.Collections.Generic;
using System;

namespace rpgtest2020
{
	internal class GameData
	{
		public static ConsoleGame GAME;
		public static VirtualConsole VConsole = new VirtualConsole(0, 20, 35, 12);
		public static int UICOLOR = Palettes.DARK_GRAY;

		public static String METALOCATION = @"C:\Users\carso\Source\Repos\conrpg\MetaTags\";
		public static String MAPSLOCATION = @"C:\Users\carso\Source\Repos\conrpg\Maps\";

		public static Player player = new Player(null, "Bobby", new Glyph("@", Palettes.DARK_BLUE), new Point(0, 0), 10);

		public static Dictionary<String, Level> allLevels = new Dictionary<String, Level>();

		public static GameState currentGameState = GameState.RUNNING;
		public enum GameState
		{
			RUNNING,
			MENU,
			STOP,
		};
	}
}