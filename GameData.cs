using ConsoleGameEngine;
using System.Collections.Generic;
using System;

namespace rpgtest2020
{
	internal class GameData
	{
		public static ConsoleGame GAME;
		public static VirtualConsole VConsole = new VirtualConsole(0, 20, 35, 10);
		public static String METALOCATION = @"C:\Users\carso\source\repos\conrpg2\MetaTags\";
		public static String MAPSLOCATION = @"C:\Users\carso\Source\Repos\conrpg2\Maps\";

		public static Player player = new Player(null, "Boddy", new Glyph("@", Palettes.DARK_BLUE), new Point(0, 0), 10);
		public static Dictionary<String, Level> allLevels = new Dictionary<string, Level>();

		public static GameState currentGameState = GameState.RUNNING;
		public enum GameState
		{
			RUNNING,
			MENU,
			STOP
		};
	}
}